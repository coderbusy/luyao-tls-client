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

    [DllImport(Consts.DllName, EntryPoint = "freeMemory", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern void FreeMemory(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] string responseId
    );

    [DllImport(Consts.DllName, EntryPoint = "destroyAll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern string DestroyAll();

    [DllImport(Consts.DllName, EntryPoint = "destroySession", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern string DestroySession(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] string destroySessionParams
    );

    [DllImport(Consts.DllName, EntryPoint = "getCookiesFromSession", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern string GetCookiesFromSession(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] string getCookiesParams
    );

    [DllImport(Consts.DllName, EntryPoint = "addCookiesToSession", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern string AddCookiesToSession(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] string addCookiesParams
    );

    [DllImport(Consts.DllName, EntryPoint = "request", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))]
    public static extern string Request(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CStringMarshaler))] string requestParams
    );
}
