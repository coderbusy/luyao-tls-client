using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuYao.TlsClient;

internal class TempFileHttpContent : HttpContent
{
    private readonly string _filePath;

    public TempFileHttpContent(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    protected override Task<Stream> CreateContentReadStreamAsync()
    {
        var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return Task.FromResult<Stream>(stream);
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
        {
            await fileStream.CopyToAsync(stream);
        }
    }

    protected override bool TryComputeLength(out long length)
    {
        length = new FileInfo(_filePath).Length;
        return true;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
}
