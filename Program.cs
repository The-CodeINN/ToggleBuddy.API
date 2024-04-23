
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Mappings;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Implementations;
using ToggleBuddy.API.Repositories.Interfaces;
using ToggleBuddy.API.Services.AuthServices;
using ToggleBuddy.API.Services.FeatureServices;
using ToggleBuddy.API.Services.ProjectServices;

namespace ToggleBuddy.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // adding database
            builder.Services.AddDbContext<ToggleBuddyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ToggleBuddyConnectionString")));

            // adding repositories
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IUser, UserRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();

            // adding services
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IFeatureServices, FeatureServices>();

            // Configure ASP.NET Core Identity services
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>("ToggleBuddy")
                .AddEntityFrameworkStores<ToggleBuddyDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Customize password requirements
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;

            });

            // Adding automapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // adding authentication configuration
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            try
            {
                DbInitializer.InitDb(app);
            }
            catch (Exception e)
            {

                Console.WriteLine("Error occurred while initializing database", e.Message);
            }
            app.Run();
        }
    }
}