using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Middle.WebAPI.Middlewares;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors();

//builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services
    .AddExceptionHandler<MyExceptionHandler>()
    .AddProblemDetails();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", configure =>
    {
        configure.Window = TimeSpan.FromSeconds(1); //ka� saniyede istek k�s�tlanaca��
        configure.PermitLimit = 100; //window'da belirtilen s�rede ka� istek kabul edilece�i
        configure.QueueLimit = 100; //istek d���nda kalanlar�n ka��n�n kuyru�a eklenece�i
        configure.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //i�lenme s�ras� => �lk giren ilk ��kar vs.
    });
});

builder.Services.AddHealthChecks().AddCheck("healthcheck", () => HealthCheckResult.Healthy());
builder.Services.AddHealthChecksUI(options =>
{
    options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
}).AddInMemoryStorage();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

//app.UseHealthChecks("/healtcheck");

app.UseCors(x => x.AllowAnyOrigin());

app.UseExceptionHandler();

app.UseRateLimiter();

//app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
