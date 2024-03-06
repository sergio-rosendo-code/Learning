using API.Infrastructure.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("V1", new OpenApiInfo
    {
        Title = "File System API",
        Version = "V1"
    });
});

var app = builder.Build();
app.MapEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/V1/swagger.json", "File System API V1");
    });
}
app.Run();