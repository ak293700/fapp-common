using System.Text;
using FappCommon.Implementations.ICurrentUserServices;
using FappCommon.Interfaces.ICurrentUserServices;
using FappCommon.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using TestWebProject.Controllers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// builder.Services.AddScoped<LogTraceMiddleware>();
builder.Services.AddScoped<ICurrentUserServiceInt, CurrentUserServiceIntImpl>();
// builder.Services.AddScoped<ICurrentUserService>(
//     provider => provider.GetService<ICurrentUserServiceInt>()!
// );

builder.Services.AddLogTraceMiddlewareWithCurrentUserService<ICurrentUserServiceInt>();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Jad - User Microservice", Version = "v1.0" });

    // Allow swagger to handle JWT Bearer tokens and authorization
    // Should not change "oauth2" to another name
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    // That is a need for authorization in swagger to
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.Configure<SwaggerUIOptions>(options =>
{
    options.DocExpansion(DocExpansion.None); // Set default collapse state
});

#endregion


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DummyController.AuthToken)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();


WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseLogTraceMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();