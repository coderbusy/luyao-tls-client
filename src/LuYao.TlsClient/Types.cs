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


//请阅读以下 golang 中定义的 struct 信息，并使用 C# 语言及 JSON.NET 库编写出等效的 C# 类型。
//https://github.com/bogdanfinn/tls-client/blob/master/cffi_src/types.go

public class DestroySessionInput
{
    [JsonProperty("sessionId")]
    public string SessionId { get; set; }
}

public class DestroyOutput : ResponseBase
{

    [JsonProperty("success")]
    public bool Success { get; set; }
}

public class AddCookiesToSessionInput
{
    [JsonProperty("cookies")]
    public List<Cookie> Cookies { get; set; } = new List<Cookie>();

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}

public class GetCookiesFromSessionInput
{
    [JsonProperty("sessionId")]
    public string SessionId { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}

public class CookiesFromSessionOutput : ResponseBase
{

    [JsonProperty("cookies")]
    public List<Cookie> Cookies { get; set; }
}

public class RequestInput
{
    [JsonProperty("catchPanics")]
    public bool CatchPanics { get; set; }

    [JsonProperty("certificatePinningHosts")]
    public Dictionary<string, List<string>> CertificatePinningHosts { get; set; }

    [JsonProperty("customTlsClient")]
    public CustomTlsClient CustomTlsClient { get; set; }

    [JsonProperty("transportOptions")]
    public TransportOptions TransportOptions { get; set; }

    [JsonProperty("followRedirects")]
    public bool FollowRedirects { get; set; }

    [JsonProperty("forceHttp1")]
    public bool ForceHttp1 { get; set; }

    [JsonProperty("headerOrder")]
    public List<string> HeaderOrder { get; set; } = new List<string>();

    [JsonProperty("headers")]
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    [JsonProperty("defaultHeaders")]
    public Dictionary<string, List<string>> DefaultHeaders { get; set; }

    [JsonProperty("connectHeaders")]
    public Dictionary<string, List<string>> ConnectHeaders { get; set; }

    [JsonProperty("insecureSkipVerify")]
    public bool InsecureSkipVerify { get; set; }

    [JsonProperty("isByteRequest")]
    public bool IsByteRequest { get; set; }

    [JsonProperty("isByteResponse")]
    public bool IsByteResponse { get; set; }

    [JsonProperty("isRotatingProxy")]
    public bool IsRotatingProxy { get; set; }

    [JsonProperty("disableIPV6")]
    public bool DisableIPV6 { get; set; }

    [JsonProperty("disableIPV4")]
    public bool DisableIPV4 { get; set; }

    [JsonProperty("localAddress")]
    public string? LocalAddress { get; set; }

    [JsonProperty("serverNameOverwrite")]
    public string ServerNameOverwrite { get; set; }

    [JsonProperty("proxyUrl")]
    public string? ProxyUrl { get; set; }

    [JsonProperty("requestBody")]
    public string RequestBody { get; set; }

    [JsonProperty("requestCookies")]
    public List<Cookie> RequestCookies { get; set; }

    [JsonProperty("requestMethod")]
    public string RequestMethod { get; set; }

    [JsonProperty("requestUrl")]
    public string RequestUrl { get; set; }

    [JsonProperty("requestHostOverride")]
    public string RequestHostOverride { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }

    [JsonProperty("streamOutputBlockSize")]
    public int? StreamOutputBlockSize { get; set; }

    [JsonProperty("streamOutputEOFSymbol")]
    public string? StreamOutputEOFSymbol { get; set; }

    [JsonProperty("streamOutputPath")]
    public string? StreamOutputPath { get; set; }

    [JsonProperty("timeoutMilliseconds")]
    public int TimeoutMilliseconds { get; set; }

    [JsonProperty("timeoutSeconds")]
    public int TimeoutSeconds { get; set; }

    [JsonProperty("tlsClientIdentifier")]
    public string TLSClientIdentifier { get; set; } = ClientIdentifiers.Default;

    [JsonProperty("withDebug")]
    public bool WithDebug { get; set; }

    [JsonProperty("withDefaultCookieJar")]
    public bool WithDefaultCookieJar { get; set; }

