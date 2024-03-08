using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SampleRest;
using SampleRest.Entities;
using SampleRest.Services;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("BasicAuthentication", null);
services.AddAuthorization();
services.Configure<APIUsersConfig>(builder.Configuration.GetSection("APIUsersConfig"))
    .AddScoped<IApiUsers>(p => new ApiUsers(p.GetService<IOptions<APIUsersConfig>>()!.Value));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
