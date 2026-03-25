namespace Valuator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // логирование 
        app.Use(async (context, next) =>
        {
            var port = context.Connection.LocalPort;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Request handled by port {port} - {context.Request.Path}");
            await next();
        });

        app.MapRazorPages();
        app.Run();
    }
}