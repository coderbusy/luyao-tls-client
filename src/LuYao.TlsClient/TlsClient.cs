using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace LuYao.TlsClient;

public class TlsClient : IDisposable
{
    public String SessionId { get; }
    public String? Proxy { get; set; }
    public Boolean FollowRedirect { get; set; }
    public Boolean InsecureSkipVerify { get; set; }
    public Boolean DisableIPV6 { get; set; }
    public Boolean DisableIPV4 { get; set; }
    public String? LocalAddress { get; set; }
    public Boolean StreamOutput { get; set; }
    public TimeSpan Timeout { get; set; } = TimeSpan.Zero;
    public Boolean WithDebug { get; set; }
    public String TLSClientIdentifier
    {
        get => tlsClientIdentifier;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            tlsClientIdentifier = value;
        }
    }
    public TlsClient(string sessionId)
    {
        SessionId = sessionId ?? throw new ArgumentNullException(nameof(sessionId));
    }

    public TlsClient() : this(Guid.NewGuid().ToString())
    {

    }

    private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    private string tlsClientIdentifier = ClientIdentifiers.Default;
    private bool disposedValue;

    public JsonSerializerSettings JsonSerializerSettings
    {
        get => _jsonSerializerSettings;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _jsonSerializerSettings = value;
        }
    }

    protected virtual string SerializeObject(Object value) => JsonConvert.SerializeObject(value, JsonSerializerSettings);

    protected virtual T? DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);

    public Response Request(RequestInput input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var request = SerializeObject(input);
        var response = NativeMethods.Request(request);
        Debug.WriteLine(response);
        var output = DeserializeObject<Response>(response);
        return output!;
    }

    public DestroyOutput DestroySession(DestroySessionInput input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var request = SerializeObject(input);
        var response = NativeMethods.DestroySession(request);
        var output = DeserializeObject<DestroyOutput>(response);
        return output!;
    }

    public DestroyOutput DestroyAll()
    {
        var response = NativeMethods.DestroyAll();
        var output = DeserializeObject<DestroyOutput>(response);
        return output!;
    }

    public CookiesFromSessionOutput GetCookiesFromSession(GetCookiesFromSessionInput input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var request = SerializeObject(input);
        var response = NativeMethods.GetCookiesFromSession(request);
        var output = DeserializeObject<CookiesFromSessionOutput>(response);
        return output!;
    }

    public CookiesFromSessionOutput AddCookiesToSession(AddCookiesToSessionInput input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var request = SerializeObject(input);
        var response = NativeMethods.AddCookiesToSession(request);
        var output = DeserializeObject<CookiesFromSessionOutput>(response);
        return output!;
    }

    public CookiesFromSessionOutput GetCookiesFromSession(RequestInput input)
    {
        return GetCookiesFromSession(new GetCookiesFromSessionInput
        {
            SessionId = input.SessionId,
            Url = input.RequestUrl
        });
    }

    public virtual RequestInput CreateRequest()
    {
        var ret = new RequestInput
        {
            SessionId = this.SessionId,
            ProxyUrl = this.Proxy,
            FollowRedirects = this.FollowRedirect,
            TLSClientIdentifier = this.TLSClientIdentifier,
            InsecureSkipVerify = this.InsecureSkipVerify,
            DisableIPV4 = this.DisableIPV4,
            DisableIPV6 = this.DisableIPV6,
            LocalAddress = this.LocalAddress,
            WithDebug = this.WithDebug
        };
        if (this.StreamOutput)
        {
            ret.StreamOutputPath = Path.GetTempFileName();
        }
        if (this.Timeout > TimeSpan.Zero)
        {
            ret.TimeoutSeconds = (int)this.Timeout.TotalSeconds;
        }
        return ret;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
            }
            this.DestroySession(new DestroySessionInput { SessionId = this.SessionId });
            disposedValue = true;
        }
    }

    ~TlsClient()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
