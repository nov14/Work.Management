using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using Work.Api.Data;
using Work.Api.Helpers;
using Work.Api.Helpers.AuthHelper;
using Work.Api.Models;
using Work.Api.Services;

namespace Work.Api
{
    public class Startup
    {
        private string apiVersionName = "V1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                setup.CacheProfiles.Add("120sCacheProfile", new CacheProfile { Duration = 120});
            }).AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setup => 
            {
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetials = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https://bilibili.com",
                        Title = "有错误！！",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "请看详细信息",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetials.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetials);
                };
            });
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddDbContext<WorkDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("WorkDbContext"));
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #region Swagger
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc(apiVersionName, new OpenApiInfo()
                {
                    Version = apiVersionName,
                    Title = $"Api.{apiVersionName}.doc",
                    Contact = new OpenApiContact() { Url = new Uri("http://www.baidu.com"), Name = "anson" }
                });
                options.IncludeXmlComments($"{Path.Combine(AppContext.BaseDirectory, "Work.Api.xml")}", true);
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权，注意在token前输入“Bearer ”",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
            #endregion

            #region 参数
            var authConfig = Configuration.GetSection("AuthConfig");
            var symmetricKeyAsBase64 = authConfig["Secret"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricKeyAsBase64));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = authConfig["Issuer"],

                ValidateAudience = true,
                ValidAudience = authConfig["Audience"],

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var tokenModels = new List<TokenModel>();
            var permissionRequirement = new PermissionRequirement(
                tokenModels,
                ClaimTypes.Role,
                authConfig["Issuer"],
                authConfig["Audience"],
                TimeSpan.FromSeconds(60),
                signingCredentials
                );
            #endregion
            #region 认证
            services.AddAuthentication("Bearer")
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = tokenValidationParameters;
                });
            #endregion
            #region 授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name, policy => policy.Requirements.Add(permissionRequirement));
            });
            #endregion
            //注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);

            #region CORS跨域
            services.AddCors(c => 
            {
                c.AddPolicy("LimitRequests", policy => 
                {
                    policy
                    .WithOrigins("http://localhost:8080", "http://192.168.101.11:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error!");
                    });
                });
            }
            app.UseResponseCaching();
            app.UseCors("LimitRequests");
            

            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint($"{apiVersionName}/swagger.json", $"{apiVersionName}.doc");
            });

            

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
