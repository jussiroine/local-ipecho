var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Helper method to get client IP address
string GetClientIpAddress(HttpContext context)
{
    // Check for X-Forwarded-For header (proxy scenarios)
    var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
    if (!string.IsNullOrEmpty(forwardedFor))
    {
        // Take the first IP if multiple are present
        return forwardedFor.Split(',')[0].Trim();
    }
    
    // Check for X-Real-IP header (nginx proxy)
    var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
    if (!string.IsNullOrEmpty(realIp))
    {
        return realIp;
    }
    
    // Fallback to remote IP address
    return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
}

// Handle GET requests
app.MapGet("/", (HttpContext context) =>
{
    var clientIp = GetClientIpAddress(context);
    return Results.Text(clientIp);
});

// Handle POST requests
app.MapPost("/", (HttpContext context) =>
{
    var clientIp = GetClientIpAddress(context);
    return Results.Text(clientIp);
});

app.Run();
