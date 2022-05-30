using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using CRM_API.SchemaFilter;
using CRM_API.Sessions.Models;
using CRM_API.Swagger;
using Data.ModelsCrm;
using Data.ModelsCrmClient;
using DomainDependencyInjection;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar(
    (context, registry) =>
    {
        // register services using Lamar

        // add the controllers
        registry.AddControllers(opt =>
        {
            opt.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            opt.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            }));
        });

        registry.AddDbContext<CrmContext>(opt =>
        {
            opt.UseSqlite("Data Source=crmDataBase.db3", b => b.MigrationsAssembly("CRM-API"));
        });

        registry.Include(DomainServerServiceRegister.GetRegister());

        registry.For<ConcurrentDictionary<string, Session>>().Use<ConcurrentDictionary<string, Session>>().Singleton();

        registry.AddEndpointsApiExplorer();
        registry.AddSwaggerGen(opt =>
        {
            opt.SchemaFilter<SwaggerExcludeFilter>();

            opt.AddSecurityDefinition("token", new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "token",
                    Type = ReferenceType.SecurityScheme
                },
                Description = "Token provided by API",
                Name = "api-token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });

            var openApiSecurityRequirement = new OpenApiSecurityRequirement();
            openApiSecurityRequirement.Add(new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "token",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>());

            opt.AddSecurityRequirement(openApiSecurityRequirement);
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
        options.DefaultModelExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
