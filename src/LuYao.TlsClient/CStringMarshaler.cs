using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LuYao.TlsClient;

internal class CStringMarshaler : ICustomMarshaler
{
    private static readonly CStringMarshaler Instance = new CStringMarshaler();

    [ThreadStatic]
    private static IntPtr lastIntPtr;

    public void CleanUpManagedData(object ManagedObj) { }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (lastIntPtr != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(lastIntPtr);
            lastIntPtr = IntPtr.Zero;
        }
    }

    public int GetNativeDataSize() => -1;

    public IntPtr MarshalManagedToNative(object managedObj)
    {
        if (ReferenceEquals(managedObj, null))
        {
            return IntPtr.Zero;
        }

        if (!(managedObj is string))
        {
            throw new InvalidOperationException();
        }

        var utf8Bytes = Encoding.UTF8.GetBytes(managedObj as string);
        var ptr = Marshal.AllocHGlobal(utf8Bytes.Length + 1);
        Marshal.Copy(utf8Bytes, 0, ptr, utf8Bytes.Length);
        Marshal.WriteByte(ptr, utf8Bytes.Length, 0);
        return lastIntPtr = ptr;
    }
    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Error = static (sender, args) => args.ErrorContext.Handled = true
    };
    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        if (pNativeData == IntPtr.Zero)
        {
            return null;
        }

        var bytes = new List<byte>();
        for (var offset = 0; ; offset++)
        {
            var b = Marshal.ReadByte(pNativeData, offset);
            if (b == 0) break;
            bytes.Add(b);
        }
        var str = Encoding.UTF8.GetString(bytes.ToArray());
        if (str.StartsWith("{") && str.EndsWith("}") && str.Contains("\"id\""))
        {
            var response = JsonConvert.DeserializeObject<ResponseBase>(str, settings);
            if (response != null && !string.IsNullOrWhiteSpace(response.Id))
            {
                NativeMethods.FreeMemory(response.Id);
            }
        }
        return str;
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return Instance;
    }
}
