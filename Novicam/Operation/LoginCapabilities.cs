// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class LoginCapabilitiesRq : Request
{
    public LoginCapabilitiesRq(CapabilitiesRqData data, string action = "get")
    {
        Action = action;
        Data   = data;
    }

    public CapabilitiesRqData Data;
}

public class CapabilitiesRqData
{
    public CapabilitiesRqData(string username)
    {
        Username = username;
    }

    public string Username;
}

public class LoginCapabilitiesRp : Response
{
    public CapabilitiesRpData Data;
}

public class CapabilitiesRpData
{
    public string[]          EncryptionType;
    public CapabilitiesParam Param;
    public string            SessionId;
}

public class CapabilitiesParam
{
    public string Challenge;
    public bool   EnableIteration;
    public int    Iterations;
    public string Salt;
}