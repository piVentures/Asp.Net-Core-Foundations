using AuthDemo.Api.Models;
using AuthDemo.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemo.Api.Extensions
{
    // ✅ Extension class used to keep Program.cs clean
    // Instead of writing long setup code in Program.cs,
    // we move Identity & Authentication configuration here.
    public static class IdentityExtensions
    {
        // =====================================================
        // REGISTER IDENTITY + DATABASE STORE
        // =====================================================
        public static IServiceCollection AddIdntityHandlerAndStore(
            this IServiceCollection services)
        {
            // Adds ASP.NET Core Identity endpoints
            // (login, register, manage users, etc.)
            // and connects Identity to Entity Framework database
            services.AddIdentityApiEndpoints<AppUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }


        // =====================================================
        // CONFIGURE IDENTITY OPTIONS (PASSWORD + USER RULES)
        // =====================================================
        public static IServiceCollection ConfigureIdentityOptions(
            this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password rules customization
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                // Require unique email for each user
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }


        // =====================================================
        // ADD JWT AUTHENTICATION
        // =====================================================
        public static IServiceCollection AddIdentityAuth(
            this IServiceCollection services,
            IConfiguration config)
        {
            // Configure authentication system
            services.AddAuthentication(options =>
            {
                // JWT will be used as default authentication scheme
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Token is not stored server-side (stateless auth)
                options.SaveToken = false;

                // JWT validation rules
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        // Validate token signature
                        ValidateIssuerSigningKey = true,

                        // Secret key used to verify JWT signature
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    config["AppSettings:JWTSecret"]!
                                )),
                            //
                                ValidateIssuer = false,
                                ValidateAudience = false,
                    };
            });

            
            services.AddAuthorization(options => 
            {options.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();

// Custom policy that requires "LibraryId" claim to access certain endpoints
            options.AddPolicy("HasLibraryId", policy => policy.RequireClaim("LibraryId")); 

          options.AddPolicy("FemaleOnly", policy => policy.RequireClaim("Gender", "Female"));    

          options.AddPolicy("Under10", policy => policy.RequireAssertion(context => Int32.Parse(context.User.Claims.First(x => x.Type=="Age").Value)<10));

            });
            
            return services;
        }


        // =====================================================
        // ADD AUTHENTICATION MIDDLEWARES TO PIPELINE
        // =====================================================
        public static WebApplication AddIdentityAuthMiddlewares(
            this WebApplication app)
        {
            // Checks incoming request for JWT token
            app.UseAuthentication();

            // Applies authorization rules ([Authorize])
            app.UseAuthorization();

            return app;
        }
    }
}
