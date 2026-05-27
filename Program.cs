using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddOpenApi();
builder.Services.AddSingleton<UserManagement.MockData.UserRepository>();

//Read serilog config from appsetting.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("In Development environment");
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Mgmt API V1"));
}

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<TokenAuthenticationMiddleware>();

//app.UseHttpsRedirection();
app.UseExceptionHandler("/error");
app.MapControllers();

app.Run();
