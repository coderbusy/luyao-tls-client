using System;

namespace LuYao.TlsClient;

public delegate void RequestCreatingEventHandler(object sender, RequestCreatingEventArgs e);
public class RequestCreatingEventArgs : EventArgs
{
    public RequestInput Request { get; }

    public RequestCreatingEventArgs(RequestInput request)
    {
        Request = request;
    }
}
