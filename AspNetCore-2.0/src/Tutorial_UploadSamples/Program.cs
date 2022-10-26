using Microsoft.AspNetCore.Server.Kestrel.Core;
using Tutorial_UploadSamples.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = null;
});
services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = null;
    // NOTE: Need to use Http1, since upload will drop connection random unser HTTPS protocol
    // https://learn.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0
    options.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http1);
});

builder.Services.AddControllersWithViews();
services.AddTransient<IStreamFileUploadService, StreamFileUploadLocalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
