using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Novicam.Operation;

namespace Novicam;

public class Session : IDisposable
{
    public void Dispose()
    {
        _client?.Dispose();
    }

    public Session(string cameraIp, string userName, string password)
    {
        if (string.IsNullOrEmpty(cameraIp) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            throw new NullReferenceException();

        BaseUrl  = $"http://{cameraIp}";
        UserName = userName;
        Password = password;
    }

    public async Task Login()
    {
        var stringPayload = JsonConvert.SerializeObject(new LoginCapabilitiesRq(new CapabilitiesRqData(UserName)));
        var httpContent   = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        var httpResponse  = await _client.PostAsync(BaseUrl + ApiLoginCapabilities, httpContent);
        httpResponse.EnsureSuccessStatusCode();

        var capabilities = JsonConvert.DeserializeObject<LoginCapabilitiesRp>(await httpResponse.Content.ReadAsStringAsync()).Data;
        var dateTime     = Security.SecurityDateTime();

        PasswordHash = Security.SecurityStep1(UserName, capabilities.Param.Salt, dateTime.Hash, Password);
        PasswordHash = Security.SecurityStep2(PasswordHash, capabilities.Param.Challenge);
        PasswordHash = capabilities.Param.EnableIteration ? Security.SecurityStep3(capabilities.Param.Iterations, PasswordHash) : PasswordHash;

        var payload = new LoginRq(new LoginRqData(UserName, "sha256-1", PasswordHash, capabilities.SessionId, dateTime.DataTime));
        httpContent  = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        httpResponse = await _client.PostAsync(BaseUrl + ApiLogin, httpContent);
        httpResponse.EnsureSuccessStatusCode();

        var loginResponse = JsonConvert.DeserializeObject<LoginRp>(await httpResponse.Content.ReadAsStringAsync());

        var uri = new Uri(BaseUrl);
        CookieContainer.Add(uri, new Cookie("updateTips", "true"));
        CookieContainer.Add(uri, new Cookie("default_streamtype", "true"));
        CookieContainer.Add(uri, new Cookie("cookie", "%7B%22cookie%22%3A%22sessionID%" + loginResponse.Data.Cookie[10..] + "%22%7D"));
        CookieContainer.Add(uri, new Cookie("sessionID", loginResponse.Data.Cookie[10..]));
        _client.DefaultRequestHeaders.Add("cookie", CookieContainer.GetCookieHeader(uri));

        IsLogged = true;
    }

    public async Task<string> ExportParams(string filePath)
    {
        if (!IsLogged) throw new ApplicationException();

        var stringPayload = new StringContent(JsonConvert.SerializeObject(new ExportParamRq()), Encoding.UTF8, "application/json");
        var httpResponse  = await _client.PostAsync(BaseUrl + ApiExportParams, stringPayload);
        httpResponse.EnsureSuccessStatusCode();

        var exportResponse = JsonConvert.DeserializeObject<ExportParamRp>(await httpResponse.Content.ReadAsStringAsync());

        var fileStream = await _client.GetStreamAsync(BaseUrl + exportResponse.Data.Url);

        await using var outputFileStream = new FileStream($"{filePath}/{exportResponse.Data.Filename}", FileMode.Create);
        await fileStream.CopyToAsync(outputFileStream);
        return exportResponse.Data.Filename;
    }

    public async Task ImportParams(string filePath, string fileName)
    {
        if (!IsLogged) throw new ApplicationException();

        using var multipartFormContent = new MultipartFormDataContent();

        var fileStreamContent = new StreamContent(File.OpenRead($"{filePath}/{fileName}"));
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-zip-compressed");

        multipartFormContent.Add(fileStreamContent, "file", fileName);
        try
        {
            await _client.PostAsync(BaseUrl + ApiImportParams, multipartFormContent);
        }
        catch
        {
            // ignored
        }
    }

    public async Task Reboot()
    {
        if (!IsLogged) throw new ApplicationException();

        var stringPayload = new StringContent(JsonConvert.SerializeObject(new RebootRq()), Encoding.UTF8, "application/json");
        var httpResponse  = await _client.PostAsync(BaseUrl + ApiSystemReboot, stringPayload);
        httpResponse.EnsureSuccessStatusCode();

        IsLogged = false;
    }

    private const string ApiLogin             = "/api/session/login";
    private const string ApiImportParams      = "/params.html";
    private const string ApiSystemReboot      = "/api/system/reboot";
    private const string ApiExportParams      = "/api/system/export-params";
    private const string ApiLoginCapabilities = "/api/session/login-capabilities";

    private static readonly CookieContainer CookieContainer = new();

    private static readonly WebProxy Proxy = new()
    {
        Address               = new Uri("http://127.0.0.1:8080"),
        BypassProxyOnLocal    = false,
        UseDefaultCredentials = false
    };

    private readonly HttpClient _client = new(new HttpClientHandler { CookieContainer = CookieContainer, Proxy = Proxy });

    private string BaseUrl      { get; }
    private string UserName     { get; }
    private string Password     { get; }
    private string PasswordHash { get; set; }
    private bool   IsLogged     { get; set; }
}