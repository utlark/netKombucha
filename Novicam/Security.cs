using System.Security.Cryptography;
using System.Text;

namespace Novicam;

public static class Security
{
    public static (string Hash, string DataTime) SecurityDateTime()
    {
        var dateTime = DateTime.Now;

        var year   = dateTime.Year.ToString("0000");
        var month  = dateTime.Month.ToString("00");
        var day    = dateTime.Day.ToString("00");
        var hour   = dateTime.Hour.ToString("00");
        var minute = dateTime.Minute.ToString("00");
        var second = dateTime.Second.ToString("00");

        var dataString = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second;
        return (Convert.ToBase64String(Encoding.UTF8.GetBytes(dataString)), dataString);
    }

    public static string SecurityStep1(string user, string salt, string dataTimeHash, string password)
    {
        return Sha256Digest(user + salt + dataTimeHash + password);
    }

    public static string SecurityStep2(string hash, string challenge)
    {
        return Sha256Digest(HexCharCodeToStr(hash) + challenge);
    }

    public static string SecurityStep3(int count, string data)
    {
        for (var i = 0; i < count; i++)
            data = Sha256Digest(HexCharCodeToStr(data));
        return data;
    }

    private static string Sha256Digest(string data)
    {
        var builder = new StringBuilder();
        foreach (var t in SHA256.HashData(data.Select(BitConverter.GetBytes).Select(x => x[0]).ToArray()))
            builder.Append(t.ToString("x2"));
        return builder.ToString();
    }

    private static string HexCharCodeToStr(string hexString)
    {
        hexString = hexString.Trim();
        if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            hexString = hexString[2..];

        if (hexString.Length % 2 != 0)
            throw new InvalidDataException();

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < hexString.Length; i += 2)
        {
            var hexByte  = hexString.Substring(i, 2);
            var charCode = Convert.ToInt32(hexByte, 16);
            stringBuilder.Append((char)charCode);
        }

        return stringBuilder.ToString();
    }
}