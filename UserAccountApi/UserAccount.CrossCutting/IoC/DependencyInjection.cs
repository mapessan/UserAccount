using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserAccount.Domain.Repository.Interfaces;
using UserAccount.Domain.Service;
using UserAccount.Domain.Service.Interfaces;
using UserAccount.Infra.Repository;

namespace UserAccount.CrossCutting.IoC;

public static class DependencyInjection
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        //builder.Services.Configure<BlogDataContext>();
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
    }
}


