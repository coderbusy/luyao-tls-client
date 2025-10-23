# AOT Sample Application

This is a sample .NET 8 console application demonstrating the use of **LuYao.TlsClient** with Native AOT (Ahead-of-Time) compilation.

## What is Native AOT?

Native AOT compiles your .NET application to native machine code ahead of time, resulting in:
- Faster startup time
- Smaller memory footprint  
- No .NET runtime dependency
- Self-contained native executable

## Prerequisites

- .NET 8.0 SDK or later
- Supported OS: Windows, Linux, or macOS

## Building and Running

### Standard Build (JIT)

```bash
# Build
dotnet build

# Run
dotnet run

# Run with verbose output
dotnet run -- https://www.google.com -v
```

### Native AOT Build

```bash
# Publish as native executable (Windows x64)
dotnet publish -c Release -r win-x64

# Publish as native executable (Linux x64)
dotnet publish -c Release -r linux-x64

# Publish as native executable (macOS ARM64 - Apple Silicon)
dotnet publish -c Release -r osx-arm64
```

The native executable will be in `bin/Release/net8.0/{runtime-id}/publish/`.

### Running the Native Executable

```bash
# Windows
./bin/Release/net8.0/win-x64/publish/AotSample.exe

# Linux/macOS
./bin/Release/net8.0/linux-x64/publish/AotSample

# With custom URL
./bin/Release/net8.0/linux-x64/publish/AotSample https://www.github.com

# With verbose output
./bin/Release/net8.0/linux-x64/publish/AotSample https://www.github.com -v
```

## Usage

```bash
AotSample [url] [options]

Arguments:
  url                Target URL (default: https://www.example.com)

Options:
  -v, --verbose      Show detailed output including headers and cookies
```

## Features Demonstrated

1. **Basic HTTP Request**: Simple GET request with TLS fingerprinting
2. **Chrome 124 Fingerprint**: Mimics Chrome 124 browser TLS signature
3. **Response Handling**: Displays status, headers, cookies, and body
4. **Cookie Session Management**: Shows session-based cookie handling
5. **Error Handling**: Graceful error handling with user-friendly messages
6. **AOT Compatibility**: Uses System.Text.Json with source generation

## Code Highlights

### TLS Client Configuration

```csharp
using var client = new TlsClient
{
    TLSClientIdentifier = ClientIdentifiers.Chrome_124,
    FollowRedirect = true,
    Timeout = TimeSpan.FromSeconds(30)
};
```

### Request Creation and Execution

```csharp
var request = client.CreateRequest();
request.RequestUrl = "https://example.com";
request.RequestMethod = "GET";
request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

var response = client.Request(request);
```

### AOT-Specific Configuration

The project file includes:

```xml
<PublishAot>true</PublishAot>
<PublishTrimmed>true</PublishTrimmed>
<IlcOptimizationPreference>Size</IlcOptimizationPreference>
```

## Performance Comparison

Typical performance characteristics (may vary by platform):

| Metric | JIT (dotnet run) | Native AOT |
|--------|------------------|------------|
| Startup Time | ~500ms | ~50ms |
| Memory Usage | ~60MB | ~20MB |
| Executable Size | Runtime + App | ~10-15MB |
| First Request | Similar | Similar |

## Troubleshooting

### Issue: Native library not found

Make sure the native TLS client libraries are in the output directory. They should be copied automatically from the NuGet package.

### Issue: Build fails with AOT warnings

Check that all serialized types are registered in `TlsClientJsonContext`. The library handles all built-in types automatically.

### Issue: Runtime error about missing types

This usually indicates a trimming issue. The library is marked as trim-safe, but custom extensions might need trim annotations.

## Learn More

- [AOT Support Guide](../../AOT-SUPPORT.md) - Complete guide to AOT with LuYao.TlsClient
- [Main README](../../README.md) - General library documentation
- [.NET Native AOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/) - Official Microsoft documentation

## License

This sample code is provided as-is under the same license as the LuYao.TlsClient library.
