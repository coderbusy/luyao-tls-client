using Newtonsoft.Json;

namespace LuYao.TlsClient;

public class ResponseBase
{
    [JsonProperty("id")]
    public string? Id { get; set; }
}
