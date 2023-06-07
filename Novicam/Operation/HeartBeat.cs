// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class HeartBeatRq : Request
{
    public HeartBeatRq(string action)
    {
        Action = action;
    }

    public HeartBeatRqData Data;
}

public class HeartBeatRqData
{
    public HeartBeatRqData(string cookie)
    {
        Cookie = cookie;
    }

    public string Cookie;
}

public class HeartBeatRp : Response
{
    public object Data;
}