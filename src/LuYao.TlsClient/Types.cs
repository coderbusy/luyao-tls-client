using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuYao.TlsClient;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
#if NET8_0_OR_GREATER
using System.Text.Json.Serialization;
using NewtonsoftJsonPropertyAttribute = Newtonsoft.Json.JsonPropertyAttribute;
#endif


//请阅读以下 golang 中定义的 struct 信息，并使用 C# 语言及 JSON.NET 库编写出等效的 C# 类型。
//https://github.com/bogdanfinn/tls-client/blob/master/cffi_src/types.go

public class DestroySessionInput
{
    [JsonProperty("sessionId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }
}

public class DestroyOutput : ResponseBase
{

    [JsonProperty("success")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("success")]
#endif
    public bool Success { get; set; }
}

public class AddCookiesToSessionInput
{
    [JsonProperty("cookies")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public List<Cookie> Cookies { get; set; } = new List<Cookie>();

    [JsonProperty("sessionId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    [JsonProperty("url")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("url")]
#endif
    public string Url { get; set; }
}

public class GetCookiesFromSessionInput
{
    [JsonProperty("sessionId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    [JsonProperty("url")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("url")]
#endif
    public string Url { get; set; }
}

public class CookiesFromSessionOutput : ResponseBase
{

    [JsonProperty("cookies")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public List<Cookie> Cookies { get; set; }
}

public class RequestInput
{
    [JsonProperty("catchPanics")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("catchPanics")]
#endif
    public bool CatchPanics { get; set; }

    [JsonProperty("certificatePinningHosts")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("certificatePinningHosts")]
#endif
    public Dictionary<string, List<string>> CertificatePinningHosts { get; set; }

    [JsonProperty("customTlsClient")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("customTlsClient")]
#endif
    public CustomTlsClient CustomTlsClient { get; set; }

    [JsonProperty("transportOptions")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("transportOptions")]
#endif
    public TransportOptions TransportOptions { get; set; }

    [JsonProperty("followRedirects")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("followRedirects")]
#endif
    public bool FollowRedirects { get; set; }

    [JsonProperty("forceHttp1")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("forceHttp1")]
#endif
    public bool ForceHttp1 { get; set; }

    [JsonProperty("disableHttp3")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableHttp3")]
#endif
    public bool DisableHttp3 { get; set; }

    [JsonProperty("headerOrder")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("headerOrder")]
#endif
    public List<string> HeaderOrder { get; set; } = new List<string>();

    [JsonProperty("headers")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("headers")]
#endif
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    [JsonProperty("defaultHeaders")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("defaultHeaders")]
#endif
    public Dictionary<string, List<string>> DefaultHeaders { get; set; }

    [JsonProperty("connectHeaders")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("connectHeaders")]
#endif
    public Dictionary<string, List<string>> ConnectHeaders { get; set; }

    [JsonProperty("insecureSkipVerify")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("insecureSkipVerify")]
#endif
    public bool InsecureSkipVerify { get; set; }

    [JsonProperty("isByteRequest")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("isByteRequest")]
#endif
    public bool IsByteRequest { get; set; }

    [JsonProperty("isByteResponse")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("isByteResponse")]
#endif
    public bool IsByteResponse { get; set; }

    [JsonProperty("isRotatingProxy")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("isRotatingProxy")]
#endif
    public bool IsRotatingProxy { get; set; }

    [JsonProperty("disableIPV6")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableIPV6")]
#endif
    public bool DisableIPV6 { get; set; }

    [JsonProperty("disableIPV4")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableIPV4")]
#endif
    public bool DisableIPV4 { get; set; }

    [JsonProperty("localAddress")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("localAddress")]
#endif
    public string? LocalAddress { get; set; }

    [JsonProperty("serverNameOverwrite")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("serverNameOverwrite")]
#endif
    public string ServerNameOverwrite { get; set; }

    [JsonProperty("proxyUrl")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("proxyUrl")]
#endif
    public string? ProxyUrl { get; set; }

    [JsonProperty("requestBody")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestBody")]
#endif
    public string RequestBody { get; set; }

    [JsonProperty("requestCookies")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestCookies")]
#endif
    public List<Cookie> RequestCookies { get; set; }

    [JsonProperty("requestMethod")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestMethod")]
#endif
    public string RequestMethod { get; set; }

    [JsonProperty("requestUrl")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestUrl")]
#endif
    public string RequestUrl { get; set; }

    [JsonProperty("requestHostOverride")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestHostOverride")]
#endif
    public string RequestHostOverride { get; set; }

    [JsonProperty("sessionId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    [JsonProperty("streamOutputBlockSize")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputBlockSize")]
#endif
    public int? StreamOutputBlockSize { get; set; }

    [JsonProperty("streamOutputEOFSymbol")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputEOFSymbol")]
#endif
    public string? StreamOutputEOFSymbol { get; set; }

    [JsonProperty("streamOutputPath")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputPath")]
#endif
    public string? StreamOutputPath { get; set; }

    [JsonProperty("timeoutMilliseconds")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("timeoutMilliseconds")]
#endif
    public int TimeoutMilliseconds { get; set; }

    [JsonProperty("timeoutSeconds")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("timeoutSeconds")]
#endif
    public int TimeoutSeconds { get; set; }

    [JsonProperty("tlsClientIdentifier")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("tlsClientIdentifier")]
#endif
    public string TLSClientIdentifier { get; set; } = ClientIdentifiers.Default;

    [JsonProperty("withDebug")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("withDebug")]
#endif
    public bool WithDebug { get; set; }

    [JsonProperty("withDefaultCookieJar")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("withDefaultCookieJar")]
#endif
    public bool WithDefaultCookieJar { get; set; }

    [JsonProperty("withoutCookieJar")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("withoutCookieJar")]
#endif
    public bool WithoutCookieJar { get; set; }

    [JsonProperty("withRandomTLSExtensionOrder")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("withRandomTLSExtensionOrder")]
#endif
    public bool WithRandomTLSExtensionOrder { get; set; }
}

public class CustomTlsClient
{
    [JsonProperty("certCompressionAlgos")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("certCompressionAlgos")]
#endif
    public List<string> CertCompressionAlgos { get; set; }

    [JsonProperty("connectionFlow")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("connectionFlow")]
#endif
    public uint ConnectionFlow { get; set; }

    [JsonProperty("recordSizeLimit")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("recordSizeLimit")]
#endif
    public ushort RecordSizeLimit { get; set; }

    [JsonProperty("h2Settings")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("h2Settings")]
#endif
    public Dictionary<string, uint> H2Settings { get; set; }

    [JsonProperty("h2SettingsOrder")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("h2SettingsOrder")]
#endif
    public List<string> H2SettingsOrder { get; set; }

    [JsonProperty("headerPriority")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("headerPriority")]
#endif
    public PriorityParam HeaderPriority { get; set; }

    [JsonProperty("ja3String")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("ja3String")]
#endif
    public string Ja3String { get; set; }

    [JsonProperty("keyShareCurves")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("keyShareCurves")]
#endif
    public List<string> KeyShareCurves { get; set; }

    [JsonProperty("alpnProtocols")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("alpnProtocols")]
#endif
    public List<string> ALPNProtocols { get; set; }

    [JsonProperty("alpsProtocols")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("alpsProtocols")]
#endif
    public List<string> ALPSProtocols { get; set; }

    [JsonProperty("ECHCandidatePayloads")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("ECHCandidatePayloads")]
#endif
    public List<ushort> ECHCandidatePayloads { get; set; }

    [JsonProperty("ECHCandidateCipherSuites")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("ECHCandidateCipherSuites")]
#endif
    public CandidateCipherSuites ECHCandidateCipherSuites { get; set; }

    [JsonProperty("priorityFrames")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("priorityFrames")]
#endif
    public List<PriorityFrames> PriorityFrames { get; set; }

    [JsonProperty("pseudoHeaderOrder")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("pseudoHeaderOrder")]
#endif
    public List<string> PseudoHeaderOrder { get; set; }

    [JsonProperty("supportedDelegatedCredentialsAlgorithms")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedDelegatedCredentialsAlgorithms")]
#endif
    public List<string> SupportedDelegatedCredentialsAlgorithms { get; set; }

    [JsonProperty("supportedSignatureAlgorithms")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedSignatureAlgorithms")]
#endif
    public List<string> SupportedSignatureAlgorithms { get; set; }

    [JsonProperty("supportedVersions")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedVersions")]
#endif
    public List<string> SupportedVersions { get; set; }
}

public class CandidateCipherSuites : List<CandidateCipherSuite> { }

public class CandidateCipherSuite
{
    [JsonProperty("kdfId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("kdfId")]
#endif
    public string KdfId { get; set; }

    [JsonProperty("aeadId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("aeadId")]
#endif
    public string AeadId { get; set; }
}

public class TransportOptions
{
    [JsonProperty("disableKeepAlives")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableKeepAlives")]
#endif
    public bool DisableKeepAlives { get; set; }

    [JsonProperty("disableCompression")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableCompression")]
#endif
    public bool DisableCompression { get; set; }

    [JsonProperty("maxIdleConns")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxIdleConns")]
#endif
    public int MaxIdleConns { get; set; }

    [JsonProperty("maxIdleConnsPerHost")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxIdleConnsPerHost")]
#endif
    public int MaxIdleConnsPerHost { get; set; }

    [JsonProperty("maxConnsPerHost")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxConnsPerHost")]
#endif
    public int MaxConnsPerHost { get; set; }

    [JsonProperty("maxResponseHeaderBytes")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxResponseHeaderBytes")]
#endif
    public long MaxResponseHeaderBytes { get; set; }

    [JsonProperty("writeBufferSize")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("writeBufferSize")]
#endif
    public int WriteBufferSize { get; set; }

    [JsonProperty("readBufferSize")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("readBufferSize")]
#endif
    public int ReadBufferSize { get; set; }

    [JsonProperty("idleConnTimeout")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("idleConnTimeout")]
#endif
    public TimeSpan? IdleConnTimeout { get; set; }
}

public class PriorityFrames
{
    [JsonProperty("priorityParam")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("priorityParam")]
#endif
    public PriorityParam PriorityParam { get; set; }

    [JsonProperty("streamID")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamID")]
#endif
    public uint StreamID { get; set; }
}

public class PriorityParam
{
    [JsonProperty("exclusive")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("exclusive")]
#endif
    public bool Exclusive { get; set; }

    [JsonProperty("streamDep")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamDep")]
#endif
    public uint StreamDep { get; set; }

    [JsonProperty("weight")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("weight")]
#endif
    public byte Weight { get; set; }
}

public class Cookie
{
    [JsonProperty("domain")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("domain")]
#endif
    public string Domain { get; set; }

    [JsonProperty("expires")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("expires")]
#endif
    public long Expires { get; set; }

    [JsonProperty("maxAge")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxAge")]
#endif
    public int MaxAge { get; set; }

    [JsonProperty("name")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("name")]
#endif
    public string Name { get; set; }

    [JsonProperty("path")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("path")]
#endif
    public string Path { get; set; }

    [JsonProperty("value")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("value")]
#endif
    public string Value { get; set; }

    [JsonProperty("secure")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("secure")]
#endif
    public bool Secure { get; set; }

    [JsonProperty("httpOnly")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("httpOnly")]
#endif
    public bool HttpOnly { get; set; }
}

public class Response : ResponseBase
{

    [JsonProperty("body")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("body")]
#endif
    public string Body { get; set; }

    [JsonProperty("cookies")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();

    [JsonProperty("headers")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("headers")]
#endif
    public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

    [JsonProperty("sessionId")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    [JsonProperty("status")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("status")]
#endif
    public int Status { get; set; }

    [JsonProperty("target")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("target")]
#endif
    public string Target { get; set; }

    [JsonProperty("usedProtocol")]
#if NET8_0_OR_GREATER
    [JsonPropertyName("usedProtocol")]
#endif
    public string UsedProtocol { get; set; }
}
