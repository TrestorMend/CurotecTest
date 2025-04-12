using Application.Pipeline;
using Application.Responses;
using Application.Services.Books;
using FluentValidation;
using MediatR;
using Repository;
using Repository.Repositories.Books;
using Repository.Session;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace CurotecTest
{
    public static class RegisterModule
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(ResponseState).Assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
              .FromAssemblyOf<IBookRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped<IResponseState, ResponseState>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserSession, UserSession>();

            services.Scan(scan => scan
              .FromAssemblyOf<IBookService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static void AddSerilogApi(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .CreateLogger();
        }
    }
}
