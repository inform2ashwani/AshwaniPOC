using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POC.Services.Commands.API.Filters;
using POC.Services.Common.Exceptions;
using POC.Services.Identity;
using POC.Services.Requests.Commands;
using POC.Services.Handlers;
using System.Text;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using POC.Logger;
using POC.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

var origin = "Product.Command";

// Add services to the container.
builder.Logging.AddLog4Net();
builder.Services.RegisterRequestHandlers(builder.Configuration.GetConnectionString("POCData")).RegisterLogger().RegisterRepositories();
var swaggerEndpointUrl = builder.Configuration.GetValue<string>("SwaggerConfig:SwaggerEndpointUrl");
builder.Services.AddEndpointsApiExplorer();
//Added for lambda hosting.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Product.Command.Services.Api",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
builder.Services.AddResponseCompression();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IExceptionHelper, ExceptionHelper>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: origin, builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionHandlingAttribute>();
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSession();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerEndpointUrl, "[Dev] POC.Command.Services.Api v1"));
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerEndpointUrl, "[Prod] POC.Command.Services.Api v1"));
}
app.UseHttpsRedirection();
app.UseCors(origin);
app.UseAuthentication();
app.UseAuthorization();
app.Map("/minimal", () => "Hello World");
app.MapControllers();
app.Run();