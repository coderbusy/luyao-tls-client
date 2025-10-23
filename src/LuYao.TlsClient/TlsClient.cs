using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
#if NET6_0_OR_GREATER
using System.Text.Json;
using SystemTextJsonSerializer = System.Text.Json.JsonSerializer;
#endif

namespace LuYao.TlsClient;

public class TlsClient : IDisposable
{
    public event RequestCreatingEventHandler? RequestCreating;
    protected virtual void OnRequestCreating(RequestInput input)
    {
        if (RequestCreating != null)
        {
            var args = new RequestCreatingEventArgs(input);
            RequestCreating?.Invoke(this, args);
        }
    }
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
    public Boolean ForceHttp1 { get; set; }
    public Boolean DisableHttp3 { get; set; }
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

#if NET6_0_OR_GREATER
    private JsonSerializerOptions? _systemTextJsonOptions;
    private bool _useSystemTextJson = true;

    /// <summary>
    /// Gets or sets whether to use System.Text.Json (AOT-compatible) instead of Newtonsoft.Json.
    /// Defaults to true for .NET 6.0 and later. Set to false to use Newtonsoft.Json for compatibility.
    /// </summary>
    public bool UseSystemTextJson
    {
        get => _useSystemTextJson;
        set => _useSystemTextJson = value;
    }

    /// <summary>
    /// Gets or sets the System.Text.Json options for AOT serialization.
    /// </summary>
    public JsonSerializerOptions SystemTextJsonOptions
    {
        get => _systemTextJsonOptions ?? CreateDefaultSystemTextJsonOptions();
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _systemTextJsonOptions = value;
        }
    }

    private JsonSerializerOptions CreateDefaultSystemTextJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if NET8_0_OR_GREATER
            TypeInfoResolver = TlsClientJsonContext.Default
#endif
        };
        return options;
    }
#endif

    private string tlsClientIdentifier = ClientIdentifiers.Default;
    private bool _isDisposed;

    public JsonSerializerSettings JsonSerializerSettings
    {
        get => _jsonSerializerSettings;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _jsonSerializerSettings = value;
        }
    }

#if NET5_0_OR_GREATER
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Fallback path for unknown types. Primary types use source generation.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Fallback path for unknown types. Primary types use source generation.")]
#endif
    protected virtual string SerializeObject(Object value)
    {
#if NET8_0_OR_GREATER
        if (_useSystemTextJson)
        {
            // Use source-generated serialization for AOT compatibility
            return value switch
            {
                RequestInput input => SystemTextJsonSerializer.Serialize(input, TlsClientJsonContext.Default.RequestInput),
                DestroySessionInput input => SystemTextJsonSerializer.Serialize(input, TlsClientJsonContext.Default.DestroySessionInput),
                AddCookiesToSessionInput input => SystemTextJsonSerializer.Serialize(input, TlsClientJsonContext.Default.AddCookiesToSessionInput),
                GetCookiesFromSessionInput input => SystemTextJsonSerializer.Serialize(input, TlsClientJsonContext.Default.GetCookiesFromSessionInput),
                _ => SystemTextJsonSerializer.Serialize(value, value.GetType(), SystemTextJsonOptions)
            };
        }
#elif NET6_0_OR_GREATER
        if (_useSystemTextJson)
        {
            return SystemTextJsonSerializer.Serialize(value, value.GetType(), SystemTextJsonOptions);
        }
#endif
        return JsonConvert.SerializeObject(value, JsonSerializerSettings);
    }

#if NET5_0_OR_GREATER
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Fallback path for unknown types. Primary types use source generation.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Fallback path for unknown types. Primary types use source generation.")]
#endif
    protected virtual T? DeserializeObject<T>(string value)
    {
#if NET8_0_OR_GREATER
        if (_useSystemTextJson)
        {
            // Use source-generated deserialization for AOT compatibility
            var typeInfo = typeof(T).Name switch
            {
                nameof(Response) => (System.Text.Json.Serialization.Metadata.JsonTypeInfo<T>)(object)TlsClientJsonContext.Default.Response,
                nameof(DestroyOutput) => (System.Text.Json.Serialization.Metadata.JsonTypeInfo<T>)(object)TlsClientJsonContext.Default.DestroyOutput,
                nameof(CookiesFromSessionOutput) => (System.Text.Json.Serialization.Metadata.JsonTypeInfo<T>)(object)TlsClientJsonContext.Default.CookiesFromSessionOutput,
                _ => null
            };
            
            if (typeInfo != null)
            {
                return SystemTextJsonSerializer.Deserialize(value, typeInfo);
            }
            return SystemTextJsonSerializer.Deserialize<T>(value, SystemTextJsonOptions);
        }
#elif NET6_0_OR_GREATER
        if (_useSystemTextJson)
        {
            return SystemTextJsonSerializer.Deserialize<T>(value, SystemTextJsonOptions);
        }
#endif
        return JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);
    }

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
            WithDebug = this.WithDebug,
            ForceHttp1 = this.ForceHttp1,
            DisableHttp3 = this.DisableHttp3
        };
        if (this.StreamOutput)
        {
            ret.StreamOutputPath = Path.GetTempFileName();
        }
        if (this.Timeout > TimeSpan.Zero)
        {
            ret.TimeoutSeconds = (int)this.Timeout.TotalSeconds;
        }
        OnRequestCreating(ret);
        return ret;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            this.DestroySession(new DestroySessionInput { SessionId = this.SessionId });
            _isDisposed = true;
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
