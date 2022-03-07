using System;
using Microsoft.AspNetCore.Builder;

namespace UserAccount.Shared.IoC;

public static class DependencyInjection
{
	public static void ConfigureServices(WebApplicationBuilder builder)
	{
        //builder.Services.AddDbContext<BlogDataContext>();
        builder.Services.AddTransient<IAccountService>;
        //builder.Services.AddTransient<EmailService>();
    }
}


