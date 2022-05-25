using System.Reflection;
using API.Filters;
using Application;
using ConsumeApi.Service;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers(opt =>
opt.Filters.Add<OnExceptionFilter>()
);

//Injection serilog and write in a file
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


//Application
builder.Services.AddDependencyAplication(configuration);
builder.Services.AddInfrastructure(configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProjetWebPcc2022",
        Version = "v1",
        Description = "A simple projet .net web API 6.0 for a school exam",
        Contact = new OpenApiContact
        {
            Name = "Bourguignon Samuel",
            Email = "psr11330@students.ephec.be",
            Url = new Uri("https://github.com/Sa-Bo27")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt auth header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });



    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


//For MovieService
builder.Services.Configure<MovieUrl>(
    builder.Configuration.GetSection(MovieUrl.Movie)
);



builder.Services.AddCors();



builder.Services.UseServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetWebPcc2022 v1"));
}

app.UseHttpsRedirection();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

