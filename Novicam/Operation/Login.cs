// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class LoginRq : Request
{
    public LoginRq(LoginRqData data, string action = "set")
    {
        Action = action;
        Data   = data;
    }

    public LoginRqData Data;
}

public class LoginRqData
{
    public LoginRqData(string username, string loginEncryptionType, string password, string sessionId, string dateTime)
    {
        Username            = username;
        LoginEncryptionType = loginEncryptionType;
        Password            = password;
        SessionId           = sessionId;
        DateTime            = dateTime;
    }

    public string DateTime;
    public string LoginEncryptionType;
    public string Password;
    public string SessionId;
    public string Username;
}

public class LoginRp : Response
{
    public LoginRpData Data;
}

public class LoginRpData
{
    public string Cookie;
}