
using LuYao.TlsClient;

using var tls = new TlsClient();
using var handler = new TlsClientHttpMessageHandler(tls);
using var client = new HttpClient(handler);
client.DefaultRequestHeaders.Accept.TryParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");

var headers = client.DefaultRequestHeaders;
headers.UserAgent.Clear();
headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

client.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip, deflate");
client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("zh-CN,zh;q=0.9,en;q=0.8,fr;q=0.7,pt;q=0.6,so;q=0.5,de;q=0.4,en-US;q=0.3,ko;q=0.2,ja;q=0.1,zh-TW;q=0.1,und;q=0.1,is;q=0.1");

var html = await client.GetStringAsync("https://www.baidu.com/");
Console.WriteLine(html);
Console.ReadLine();