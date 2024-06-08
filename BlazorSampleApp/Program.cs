using BlazorSampleApp;
using BlazorSampleApp.Components;
using Radzen;
using DatabaseAccessLib;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddSingleton<IDataAccessAPI, DataAccessAPIWrapper>();

        //builder.Services.AddScoped<DialogService>();
        //builder.Services.AddScoped<NotificationService>();
        //builder.Services.AddScoped<TooltipService>();
        //builder.Services.AddScoped<ContextMenuService>();

        builder.Services.AddRadzenComponents();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode();

  


        app.Run();
    }
}

