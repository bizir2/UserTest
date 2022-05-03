using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UsersTest.Auth;
using UsersTest.Context;
using UsersTest.Entites;
using UsersTest.Interfaces;
using UsersTest.Models;
using UsersTest.Serializers;
using UsersTest.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(
        options => 
            options.OutputFormatters.Add(new XmlSerializerOutputFormatterNamespace())
    )
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    })
    .AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters();

var configuration = builder.Configuration;
builder.Services.Configure<MySqlConnection>(
    configuration.GetSection(nameof(MySqlConnection))
);
var basicAuth = configuration.GetSection(nameof(BasicAuth))
    .Get<BasicAuth>();


builder.Services.AddDbContext<MySqlContext>();

IMapper mapper = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
}).CreateMapper();

builder.Services.AddAuthentication()
    .AddScheme<BasicAuthOptions, BasicAuthenticationHandler>(BasicAuthenticationHandler.BasicAuthentication,
        options =>
        {
            options.Login = basicAuth.Login;
            options.Pass = basicAuth.Pass;
        });

builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<IMySqlConnection>(sp =>
    sp.GetRequiredService<IOptions<MySqlConnection>>().Value);
builder.Services.AddScoped<MySqlContext>();

builder.Services.AddSingleton<IСacheDbOperation<UserDocument>, СacheDbOperation<UserDocument>>();
builder.Services.AddScoped<IDbOperation<UserDocument>, DbOperation<UserDocument>>();

builder.Services.AddScoped<IUserDbOrCache, UserDbOrCache>();
builder.Services.AddScoped<IUsersDbOperation, UsersDbOperation>();
builder.Services.AddSingleton<IUserСacheDbOperation, UserСacheDbOperation>();

builder.Services.AddTransient<JobFactory>();
builder.Services.AddScoped<DataJob<UserDocument>>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(BasicAuthenticationHandler.BasicAuthentication, new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
}); 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetService<MySqlContext>().Database.EnsureCreatedAsync();
    await scope.ServiceProvider.GetService<IUserDbOrCache>().SetAll();
    var serviceProvider = scope.ServiceProvider;
    try
    {
        DataScheduler.Start(serviceProvider);
    }
    catch (Exception)
    {
        throw;
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();