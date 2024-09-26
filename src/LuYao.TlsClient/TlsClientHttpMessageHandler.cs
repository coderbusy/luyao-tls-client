using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LuYao.TlsClient;

public class TlsClientHttpMessageHandler : HttpMessageHandler
{
    public TlsClient TlsClient { get; }

    public TlsClientHttpMessageHandler(TlsClient tlsClient)
    {
        TlsClient = tlsClient ?? throw new ArgumentNullException(nameof(tlsClient));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestInput input = await CreateRequest(request);
        Response output = this.TlsClient.Request(input);
        if (output.Status == 0) throw new ArgumentException(output.Body);
        HttpResponseMessage response = CreateResponse(input, output);
        return response;
    }
    protected virtual HttpResponseMessage CreateResponse(RequestInput input, Response output)
    {
        var response = new HttpResponseMessage((HttpStatusCode)output.Status);

        foreach (var h in output.Headers)
        {
            var k = h.Key;
            response.Headers.TryAddWithoutValidation(k, h.Value);
        }

        if (!string.IsNullOrWhiteSpace(output.Body))
        {
            var bytes = output.ReadBodyAsBase64(out var type);
            if (!string.IsNullOrWhiteSpace(input.StreamOutputPath) && File.Exists(input.StreamOutputPath))
            {
                bytes = File.ReadAllBytes(input.StreamOutputPath);
                File.Delete(input.StreamOutputPath);
            }
            var content = new ByteArrayContent(bytes);
            response.Content = content;
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(type);
            foreach (var h in output.Headers)
            {
                var k = h.Key;
                content.Headers.TryAddWithoutValidation(k, h.Value);
            }
        }

        return response;
    }


    private void ReadHeaders(HttpHeaders headers, RequestInput input)
    {
        if (headers is null || headers.Count() == 0) return;
        var str = headers.ToString();
        var lines = str.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++) lines[i] = lines[i].Trim();
        foreach (var h in headers)
        {
            input.HeaderOrder.Add(h.Key);
            var prefix = h.Key + ":";
            foreach (var line in lines)
            {
                if (line.StartsWith(prefix))
                {
                    var v = line.Substring(prefix.Length);
                    input.Headers[h.Key] = v;
                    break;
                }
            }
        }
    }

    protected virtual async Task<RequestInput> CreateRequest(HttpRequestMessage request)
    {
        var input = this.TlsClient.CreateRequest();
        input.RequestMethod = request.Method.Method;
        if (request.RequestUri != null) input.RequestUrl = request.RequestUri.ToString();
        input.IsByteRequest = true;
        input.IsByteResponse = true;
        ReadHeaders(request.Headers, input);
        if (request.Content != null)
        {
            ReadHeaders(request.Content.Headers, input);
            var bytes = await request.Content.ReadAsByteArrayAsync();
            input.RequestBody = Convert.ToBase64String(bytes);
        }

        return input;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            this.TlsClient.Dispose();
        }
    }
}
