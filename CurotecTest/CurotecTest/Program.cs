using CurotecTest;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Serilog;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        RegisterModule.AddSerilogApi(builder.Configuration);
        builder.Host.UseSerilog(Log.Logger);

        // Add services to the container.

        builder.Services.AddCors(p => p.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build();
        }));

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curotec Test", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
            new List<string>()
        }
            });
        });

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddDbContext<CurotecDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerCurotec")));

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Application"));
        });

        builder.Services.AddRepositories();
        builder.Services.AddValidators();
        builder.Services.AddServices();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddIdentityCore<User>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
        })
        .AddRoles<Role>()
        .AddEntityFrameworkStores<CurotecDbContext>()
        .AddRoleValidator<RoleValidator<Role>>()
        .AddRoleManager<RoleManager<Role>>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
           opt.TokenLifespan = TimeSpan.FromHours(2));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authorization:SecretKey"))),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "MediaFiles"));

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                   Path.Combine(builder.Environment.ContentRootPath, "MediaFiles")),
            RequestPath = "/MediaFiles"
        });

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using (var serviceScope = app.Services.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<CurotecDbContext>()?.Database.Migrate();
        }

        app.Run();
    }
}