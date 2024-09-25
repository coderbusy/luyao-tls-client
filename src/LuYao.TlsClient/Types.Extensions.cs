using System;
using System.Text.RegularExpressions;

namespace LuYao.TlsClient;

public static class Types
{

    public static byte[] ReadBodyAsBase64(this Response response, out string type)
    {
        var m = Regex.Match(
            response.Body,
            "^data:(?<type>.+?);base64,(?<data>.+)?$",
            RegexOptions.IgnoreCase
        );
        if (!m.Success) throw new ArgumentException("格式不正确", nameof(response.Body));
        type = m.Groups["type"].Value;
        var str = m.Groups["data"].Value;
        return Convert.FromBase64String(str);
    }
}
