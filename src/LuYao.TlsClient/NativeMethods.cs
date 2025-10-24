using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LuYao.TlsClient;

public static partial class NativeMethods
{
    /*
    func freeMemory(responseId *C.char){};
    func destroyAll() *C.char{};
    func destroySession(destroySessionParams *C.char) *C.char{}
    func getCookiesFromSession(getCookiesParams *C.char) *C.char{}
    func addCookiesToSession(addCookiesParams *C.char) *C.char{}
    func request(requestParams *C.char) *C.char{}
     */

    // Low-level P/Invoke declarations that return IntPtr for AOT compatibility
    [DllImport(Consts.DllName, EntryPoint = "freeMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern void FreeMemoryNative(IntPtr responseId);

    [DllImport(Consts.DllName, EntryPoint = "destroyAll", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr DestroyAllNative();

    [DllImport(Consts.DllName, EntryPoint = "destroySession", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr DestroySessionNative(IntPtr destroySessionParams);

    [DllImport(Consts.DllName, EntryPoint = "getCookiesFromSession", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr GetCookiesFromSessionNative(IntPtr getCookiesParams);

    [DllImport(Consts.DllName, EntryPoint = "addCookiesToSession", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr AddCookiesToSessionNative(IntPtr addCookiesParams);

    [DllImport(Consts.DllName, EntryPoint = "request", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr RequestNative(IntPtr requestParams);

    // Public wrapper methods that handle marshaling
    public static void FreeMemory(string responseId)
    {
        IntPtr ptr = CStringMarshaler.ManagedToNative(responseId);
        try
        {
            FreeMemoryNative(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    public static string DestroyAll()
    {
        IntPtr resultPtr = DestroyAllNative();
        return CStringMarshaler.NativeToManaged(resultPtr);
    }

    public static string DestroySession(string destroySessionParams)
    {
        IntPtr paramsPtr = CStringMarshaler.ManagedToNative(destroySessionParams);
        try
        {
            IntPtr resultPtr = DestroySessionNative(paramsPtr);
            return CStringMarshaler.NativeToManaged(resultPtr);
        }
        finally
        {
            if (paramsPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(paramsPtr);
            }
        }
    }

    public static string GetCookiesFromSession(string getCookiesParams)
    {
        IntPtr paramsPtr = CStringMarshaler.ManagedToNative(getCookiesParams);
        try
        {
            IntPtr resultPtr = GetCookiesFromSessionNative(paramsPtr);
            return CStringMarshaler.NativeToManaged(resultPtr);
        }
        finally
        {
            if (paramsPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(paramsPtr);
            }
        }
    }

    public static string AddCookiesToSession(string addCookiesParams)
    {
        IntPtr paramsPtr = CStringMarshaler.ManagedToNative(addCookiesParams);
        try
        {
            IntPtr resultPtr = AddCookiesToSessionNative(paramsPtr);
            return CStringMarshaler.NativeToManaged(resultPtr);
        }
        finally
        {
            if (paramsPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(paramsPtr);
            }
        }
    }

    public static string Request(string requestParams)
    {
        IntPtr paramsPtr = CStringMarshaler.ManagedToNative(requestParams);
        try
        {
            IntPtr resultPtr = RequestNative(paramsPtr);
            return CStringMarshaler.NativeToManaged(resultPtr);
        }
        finally
        {
            if (paramsPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(paramsPtr);
            }
        }
    }
}
