# AOT (Ahead-of-Time) Support Guide

## Overview

Starting from version 1.0, **LuYao.TlsClient** supports Native AOT compilation for .NET 8 applications. AOT compilation offers several benefits:

- **Faster startup time**: No JIT compilation needed at runtime
- **Smaller memory footprint**: Reduced memory usage for your application
- **Single-file deployment**: Everything compiled into a native executable
- **Better performance**: Optimized native code
- **No .NET runtime required**: Deploy standalone executables

## Requirements

- **.NET 8.0 SDK or later**: AOT features require .NET 8+
- **Supported platforms**: Windows (x64), Linux (x64, ARM64), macOS (x64, ARM64)

## How It Works

LuYao.TlsClient achieves AOT compatibility through:

1. **System.Text.Json with Source Generation**: For .NET 8+, the library uses System.Text.Json with compile-time source generation instead of reflection-based Newtonsoft.Json
2. **P/Invoke Native Interop**: Already AOT-compatible (no changes needed)
3. **Conditional Compilation**: Backward compatibility maintained with Newtonsoft.Json for older frameworks

### Automatic Serialization Selection

The library automatically selects the best JSON serializer based on your target framework:

- **For .NET 8+**: System.Text.Json with source generation (AOT-compatible)
- **For .NET 6/7**: System.Text.Json (AOT-compatible, partial source generation support)
- **For older frameworks (.NET Framework, .NET Standard)**: Newtonsoft.Json

This selection happens at **compile time** through conditional compilation, ensuring zero overhead and optimal performance for each framework.

## Usage

### Basic AOT Application

Here's a complete example of using LuYao.TlsClient in an AOT-compiled application:

```csharp
using LuYao.TlsClient;

var client = new TlsClient
{
    TLSClientIdentifier = ClientIdentifiers.Chrome_124,
    FollowRedirect = true,
    Timeout = TimeSpan.FromSeconds(30)
};

var request = client.CreateRequest();
request.RequestUrl = "https://example.com";
request.RequestMethod = "GET";

var response = client.Request(request);
Console.WriteLine($"Status: {response.Status}");
Console.WriteLine($"Body: {response.Body}");

client.Dispose();
```

### Project Configuration for AOT

Create or update your project file (`.csproj`) to enable AOT:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    
    <!-- Enable Native AOT -->
    <PublishAot>true</PublishAot>
    
    <!-- Optional: Enable invariant globalization for smaller output -->
    <InvariantGlobalization>true</InvariantGlobalization>
    
    <!-- Optional: Trim unused code -->
    <PublishTrimmed>true</PublishTrimmed>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="LuYao.TlsClient" Version="*" />
  </ItemGroup>

</Project>
```

### Publishing as Native AOT

To publish your application as a native executable:

```bash
# Windows x64
dotnet publish -c Release -r win-x64

# Linux x64
dotnet publish -c Release -r linux-x64

# Linux ARM64
dotnet publish -c Release -r linux-arm64

# macOS x64
dotnet publish -c Release -r osx-x64

# macOS ARM64 (Apple Silicon)
dotnet publish -c Release -r osx-arm64
```

The native executable will be in `bin/Release/net8.0/{runtime-id}/publish/`.

### Size Optimization

For smaller executable sizes, add these properties:

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
  <PublishTrimmed>true</PublishTrimmed>
  <InvariantGlobalization>true</InvariantGlobalization>
  <IlcOptimizationPreference>Size</IlcOptimizationPreference>
</PropertyGroup>
```

## JSON Serialization

The library uses different JSON serializers based on your target framework:

### .NET 6/7/8 - System.Text.Json

For modern frameworks, the library automatically uses System.Text.Json. You can configure serialization options:

```csharp
var client = new TlsClient
{
    SystemTextJsonOptions = new System.Text.Json.JsonSerializerOptions
    {
        // Custom System.Text.Json settings
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
    }
};
```

### Older Frameworks - Newtonsoft.Json

For .NET Framework and .NET Standard, the library uses Newtonsoft.Json:

```csharp
var client = new TlsClient
{
    JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
    {
        // Custom Newtonsoft.Json settings
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
    }
};
```

**Note**: The serializer selection happens at **compile time**, not runtime. This ensures optimal performance and eliminates unnecessary dependencies for modern frameworks.

## HttpClient Integration with AOT

The `TlsClientHttpMessageHandler` works seamlessly with AOT:

```csharp
using System.Net.Http;
using LuYao.TlsClient;

var tlsClient = new TlsClient
{
    TLSClientIdentifier = ClientIdentifiers.Chrome_124,
    FollowRedirect = true
};

using var httpClient = new HttpClient(new TlsClientHttpMessageHandler(tlsClient));

var response = await httpClient.GetAsync("https://example.com");
var content = await response.Content.ReadAsStringAsync();

Console.WriteLine(content);
```

