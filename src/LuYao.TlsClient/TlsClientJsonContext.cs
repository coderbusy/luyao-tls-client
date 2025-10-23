#if NET8_0_OR_GREATER
using System.Text.Json.Serialization;

namespace LuYao.TlsClient;

/// <summary>
/// JSON serialization context for AOT support using System.Text.Json source generation.
/// This context includes all types used for serialization in the TLS client.
/// </summary>
[JsonSerializable(typeof(RequestInput))]
[JsonSerializable(typeof(Response))]
[JsonSerializable(typeof(DestroySessionInput))]
[JsonSerializable(typeof(DestroyOutput))]
[JsonSerializable(typeof(AddCookiesToSessionInput))]
[JsonSerializable(typeof(GetCookiesFromSessionInput))]
[JsonSerializable(typeof(CookiesFromSessionOutput))]
[JsonSerializable(typeof(Cookie))]
[JsonSerializable(typeof(CustomTlsClient))]
[JsonSerializable(typeof(TransportOptions))]
[JsonSerializable(typeof(PriorityFrames))]
[JsonSerializable(typeof(PriorityParam))]
[JsonSerializable(typeof(CandidateCipherSuite))]
[JsonSourceGenerationOptions(
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class TlsClientJsonContext : JsonSerializerContext
{
}
#endif
