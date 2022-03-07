using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using UserAccount.Domain.Entity;
using Xunit;

namespace UserAccount.Tests.Services.AccountService;

public class GenerateTokenTest
{
    private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

    [Fact]
    public void ShouldGenerateTokenSuccessfully()
    {
        _configuration.Setup(p => p["JwtKey"]).Returns("amFkdGY3ZDg4NjNiNDhlMTI3YjkzMzdkNDkyYjcwOGU=");

        var userMock = new User()
        {
            Email = "teste@teste.com",
            PasswordHash = "10000.9kc1H1/HBzOhUvnGLbZLHg==.fkmN1ShUNF9EvIVhP9lcKAQJ9d6WfMKJ/xIhXKgC90E=",
            Roles = new List<Role>()
                {
                    new Role() { Name = "role", Slug = "role"}
                }
        };

        var tokenService = new Domain.Service.TokenService(_configuration.Object);
        var result = tokenService.GenerateToken(userMock);
        result.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ShouldNotGenerateTokenMissingConfiguration()
    {
        _configuration.Setup(p => p["aJwtKey"]).Returns("amFkdGY3ZDg4NjNi132YjcwOGU=");

        var userMock = new User()
        {
            Email = "teste@teste.com",
            PasswordHash = "10000.9kc1H1/HBzOhUvnGLbZLHg==.fkmN1ShUNF9EvIVhP9lcKAQJ9d6WfMKJ/xIhXKgC90E=",
            Roles = new List<Role>()
                {
                    new Role() { Name = "role", Slug = "role"}
                }
        };

        var tokenService = new Domain.Service.TokenService(_configuration.Object);

        var result = tokenService.GenerateToken(userMock);
        result.Should().Be("Invalid Jwt Key.");
    }
}


