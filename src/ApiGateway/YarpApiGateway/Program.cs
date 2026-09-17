using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddRateLimiter(options =>
    {
      options.AddFixedWindowLimiter("fixed", op =>
      {
        op.Window = TimeSpan.FromSeconds(10);
        op.PermitLimit = 5;
      });
    });



app.Run();
