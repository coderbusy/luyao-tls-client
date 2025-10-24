using LuYao.TlsClient;
using System;
using System.Linq;

namespace AotSample;

/// <summary>
/// Sample application demonstrating LuYao.TlsClient with Native AOT compilation.
/// This application can be compiled to a native executable without requiring the .NET runtime.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("LuYao TLS Client - Native AOT Sample");
        Console.WriteLine("========================================");
        Console.WriteLine();

        // Parse command line arguments
        var url = args.Length > 0 ? args[0] : "https://www.coderbusy.com/";
        var verbose = args.Any(a => a == "-v" || a == "--verbose");

        if (verbose)
        {
            Console.WriteLine($"Target URL: {url}");
            Console.WriteLine($"Using System.Text.Json with source generation for AOT");
            Console.WriteLine();
        }

        // Create TLS client with Chrome 124 fingerprint
        using var client = new TlsClient
        {
            TLSClientIdentifier = ClientIdentifiers.Chrome_124,
            FollowRedirect = true,
            Timeout = TimeSpan.FromSeconds(30),
            WithDebug = verbose
        };

        Console.WriteLine("Creating request...");
        var request = client.CreateRequest();
        request.RequestUrl = url;
        request.RequestMethod = "GET";
        request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";

        try
        {
            Console.WriteLine($"Sending request to {url}...");
            var response = client.Request(request);

            Console.WriteLine();
            Console.WriteLine("Response received:");
            Console.WriteLine($"  Status Code: {response.Status}");
            Console.WriteLine($"  Protocol: {response.UsedProtocol}");
            Console.WriteLine($"  Target: {response.Target}");
            
            if (response.Headers != null && response.Headers.Count > 0)
            {
                Console.WriteLine($"  Headers: {response.Headers.Count} headers received");
                
                if (verbose)
                {
                    Console.WriteLine();
                    Console.WriteLine("Response Headers:");
                    foreach (var header in response.Headers)
                    {
                        Console.WriteLine($"    {header.Key}: {string.Join(", ", header.Value)}");
                    }
                }
            }

            if (response.Cookies != null && response.Cookies.Count > 0)
            {
                Console.WriteLine($"  Cookies: {response.Cookies.Count} cookies received");
                
                if (verbose)
                {
                    Console.WriteLine();
                    Console.WriteLine("Cookies:");
                    foreach (var cookie in response.Cookies)
                    {
                        Console.WriteLine($"    {cookie.Key}: {cookie.Value}");
                    }
                }
            }

            if (!string.IsNullOrEmpty(response.Body))
            {
                Console.WriteLine($"  Body Length: {response.Body.Length} characters");
                
                Console.WriteLine();
                Console.WriteLine("Response Body (first 500 characters):");
                Console.WriteLine("----------------------------------------");
                var preview = response.Body.Substring(0, Math.Min(500, response.Body.Length));
                Console.WriteLine(preview);
                if (response.Body.Length > 500)
                {
                    Console.WriteLine("...");
                    Console.WriteLine($"(Showing 500 of {response.Body.Length} total characters)");
                }
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine();
            Console.WriteLine("✓ Request completed successfully!");
            
            // Test cookie session management
            if (verbose)
            {
                Console.WriteLine();
                Console.WriteLine("Testing cookie session management...");
                
                var getCookiesInput = new GetCookiesFromSessionInput
                {
                    SessionId = client.SessionId,
                    Url = url
                };
                var cookiesOutput = client.GetCookiesFromSession(getCookiesInput);
                
                Console.WriteLine($"  Session cookies: {cookiesOutput.Cookies?.Count ?? 0}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("✗ Error occurred:");
            Console.WriteLine($"  {ex.GetType().Name}: {ex.Message}");
            
            if (verbose && ex.StackTrace != null)
            {
                Console.WriteLine();
                Console.WriteLine("Stack Trace:");
                Console.WriteLine(ex.StackTrace);
            }
            
            Environment.Exit(1);
        }

        Console.WriteLine();
        Console.WriteLine("Done!");
    }
}
