using System.Collections.Generic;
using FluentAssertions;
using Moq;
using UserAccount.Domain.Entity;
using UserAccount.Domain.Repository.Interfaces;
using UserAccount.Domain.Service.Interfaces;
using UserAccount.Domain.ViewModel.Input;
using Xunit;

namespace UserAccount.Services.AccountService.LoginTest;

public class LoginTest
{
    private Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private Mock<ITokenService> _tokenService = new Mock<ITokenService>();

    private User MockUser()
    {
        return new User()
        {
            Email = "teste@teste.com",
            PasswordHash = "10000.9kc1H1/HBzOhUvnGLbZLHg==.fkmN1ShUNF9EvIVhP9lcKAQJ9d6WfMKJ/xIhXKgC90E=",
            Roles = new List<Role>()
                {
                    new Role() { Name = "role", Slug = "role"}
                }
        };
    }

    [Fact]
    public void ShouldLoginSuccessfully()
    {
        var userMoq = MockUser();

        _userRepository.Setup(p => p.GetUserByEmail("teste@teste.com"))
            .Returns(userMoq);

        _tokenService.Setup(t => t.GenerateToken(userMoq)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcGVzc2FuQGdtYWlsLmNvbSIsInJvbGUiOiJ0ZXN0ZSIsIm5iZiI6MTY0NjY3ODMxMCwiZXhwIjoxNjQ2NjgxOTEwLCJpYXQiOjE2NDY2NzgzMTB9.QFLfgnwLZQ9tu2CKPud0ejbgjl_s-seZSyhALdnVMQA");

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.Login(new LoginViewModel() { Email = "teste@teste.com", Password = "Alfredo#123" });

        result.Should().NotBe(null);
        result.Data.Should().NotBeNullOrWhiteSpace();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShouldReturnErrorUserInvalid()
    {
        var userMock = MockUser();

        _userRepository.Setup(p => p.GetUserByEmail("teste@teste.com"))
            .Returns(userMock);

        _tokenService.Setup(t => t.GenerateToken(userMock)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcGVzc2FuQGdtYWlsLmNvbSIsInJvbGUiOiJ0ZXN0ZSIsIm5iZiI6MTY0NjY3ODMxMCwiZXhwIjoxNjQ2NjgxOTEwLCJpYXQiOjE2NDY2NzgzMTB9.QFLfgnwLZQ9tu2CKPud0ejbgjl_s-seZSyhALdnVMQA");

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.Login(new LoginViewModel() { Email = "", Password = "Alfredo#123" });

        result.Should().NotBe(null);
        result.Data.Should().BeNullOrWhiteSpace();
        result.Errors.Should().NotBeNullOrEmpty();

        result.Errors.Should().Contain("User or password is invalid.");
    }

    [Fact]
    public void ShouldReturnErrorPasswordInvalid()
    {
        var userMoq = MockUser();

        _userRepository.Setup(p => p.GetUserByEmail("teste@teste.com"))
            .Returns(userMoq);

        _tokenService.Setup(t => t.GenerateToken(userMoq)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcGVzc2FuQGdtYWlsLmNvbSIsInJvbGUiOiJ0ZXN0ZSIsIm5iZiI6MTY0NjY3ODMxMCwiZXhwIjoxNjQ2NjgxOTEwLCJpYXQiOjE2NDY2NzgzMTB9.QFLfgnwLZQ9tu2CKPud0ejbgjl_s-seZSyhALdnVMQA");

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.Login(new LoginViewModel() { Email = "teste@teste.com", Password = "" });

        result.Should().NotBe(null);
        result.Data.Should().BeNullOrWhiteSpace();
        result.Errors.Should().NotBeNullOrEmpty();

        result.Errors.Should().Contain("User or password is invalid.");
    }

    [Fact]
    public void ShouldReturnErrorWhenUserNotExists()
    {
        var userMoq = MockUser();

        _userRepository.Setup(p => p.GetUserByEmail("teste@teste.com"))
            .Returns(userMoq);

        _tokenService.Setup(t => t.GenerateToken(userMoq)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcGVzc2FuQGdtYWlsLmNvbSIsInJvbGUiOiJ0ZXN0ZSIsIm5iZiI6MTY0NjY3ODMxMCwiZXhwIjoxNjQ2NjgxOTEwLCJpYXQiOjE2NDY2NzgzMTB9.QFLfgnwLZQ9tu2CKPud0ejbgjl_s-seZSyhALdnVMQA");

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.Login(new LoginViewModel() { Email = "teste1@teste.com", Password = "Alfredo#123" });

        result.Should().NotBe(null);
        result.Data.Should().BeNullOrWhiteSpace();
        result.Errors.Should().NotBeNullOrEmpty();

        result.Errors.Should().Contain("User or password is invalid.");
    }
}
