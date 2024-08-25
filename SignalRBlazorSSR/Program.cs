using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor.Services;
using SignalRBlazorSSR.Components;
using SignalRBlazorSSR.Hubs;
using SignalRBlazorSSR.Services;

var builder = WebApplication.CreateBuilder(args);

// responseCompression
// Not sure what this does guide said i should add it?
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" }
    );
});

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSignalR();
builder.Services.AddSingleton<EmployeeService>();

var app = builder.Build();

// app.UseResponseCompression();

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

app.MapHub<EmployeeHub>("/employeehub");

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