    [JsonProperty("withoutCookieJar")]
    public bool WithoutCookieJar { get; set; }

    [JsonProperty("withRandomTLSExtensionOrder")]
    public bool WithRandomTLSExtensionOrder { get; set; }
}

public class CustomTlsClient
{
    [JsonProperty("certCompressionAlgo")]
    public string CertCompressionAlgo { get; set; }

    [JsonProperty("connectionFlow")]
    public uint ConnectionFlow { get; set; }

    [JsonProperty("h2Settings")]
    public Dictionary<string, uint> H2Settings { get; set; }

    [JsonProperty("h2SettingsOrder")]
    public List<string> H2SettingsOrder { get; set; }

    [JsonProperty("headerPriority")]
    public PriorityParam HeaderPriority { get; set; }

    [JsonProperty("ja3String")]
    public string Ja3String { get; set; }

    [JsonProperty("keyShareCurves")]
    public List<string> KeyShareCurves { get; set; }

    [JsonProperty("alpnProtocols")]
    public List<string> ALPNProtocols { get; set; }

    [JsonProperty("alpsProtocols")]
    public List<string> ALPSProtocols { get; set; }

    [JsonProperty("ECHCandidatePayloads")]
    public List<ushort> ECHCandidatePayloads { get; set; }

    [JsonProperty("ECHCandidateCipherSuites")]
    public CandidateCipherSuites ECHCandidateCipherSuites { get; set; }

    [JsonProperty("priorityFrames")]
    public List<PriorityFrames> PriorityFrames { get; set; }

    [JsonProperty("pseudoHeaderOrder")]
    public List<string> PseudoHeaderOrder { get; set; }

    [JsonProperty("supportedDelegatedCredentialsAlgorithms")]
    public List<string> SupportedDelegatedCredentialsAlgorithms { get; set; }

    [JsonProperty("supportedSignatureAlgorithms")]
    public List<string> SupportedSignatureAlgorithms { get; set; }

    [JsonProperty("supportedVersions")]
    public List<string> SupportedVersions { get; set; }
}

public class CandidateCipherSuites : List<CandidateCipherSuite> { }

public class CandidateCipherSuite
{
    [JsonProperty("kdfId")]
    public string KdfId { get; set; }

    [JsonProperty("aeadId")]
    public string AeadId { get; set; }
}

public class TransportOptions
{
    [JsonProperty("disableKeepAlives")]
    public bool DisableKeepAlives { get; set; }

    [JsonProperty("disableCompression")]
    public bool DisableCompression { get; set; }

    [JsonProperty("maxIdleConns")]
    public int MaxIdleConns { get; set; }

    [JsonProperty("maxIdleConnsPerHost")]
    public int MaxIdleConnsPerHost { get; set; }

    [JsonProperty("maxConnsPerHost")]
    public int MaxConnsPerHost { get; set; }

    [JsonProperty("maxResponseHeaderBytes")]
    public long MaxResponseHeaderBytes { get; set; }

    [JsonProperty("writeBufferSize")]
    public int WriteBufferSize { get; set; }

    [JsonProperty("readBufferSize")]
    public int ReadBufferSize { get; set; }

    [JsonProperty("idleConnTimeout")]
    public TimeSpan? IdleConnTimeout { get; set; }
}

public class PriorityFrames
{
    [JsonProperty("priorityParam")]
    public PriorityParam PriorityParam { get; set; }

    [JsonProperty("streamID")]
    public uint StreamID { get; set; }
}

public class PriorityParam
{
    [JsonProperty("exclusive")]
    public bool Exclusive { get; set; }

    [JsonProperty("streamDep")]
    public uint StreamDep { get; set; }

    [JsonProperty("weight")]
    public byte Weight { get; set; }
}

public class Cookie
{
    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("expires")]
    public long Expires { get; set; }

    [JsonProperty("maxAge")]
    public int MaxAge { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public class Response : ResponseBase
{

    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("cookies")]
    public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();

    [JsonProperty("headers")]
    public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("target")]
    public string Target { get; set; }

    [JsonProperty("usedProtocol")]
    public string UsedProtocol { get; set; }
}
