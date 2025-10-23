# LuYao.TlsClient

一个基于 [bogdanfinn/tls-client](https://github.com/bogdanfinn/tls-client) 的 .NET TLS 客户端库，通过原生互操作 (P/Invoke) 调用 Go 编译的动态链接库，提供高级 TLS 指纹模拟功能。

## 项目简介

LuYao.TlsClient 是一个跨平台的 .NET 库，允许开发者在 .NET 应用程序中使用高级 TLS 客户端功能。该库封装了 tls-client Go 库的功能，可以模拟各种浏览器和客户端的 TLS 指纹，绕过某些基于 TLS 指纹的反爬虫机制。

### 主要特性

- **多浏览器 TLS 指纹模拟**：支持模拟 Chrome、Firefox、Safari、Opera 等多种浏览器的不同版本
- **跨平台支持**：支持 Windows (x86/x64)、Linux (x64/ARM64)、macOS (x64/ARM64)、Alpine Linux
- **多 .NET 版本兼容**：支持 .NET Framework 4.5/4.6.1、.NET Standard 2.0/2.1、.NET 6/7/8
- **Native AOT 支持**：.NET 8+ 应用可使用 Native AOT 编译，获得更快的启动速度和更小的内存占用 🚀
- **HttpClient 集成**：提供 `TlsClientHttpMessageHandler`，可无缝集成到标准 .NET HttpClient 工作流
- **会话管理**：支持 Cookie 会话管理、自定义代理、重定向控制等
- **灵活配置**：支持自定义 TLS 配置、HTTP 头顺序、超时设置等

## 使用方式

### 安装

通过 NuGet 包管理器安装：

```bash
dotnet add package LuYao.TlsClient
```

或在项目文件中添加：

```xml
<PackageReference Include="LuYao.TlsClient" Version="*" />
```

### 基本使用示例

#### 1. 直接使用 TlsClient

```csharp
using LuYao.TlsClient;

// 创建 TLS 客户端实例
using var client = new TlsClient();

// 配置客户端
client.TLSClientIdentifier = ClientIdentifiers.Chrome_124;  // 模拟 Chrome 124
client.Proxy = "http://proxy.example.com:8080";             // 可选：设置代理
client.FollowRedirect = true;                               // 自动跟随重定向
client.Timeout = TimeSpan.FromSeconds(30);                  // 设置超时

// 创建请求
var request = client.CreateRequest();
request.RequestUrl = "https://example.com";
request.RequestMethod = "GET";
request.Headers["User-Agent"] = "Mozilla/5.0...";

// 发送请求
var response = client.Request(request);

// 处理响应
Console.WriteLine($"状态码: {response.Status}");
Console.WriteLine($"响应体: {response.Body}");
```

#### 2. 与 HttpClient 集成使用

```csharp
using System.Net.Http;
using LuYao.TlsClient;

// 创建 TLS 客户端
var tlsClient = new TlsClient
{
    TLSClientIdentifier = ClientIdentifiers.Chrome_124,
    FollowRedirect = true,
    Timeout = TimeSpan.FromSeconds(30)
};

// 使用 TlsClientHttpMessageHandler 创建 HttpClient
using var httpClient = new HttpClient(new TlsClientHttpMessageHandler(tlsClient));

// 像使用普通 HttpClient 一样使用
var response = await httpClient.GetAsync("https://example.com");
var content = await response.Content.ReadAsStringAsync();

Console.WriteLine(content);
```

#### 3. Cookie 会话管理

```csharp
using var client = new TlsClient();

// 添加 Cookies
var addCookiesInput = new AddCookiesToSessionInput
{
    SessionId = client.SessionId,
    Url = "https://example.com",
    Cookies = new List<Cookie>
    {
        new Cookie 
        { 
            Name = "session_id", 
            Value = "abc123",
            Domain = "example.com",
            Path = "/"
        }
    }
};
client.AddCookiesToSession(addCookiesInput);

// 获取 Cookies
var getCookiesInput = new GetCookiesFromSessionInput
{
    SessionId = client.SessionId,
    Url = "https://example.com"
};
var cookies = client.GetCookiesFromSession(getCookiesInput);

foreach (var cookie in cookies.Cookies)
{
    Console.WriteLine($"{cookie.Name}: {cookie.Value}");
}
```

### 支持的浏览器标识

库提供了多种预定义的浏览器 TLS 指纹标识（通过 `ClientIdentifiers` 类）：

- **Chrome**: Chrome_103 至 Chrome_124
- **Firefox**: Firefox_102 至 Firefox_110
- **Safari**: Safari_15_6_1, Safari_16_0, Safari_IOS_15_5/15_6/16_0 等
- **Opera**: Opera_89 至 Opera_91
- **移动应用**: Nike、Zalando、Mesh、Confirmed 等特定应用的指纹
- **Android OkHttp**: Okhttp4_Android_7 至 Okhttp4_Android_13

## 软件架构

### 架构概述

```
┌─────────────────────────────────────┐
│    .NET 应用程序                     │
│                                     │
│  ┌──────────────────────────────┐  │
│  │   HttpClient (可选)           │  │
│  └──────────┬───────────────────┘  │
│             │                       │
│  ┌──────────▼───────────────────┐  │
│  │ TlsClientHttpMessageHandler  │  │
│  └──────────┬───────────────────┘  │
│             │                       │
│  ┌──────────▼───────────────────┐  │
│  │      TlsClient               │  │
│  │  - 会话管理                   │  │
│  │  - 配置选项                   │  │
│  │  - JSON 序列化               │  │
│  └──────────┬───────────────────┘  │
│             │                       │
│  ┌──────────▼───────────────────┐  │
│  │    NativeMethods             │  │
│  │  - P/Invoke 调用             │  │
│  │  - 内存封送处理               │  │
│  └──────────┬───────────────────┘  │
└─────────────┼───────────────────────┘
              │ P/Invoke / FFI
┌─────────────▼───────────────────────┐
│   原生动态链接库 (Go 编译)           │
│   tls-client.dll / libtls-client.so │
│                                     │
│  - TLS 握手处理                     │
│  - 浏览器指纹模拟                    │
│  - HTTP/HTTPS 请求处理              │
└─────────────────────────────────────┘
```

### 核心组件说明

#### 1. TlsClient 类
- **职责**: 主要客户端类，管理 TLS 会话和请求
- **功能**:
  - 会话生命周期管理
  - 请求配置和执行
  - Cookie 管理
  - JSON 序列化/反序列化

#### 2. TlsClientHttpMessageHandler 类
- **职责**: HttpMessageHandler 实现，用于与标准 HttpClient 集成
- **功能**:
  - 将 HttpRequestMessage 转换为 RequestInput
  - 将 Response 转换为 HttpResponseMessage
  - 支持流式输出和临时文件处理

#### 3. NativeMethods 类
- **职责**: P/Invoke 声明，调用原生 Go 库
- **方法**:
  - `Request`: 发送 HTTP/HTTPS 请求
  - `DestroySession`: 销毁指定会话
  - `DestroyAll`: 销毁所有会话
  - `GetCookiesFromSession`: 获取会话 Cookies
  - `AddCookiesToSession`: 添加 Cookies 到会话
  - `FreeMemory`: 释放原生内存

#### 4. Types 类
- **职责**: 定义与 Go 库交互的数据类型
- **主要类型**:
  - `RequestInput`: 请求参数
  - `Response`: 响应数据
  - `Cookie`: Cookie 信息
  - `CustomTlsClient`: 自定义 TLS 配置
  - `TransportOptions`: 传输选项

#### 5. ClientIdentifiers 类
- **职责**: 提供预定义的浏览器 TLS 指纹标识符
- **包含**: 50+ 种不同浏览器和应用的 TLS 指纹

### 平台支持

该库通过运行时标识符 (RID) 支持多平台：

| 平台 | 架构 | RID | 原生库格式 |
|------|------|-----|-----------|
| Windows | x86 | win-x86 | tls-client.dll |
| Windows | x64 | win-x64 | tls-client.dll |
| Linux | x64 | linux-x64 | libtls-client.so |
| Linux | ARM64 | linux-arm64 | libtls-client.so |
| Alpine Linux | x64 | alpine-x64 | libtls-client.so |
| macOS | x64 | osx-x64 | libtls-client.dylib |
| macOS | ARM64 | osx-arm64 | libtls-client.dylib |

## 依赖组件

### NuGet 包依赖

#### 运行时依赖（按框架区分）

**.NET 6.0 / 7.0 / 8.0:**
- **System.Text.Json** (内置)
  - 用途: JSON 序列化和反序列化
  - 特性: 支持 AOT 编译，.NET 8 使用源代码生成器
  - 许可证: MIT

**.NET Framework 4.5/4.6.1 和 .NET Standard 2.0/2.1:**
- **Newtonsoft.Json** (v13.0.3)
  - 用途: JSON 序列化和反序列化
  - 许可证: MIT

#### .NET Framework 特定依赖
- **System.Net.Http** (仅 .NET Framework 4.5 和 4.6.1)
  - 用途: HTTP 客户端功能
  - 来源: .NET Framework 内置

### 原生库依赖

- **tls-client** (Go 编译的动态链接库, v1.9.1)
  - 来源: [bogdanfinn/tls-client](https://github.com/bogdanfinn/tls-client)
  - 用途: 提供核心 TLS 客户端功能
  - 许可证: BSD 3-Clause

### 目标框架

库支持以下 .NET 目标框架：
- .NET Framework 4.5
- .NET Framework 4.6.1
- .NET Standard 2.0
- .NET Standard 2.1
- .NET 6.0
- .NET 7.0
- .NET 8.0

### 构建工具依赖

- **Nuke.Common**: 构建自动化框架
- **GitVersion**: 版本管理
- **.NET SDK**: 用于编译和构建

## 配置选项

### TlsClient 主要属性

| 属性 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| `SessionId` | string | GUID | 会话唯一标识符 |
| `TLSClientIdentifier` | string | Chrome_124 | TLS 指纹标识符 |
| `Proxy` | string | null | 代理服务器地址 |
| `FollowRedirect` | bool | false | 是否自动跟随重定向 |
| `InsecureSkipVerify` | bool | false | 是否跳过 SSL 证书验证 |
| `DisableIPV6` | bool | false | 是否禁用 IPv6 |
| `DisableIPV4` | bool | false | 是否禁用 IPv4 |
| `LocalAddress` | string | null | 本地绑定地址 |
| `StreamOutput` | bool | false | 是否使用流式输出 |
| `Timeout` | TimeSpan | 0 (无限制) | 请求超时时间 |
| `WithDebug` | bool | false | 是否启用调试模式 |
| `ForceHttp1` | bool | false | 是否强制使用 HTTP/1.1 |

### RequestInput 高级选项

- **CertificatePinning**: 证书固定配置
- **CustomTlsClient**: 自定义 TLS 客户端配置
- **TransportOptions**: 传输层选项（拨号超时、空闲连接超时等）
- **HeaderOrder**: 自定义 HTTP 头顺序
- **ProxyRotation**: 代理轮换配置

## 项目结构

```
luyao-tls-client/
├── src/
│   └── LuYao.TlsClient/           # 主项目源代码
│       ├── TlsClient.cs           # 核心客户端类
│       ├── TlsClientHttpMessageHandler.cs  # HttpClient 集成
│       ├── NativeMethods.cs       # P/Invoke 声明
│       ├── Types.cs               # 数据类型定义
│       ├── ClientIdentifiers.cs   # 浏览器标识符
│       ├── CStringMarshaler.cs    # C 字符串封送器
│       └── ...
├── build/                         # 构建脚本和配置
│   ├── Build.cs                   # Nuke 构建定义
│   ├── Consts.cs                  # 常量定义
│   └── ...
├── .github/
│   └── workflows/                 # GitHub Actions 工作流
│       ├── continuous.yml         # 持续集成
│       └── Publish_NuGet_Manual.yml  # NuGet 发布
├── LuYao.TlsClient.sln           # Visual Studio 解决方案
└── README.md                      # 本文档
```

## 构建和发布

### 本地构建

```bash
# Windows
.\build.cmd Compile

# Linux/macOS
./build.sh Compile
```

### 打包 NuGet

```bash
# Windows
.\build.cmd Pack

# Linux/macOS
./build.sh Pack
```

### 发布到 NuGet

项目使用 GitHub Actions 自动化发布流程，通过 `Publish_NuGet_Manual` 工作流手动触发。

## Native AOT 支持

从 .NET 8 开始，本库完全支持 Native AOT 编译！这意味着您可以：

- 🚀 **更快的启动速度**：无需 JIT 编译，启动速度提升 50-80%
- 💾 **更小的内存占用**：内存使用降低 20-40%
- 📦 **自包含部署**：无需安装 .NET 运行时
- ⚡ **原生性能**：编译为平台原生代码

### 快速开始 AOT

在您的 .NET 8 项目中启用 AOT：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <PublishAot>true</PublishAot>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="LuYao.TlsClient" Version="*" />
  </ItemGroup>
</Project>
```

然后发布为原生可执行文件：

```bash
dotnet publish -c Release -r win-x64    # Windows
dotnet publish -c Release -r linux-x64  # Linux
dotnet publish -c Release -r osx-arm64  # macOS (Apple Silicon)
```

📖 **详细文档**：
- [完整 AOT 支持指南](AOT-SUPPORT.md) - 深入了解 AOT 功能和最佳实践
- [AOT 示例应用](samples/AotSample/) - 可运行的完整示例代码

**注意**：AOT 功能仅在 .NET 8.0 及更高版本可用。对于较旧的框架，库将自动使用标准的 JIT 编译。

## 注意事项

1. **法律和道德使用**: 此库应仅用于合法目的。使用此库绕过网站保护可能违反服务条款或当地法律。
2. **原生库依赖**: 需要确保目标平台的原生动态链接库可用。
3. **内存管理**: 库实现了 `IDisposable`，使用完毕后应正确释放资源。
4. **线程安全**: 每个 `TlsClient` 实例维护独立的会话，建议每个线程使用独立实例。

## 许可证

本项目基于 [bogdanfinn/tls-client](https://github.com/bogdanfinn/tls-client) 构建，请遵循相应的开源许可证。

## 相关链接

- [源代码仓库](https://github.com/coderbusy/luyao-tls-client)
- [bogdanfinn/tls-client](https://github.com/bogdanfinn/tls-client) - 底层 Go TLS 客户端库
- [NuGet 包](https://www.nuget.org/packages/LuYao.TlsClient/)

## 贡献

欢迎提交 Issue 和 Pull Request 来改进这个项目。

---

**最后更新**: 2025-10-23