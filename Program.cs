using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(opts =>
{
    opts.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

app.MapGet("/", (HttpContext context) =>
{
    var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    return Results.Text(clientIp);
});

app.MapPost("/", (HttpContext context) =>
{
    var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    return Results.Text(clientIp);
});

app.Run();
