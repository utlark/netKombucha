// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Novicam.Operation;

public class Request
{
    public Request()
    {
    }

    public Request(string action)
    {
        Action = action;
    }

    public string Action;
}

public class Response
{
    public Response()
    {
    }

    public Response(int code)
    {
        Code = code;
    }

    public int Code;
}