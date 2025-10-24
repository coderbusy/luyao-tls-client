using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
#if !NET6_0_OR_GREATER
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace LuYao.TlsClient;

/// <summary>
/// Helper class for marshaling strings between managed and native code.
/// Provides AOT-compatible manual marshaling instead of ICustomMarshaler.
/// </summary>
internal static class CStringMarshaler
{
    /// <summary>
    /// Marshals a managed string to a native UTF-8 C-string.
    /// The caller is responsible for freeing the returned pointer using Marshal.FreeHGlobal.
    /// </summary>
    public static IntPtr ManagedToNative(string? managedString)
    {
        if (string.IsNullOrEmpty(managedString))
        {
            return IntPtr.Zero;
        }

        var utf8Bytes = Encoding.UTF8.GetBytes(managedString);
        var ptr = Marshal.AllocHGlobal(utf8Bytes.Length + 1);
        Marshal.Copy(utf8Bytes, 0, ptr, utf8Bytes.Length);
        Marshal.WriteByte(ptr, utf8Bytes.Length, 0); // Null terminator
        return ptr;
    }

#if !NET6_0_OR_GREATER
    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Error = static (sender, args) => args.ErrorContext.Handled = true,
        NullValueHandling = NullValueHandling.Ignore
    };
#endif

#if NET5_0_OR_GREATER
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "This is a fallback path for ResponseBase parsing. Primary types use source generation in TlsClient.")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "This is a fallback path for ResponseBase parsing. Primary types use source generation in TlsClient.")]
#endif
    private static ResponseBase? TryDeserializeResponseBase(string str)
    {
#if !NET6_0_OR_GREATER
        return JsonConvert.DeserializeObject<ResponseBase>(str, settings);
#elif NET8_0_OR_GREATER
        try
        {
            // Try to use source-generated deserialization first
            return JsonSerializer.Deserialize(str, TlsClientJsonContext.Default.ResponseBase);
        }
        catch
        {
            // Ignore deserialization errors - this is just for memory cleanup
            return null;
        }
#else
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<ResponseBase>(str, options);
        }
        catch
        {
            // Ignore deserialization errors - this is just for memory cleanup
            return null;
        }
#endif
    }

    /// <summary>
    /// Marshals a native UTF-8 C-string to a managed string.
    /// Also handles automatic memory cleanup for response IDs.
    /// </summary>
    public static string NativeToManaged(IntPtr pNativeData)
    {
        if (pNativeData == IntPtr.Zero)
        {
            return string.Empty;
        }

        // Read the null-terminated UTF-8 string
        var bytes = new List<byte>();
        for (var offset = 0; ; offset++)
        {
            var b = Marshal.ReadByte(pNativeData, offset);
            if (b == 0) break;
            bytes.Add(b);
        }
        
        var str = Encoding.UTF8.GetString(bytes.ToArray());
        
        // Handle automatic memory cleanup for responses with an ID
        if (str.StartsWith("{") && str.EndsWith("}") && str.Contains("\"id\""))
        {
            var response = TryDeserializeResponseBase(str);
            if (response != null && !string.IsNullOrWhiteSpace(response.Id))
            {
                NativeMethods.FreeMemory(response.Id);
            }
        }
        
        return str;
    }
}
