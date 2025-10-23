#if !NET6_0_OR_GREATER
using Newtonsoft.Json;
#else
using System.Text.Json.Serialization;
#endif

namespace LuYao.TlsClient;

public class ResponseBase
{
#if !NET6_0_OR_GREATER
    [JsonProperty("id")]
#else
    [JsonPropertyName("id")]
#endif
    public string? Id { get; set; }
}
