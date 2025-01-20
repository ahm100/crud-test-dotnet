using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Vehicle.API;
using Vehicle.API.Middleware;
using Vehicle.API.SwaggerFilter;
using Vehicle.Application;
using Vehicle.Application.Contracts;
using Vehicle.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkedJob API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  }
              },
              new string[] {}
        }
    });
    c.OperationFilter<AllowAnonymousOperationFilter>();
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton<ISharedLocalizer, SharedLocalizer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseMiddleware<TokenMiddleware>(); // Use custom middleware to append Bearer prefix

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Register the middleware

app.UseAuthentication();
app.UseAuthorization();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.MapControllers();

app.Run();
