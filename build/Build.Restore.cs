using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LuYao.TlsClient;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Octokit;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Restore => _ => _
    .Executes(() =>
    {
        DotNetRestore(dotNetRestoreSettings => dotNetRestoreSettings.SetProjectFile(Solution));
    })
    .Executes(DownloadRuntimes);

    private async Task DownloadRuntimes()
    {
        Serilog.Log.Information("DownloadRuntimes");
        var dir = RuntimesDirectory;
        var list = LibItem.List();
        var tag = Consts.RuntimeVersion;
        using var http = new HttpClient();
        var client = new GitHubClient(new ProductHeaderValue("luyao-tls-client"));
        var last = await client.Repository.Release.Get("bogdanfinn", "tls-client", tag);
        var extensions = new List<string> { ".dylib", ".so", ".dll" };
        foreach (var ass in last.Assets)
        {
            Serilog.Log.Information(ass.Name);
            if (ass.Name.Contains("xgo")) continue;
            var ext = Path.GetExtension(ass.Name);
            if (extensions.Contains(ext) == false) continue;
            var map = list.FirstOrDefault(f => ass.Name.Contains(f.Keyword));
            if (map == null) continue;
            //下载至缓存
            var cache = CacheDirectory / "runtimes" / ass.Id.ToString() + ".bin";
            string hash = string.Empty;
            if (!cache.FileExists())
            {
                var bytes = await http.GetByteArrayAsync(ass.BrowserDownloadUrl);
                cache.WriteAllBytes(bytes);
            }
            hash = cache.GetFileHash();
            var lib = dir / map.Directory / "native" / map.Format.Replace("${name}", "tls-client").Replace("${ext}", ext);
            if (!lib.FileExists() || lib.GetFileHash() != hash)
            {
                cache.Copy(lib);
            }
        }
    }
}