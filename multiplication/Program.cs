using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(route =>
                {
                    route.MapGet("/", async context =>
                    {
                        int.TryParse(context.Request.Query["x"].ToString(), out int x);
                        int.TryParse(context.Request.Query["y"].ToString(), out int y);

                        var total = (x % 2) == 0 ? 0 : y;

                        for (var i = 0; i < x / 2; i++)
                        {
                            using var webClient = new WebClient();
                            webClient.QueryString.Add("x", y.ToString());
                            webClient.QueryString.Add("y", y.ToString());

                            var apiUri = new Uri("http://addition/");
                            var result = await webClient.DownloadStringTaskAsync(apiUri);

                            int.TryParse(result, out int multiply);
                            total += multiply;
                        }


                        await context.Response.WriteAsync(total.ToString());
                    });
                });
            });
        });

        var host = builder.Build();
        host.Run();
    }
}
