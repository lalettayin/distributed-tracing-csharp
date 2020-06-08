using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
                        var result = x + y;
                        await context.Response.WriteAsync(result.ToString());
                    });
                });
            });
        });

        var host = builder.Build();
        host.Run();
    }
}