## Limitations and Considerations

### 1. Native Library Dependencies

The native TLS client library (`tls-client.dll`, `libtls-client.so`, or `libtls-client.dylib`) must be deployed alongside your AOT application. These files are automatically included when you reference the NuGet package.

### 2. Platform-Specific Builds

Each target platform requires its own AOT compilation. You cannot compile on Windows and run on Linux without separate builds.

### 3. Debugging

AOT-compiled applications have limited debugging capabilities compared to JIT-compiled apps. For development, consider:
- Testing with normal builds first
- Using `PublishAot=false` during development
- Switching to AOT only for production releases

### 4. Custom TLS Configurations

All custom TLS configuration types are pre-registered for AOT serialization. If you extend the library with custom types, you may need to add them to the `TlsClientJsonContext`.

## Backward Compatibility

All existing code continues to work without changes:

- **Older frameworks** (.NET Framework, .NET Standard): Use Newtonsoft.Json automatically
- **.NET 6/7/8**: Use System.Text.Json automatically for better performance and AOT support
- **No breaking changes**: The serializer selection is transparent and happens at compile time

## Troubleshooting

### Issue: "JSON serialization requires unreferenced code"

**Solution**: Make sure you're targeting .NET 8.0 or later. The library uses source generation only for .NET 8+.

### Issue: Native library not found

**Solution**: Ensure the native library files are copied to the output directory. This should happen automatically with the NuGet package.

### Issue: Serialization differences between Newtonsoft.Json and System.Text.Json

**Solution**: The library handles the serialization differences internally. Both serializers use camelCase property naming by default. If you need custom serialization behavior, configure the appropriate options for your target framework:
- **.NET 6+**: Use `SystemTextJsonOptions` property
- **Older frameworks**: Use `JsonSerializerSettings` property

### Issue: Larger executable size than expected

**Solution**: 
1. Use `<IlcOptimizationPreference>Size</IlcOptimizationPreference>`
2. Enable `<InvariantGlobalization>true</InvariantGlobalization>`
3. Consider `<PublishTrimmed>true</PublishTrimmed>`

## Example: Complete AOT Console Application

```csharp
using LuYao.TlsClient;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("LuYao TLS Client - AOT Example");
        
        using var client = new TlsClient
        {
            TLSClientIdentifier = ClientIdentifiers.Chrome_124,
            FollowRedirect = true,
            Timeout = TimeSpan.FromSeconds(30)
        };

        var request = client.CreateRequest();
        request.RequestUrl = args.Length > 0 ? args[0] : "https://www.example.com";
        request.RequestMethod = "GET";
        request.Headers["User-Agent"] = "LuYao-TLS-Client/1.0";

        try
        {
            Console.WriteLine($"Fetching {request.RequestUrl}...");
            var response = client.Request(request);
            
            Console.WriteLine($"Status: {response.Status}");
            Console.WriteLine($"Protocol: {response.UsedProtocol}");
            Console.WriteLine($"Body Length: {response.Body?.Length ?? 0} characters");
            
            if (response.Status == 200)
            {
                Console.WriteLine("\nFirst 500 characters:");
                Console.WriteLine(response.Body?.Substring(0, Math.Min(500, response.Body.Length)));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }
        
        Console.WriteLine("\nDone!");
    }
}
```

Save this as `Program.cs`, then:

```bash
# Create project
dotnet new console -n MyAotApp
cd MyAotApp

# Add LuYao.TlsClient
dotnet add package LuYao.TlsClient

# Update .csproj to enable AOT
# (Add <PublishAot>true</PublishAot> to PropertyGroup)

# Publish as native
dotnet publish -c Release -r win-x64

# Run the native executable
./bin/Release/net8.0/win-x64/publish/MyAotApp.exe
```

## Performance Benchmarks

AOT compilation typically provides:

- **Startup time**: 50-80% faster than JIT
- **Memory usage**: 20-40% lower than JIT
- **Throughput**: Similar to JIT (sometimes slightly better)
- **File size**: Native executable + dependencies (~10-30 MB depending on configuration)

## Additional Resources

- [.NET Native AOT Documentation](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/)
- [System.Text.Json Source Generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)
- [LuYao.TlsClient GitHub Repository](https://github.com/coderbusy/luyao-tls-client)

## Support

If you encounter issues with AOT support:

1. Check that you're using .NET 8.0 or later
2. Review the troubleshooting section above
3. Open an issue on [GitHub](https://github.com/coderbusy/luyao-tls-client/issues)

---

**Note**: AOT support is a powerful feature but adds complexity. For simple applications or when maximum compatibility is needed, standard JIT compilation remains a great choice.
