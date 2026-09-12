using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuYao.TlsClient;

#if !NET6_0_OR_GREATER
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif
#if NET8_0_OR_GREATER
using System.Text.Json.Serialization;
#endif
using System;
using System.Collections.Generic;


//请阅读以下 golang 中定义的 struct 信息，并使用 C# 语言及 JSON.NET 库编写出等效的 C# 类型。
//https://github.com/bogdanfinn/tls-client/blob/master/cffi_src/types.go

public class DestroySessionInput
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("sessionId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }
}

public class DestroyOutput : ResponseBase
{

    #if !NET6_0_OR_GREATER
    [JsonProperty("success")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("success")]
#endif
    public bool Success { get; set; }
}

public class AddCookiesToSessionInput
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("cookies")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public List<Cookie> Cookies { get; set; } = new List<Cookie>();

    #if !NET6_0_OR_GREATER
    [JsonProperty("sessionId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("url")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("url")]
#endif
    public string Url { get; set; }
}

public class GetCookiesFromSessionInput
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("sessionId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("url")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("url")]
#endif
    public string Url { get; set; }
}

public class CookiesFromSessionOutput : ResponseBase
{

    #if !NET6_0_OR_GREATER
    [JsonProperty("cookies")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public List<Cookie> Cookies { get; set; }
}

public class RequestInput
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("catchPanics")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("catchPanics")]
#endif
    public bool CatchPanics { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("certificatePinningHosts")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("certificatePinningHosts")]
#endif
    public Dictionary<string, List<string>> CertificatePinningHosts { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("customTlsClient")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("customTlsClient")]
#endif
    public CustomTlsClient CustomTlsClient { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("transportOptions")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("transportOptions")]
#endif
    public TransportOptions TransportOptions { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("followRedirects")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("followRedirects")]
#endif
    public bool FollowRedirects { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("forceHttp1")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("forceHttp1")]
#endif
    public bool ForceHttp1 { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("disableHttp3")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableHttp3")]
#endif
    public bool DisableHttp3 { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("disableSessionTickets")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableSessionTickets")]
#endif
    public bool DisableSessionTickets { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("withProtocolRacing")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("withProtocolRacing")]
#endif
    public bool WithProtocolRacing { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("headerOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("headerOrder")]
#endif
    public List<string> HeaderOrder { get; set; } = new List<string>();

    #if !NET6_0_OR_GREATER
    [JsonProperty("headers")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("headers")]
#endif
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    #if !NET6_0_OR_GREATER
    [JsonProperty("defaultHeaders")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("defaultHeaders")]
#endif
    public Dictionary<string, List<string>> DefaultHeaders { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("connectHeaders")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("connectHeaders")]
#endif
    public Dictionary<string, List<string>> ConnectHeaders { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("insecureSkipVerify")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("insecureSkipVerify")]
#endif
    public bool InsecureSkipVerify { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("isByteRequest")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("isByteRequest")]
#endif
    public bool IsByteRequest { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("isByteResponse")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("isByteResponse")]
#endif
    public bool IsByteResponse { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("isRotatingProxy")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("isRotatingProxy")]
#endif
    public bool IsRotatingProxy { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("disableIPV6")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableIPV6")]
#endif
    public bool DisableIPV6 { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("disableIPV4")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableIPV4")]
#endif
    public bool DisableIPV4 { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("localAddress")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("localAddress")]
#endif
    public string? LocalAddress { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("serverNameOverwrite")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("serverNameOverwrite")]
#endif
    public string ServerNameOverwrite { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("proxyUrl")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("proxyUrl")]
#endif
    public string? ProxyUrl { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("requestBody")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestBody")]
#endif
    public string RequestBody { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("requestCookies")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestCookies")]
#endif
    public List<Cookie> RequestCookies { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("requestMethod")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestMethod")]
#endif
    public string RequestMethod { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("requestUrl")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestUrl")]
#endif
    public string RequestUrl { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("requestHostOverride")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("requestHostOverride")]
#endif
    public string RequestHostOverride { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("sessionId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamOutputBlockSize")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputBlockSize")]
#endif
    public int? StreamOutputBlockSize { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamOutputEOFSymbol")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputEOFSymbol")]
#endif
    public string? StreamOutputEOFSymbol { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamOutputPath")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamOutputPath")]
#endif
    public string? StreamOutputPath { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("timeoutMilliseconds")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("timeoutMilliseconds")]
#endif
    public int TimeoutMilliseconds { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("timeoutSeconds")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("timeoutSeconds")]
#endif
    public int TimeoutSeconds { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("tlsClientIdentifier")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("tlsClientIdentifier")]
#endif
    public string TLSClientIdentifier { get; set; } = ClientIdentifiers.Default;

    #if !NET6_0_OR_GREATER
    [JsonProperty("withDebug")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("withDebug")]
#endif
    public bool WithDebug { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("withCustomCookieJar")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("withCustomCookieJar")]
#endif
    public bool WithCustomCookieJar { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("withoutCookieJar")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("withoutCookieJar")]
#endif
    public bool WithoutCookieJar { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("withRandomTLSExtensionOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("withRandomTLSExtensionOrder")]
#endif
    public bool WithRandomTLSExtensionOrder { get; set; }
}

public class CustomTlsClient
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("certCompressionAlgos")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("certCompressionAlgos")]
#endif
    public List<string> CertCompressionAlgos { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("connectionFlow")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("connectionFlow")]
#endif
    public uint ConnectionFlow { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("recordSizeLimit")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("recordSizeLimit")]
#endif
    public ushort RecordSizeLimit { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamId")]
#endif
    public uint StreamId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h3PriorityParam")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h3PriorityParam")]
#endif
    public uint H3PriorityParam { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h3SendGreaseFrames")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h3SendGreaseFrames")]
#endif
    public bool H3SendGreaseFrames { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("allowHttp")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("allowHttp")]
#endif
    public bool AllowHttp { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h2Settings")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h2Settings")]
#endif
    public Dictionary<string, uint> H2Settings { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h2SettingsOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h2SettingsOrder")]
#endif
    public List<string> H2SettingsOrder { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h3Settings")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h3Settings")]
#endif
    public Dictionary<string, ulong> H3Settings { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h3SettingsOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h3SettingsOrder")]
#endif
    public List<string> H3SettingsOrder { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("h3PseudoHeaderOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("h3PseudoHeaderOrder")]
#endif
    public List<string> H3PseudoHeaderOrder { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("headerPriority")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("headerPriority")]
#endif
    public PriorityParam HeaderPriority { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("ja3String")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("ja3String")]
#endif
    public string Ja3String { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("trustAnchorsPayload")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("trustAnchorsPayload")]
#endif
    public string TrustAnchorsPayload { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("keyShareCurves")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("keyShareCurves")]
#endif
    public List<string> KeyShareCurves { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("alpnProtocols")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("alpnProtocols")]
#endif
    public List<string> ALPNProtocols { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("alpsProtocols")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("alpsProtocols")]
#endif
    public List<string> ALPSProtocols { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("ECHCandidatePayloads")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("ECHCandidatePayloads")]
#endif
    public List<ushort> ECHCandidatePayloads { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("ECHCandidateCipherSuites")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("ECHCandidateCipherSuites")]
#endif
    public CandidateCipherSuites ECHCandidateCipherSuites { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("priorityFrames")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("priorityFrames")]
#endif
    public List<PriorityFrames> PriorityFrames { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("pseudoHeaderOrder")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("pseudoHeaderOrder")]
#endif
    public List<string> PseudoHeaderOrder { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("supportedDelegatedCredentialsAlgorithms")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedDelegatedCredentialsAlgorithms")]
#endif
    public List<string> SupportedDelegatedCredentialsAlgorithms { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("supportedSignatureAlgorithms")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedSignatureAlgorithms")]
#endif
    public List<string> SupportedSignatureAlgorithms { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("supportedVersions")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("supportedVersions")]
#endif
    public List<string> SupportedVersions { get; set; }
}

public class CandidateCipherSuites : List<CandidateCipherSuite> { }

public class CandidateCipherSuite
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("kdfId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("kdfId")]
#endif
    public string KdfId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("aeadId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("aeadId")]
#endif
    public string AeadId { get; set; }
}

public class TransportOptions
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("disableKeepAlives")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableKeepAlives")]
#endif
    public bool DisableKeepAlives { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("disableCompression")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("disableCompression")]
#endif
    public bool DisableCompression { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("maxIdleConns")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxIdleConns")]
#endif
    public int MaxIdleConns { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("maxIdleConnsPerHost")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxIdleConnsPerHost")]
#endif
    public int MaxIdleConnsPerHost { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("maxConnsPerHost")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxConnsPerHost")]
#endif
    public int MaxConnsPerHost { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("maxResponseHeaderBytes")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxResponseHeaderBytes")]
#endif
    public long MaxResponseHeaderBytes { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("writeBufferSize")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("writeBufferSize")]
#endif
    public int WriteBufferSize { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("readBufferSize")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("readBufferSize")]
#endif
    public int ReadBufferSize { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("idleConnTimeout")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("idleConnTimeout")]
#endif
    public TimeSpan? IdleConnTimeout { get; set; }
}

public class PriorityFrames
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("priorityParam")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("priorityParam")]
#endif
    public PriorityParam PriorityParam { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamID")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamID")]
#endif
    public uint StreamID { get; set; }
}

public class PriorityParam
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("exclusive")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("exclusive")]
#endif
    public bool Exclusive { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("streamDep")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("streamDep")]
#endif
    public uint StreamDep { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("weight")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("weight")]
#endif
    public byte Weight { get; set; }
}

public class Cookie
{
    #if !NET6_0_OR_GREATER
    [JsonProperty("domain")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("domain")]
#endif
    public string Domain { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("expires")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("expires")]
#endif
    public long Expires { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("maxAge")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("maxAge")]
#endif
    public int MaxAge { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("name")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("name")]
#endif
    public string Name { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("path")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("path")]
#endif
    public string Path { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("value")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("value")]
#endif
    public string Value { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("secure")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("secure")]
#endif
    public bool Secure { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("httpOnly")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("httpOnly")]
#endif
    public bool HttpOnly { get; set; }
}

public class Response : ResponseBase
{

    #if !NET6_0_OR_GREATER
    [JsonProperty("body")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("body")]
#endif
    public string Body { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("cookies")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("cookies")]
#endif
    public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();

    #if !NET6_0_OR_GREATER
    [JsonProperty("headers")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("headers")]
#endif
    public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

    #if !NET6_0_OR_GREATER
    [JsonProperty("sessionId")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("sessionId")]
#endif
    public string SessionId { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("status")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("status")]
#endif
    public int Status { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("target")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("target")]
#endif
    public string Target { get; set; }

    #if !NET6_0_OR_GREATER
    [JsonProperty("usedProtocol")]
    #endif
#if NET8_0_OR_GREATER
    [JsonPropertyName("usedProtocol")]
#endif
    public string UsedProtocol { get; set; }
}
