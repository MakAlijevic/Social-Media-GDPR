using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.BLL.Services;
using SocialMediaAPI.DAL.Data;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace SocialMediaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DbContext for UserDB
            builder.Services.AddDbContext<UserDataContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserDBDefaultConnection"));
            });

            // DbContext for GeneralDB
            builder.Services.AddDbContext<GeneralDataContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GeneralDBDefaultConnection"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<IPolicyRepository, PolicyRepository>();
            builder.Services.AddTransient<IUserPolicyRepository, UserPolicyRepository>();
            builder.Services.AddTransient<IFollowService, FollowService>();
            builder.Services.AddTransient<IFollowRepository, FollowRepository>();
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}