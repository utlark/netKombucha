// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class RebootRq : Request
{
    public RebootRq(string action = "set")
    {
        Action = action;
    }

    public object Data = null;
}

public class RebootRp : Response
{
    public object Data;
}