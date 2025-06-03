using BeYou.Utils.Converter;
using BeYou.WebAPI.Authorization;
using BeYou.WebAPI.Configuration;
using BeYou.WebAPI.Swagger;
using Newtonsoft.Json;
using Serilog;

var BeYouSpecificOrigins = "_BeYouSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("BeYou", p =>
    {
        p.RequireAuthenticatedUser();
        p.AddRequirements(new IdentifiedUser());
        p.Build();
    });
});

builder.Services.ConfigureDataBase(configuration);

builder.Services.ConfigureAuthentication(configuration);

builder.Services.ConfigureApiVersioning();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureIoC();

builder.Services.ConfigureAutoMapper();

builder.Services.ConfigureFluentValidation();

builder.Services.ConfigureSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: BeYouSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:44378",
                                             "http://localhost:5000",
                                             "https://localhost:44378",
                                             "https://localhost:5000",
                                             "https://localhost:5191",
                                             "http://localhost:5191",
                                             "http://localhost:5173",
                                             "https://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.LoadSwagger();

app.UseHttpsRedirection();

app.UseCors(BeYouSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.ConfigureExceptionHandler(Log.Logger);

await app.RunAsync();