// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class ExportParamRq : Request
{
    public ExportParamRq(string action = "get")
    {
        Action = action;
    }

    public object Data = null;
}

public class ExportParamRp : Response
{
    public ExportParamRpData Data;
}

public class ExportParamRpData
{
    public string Filename;
    public string Url;
}