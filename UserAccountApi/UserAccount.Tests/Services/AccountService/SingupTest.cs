using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using UserAccount.Domain.Entity;
using UserAccount.Domain.Repository.Interfaces;
using UserAccount.Domain.Service.Interfaces;
using UserAccount.Domain.ViewModel.Input;
using Xunit;

namespace UserAccount.Tests.Services.AccountService;

public class SingupTest
{
    public Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    public Mock<ITokenService> _tokenService = new Mock<ITokenService>();


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
    public void ShouldSignupSuccessfully()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "Alfred!#123",
        });

        result.Should().NotBe(null);
        result.Data.Email.Should().Be("newmail@gmail.com");
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShouldReturnErrorUserDataIsInvalid()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));
        SingupViewModel singUpNullMock = null;

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(singUpNullMock);

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User data is invalid.");
    }

    [Fact]
    public void ShouldReturnErrorUserEmailIsInvalid()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmaigmail.com",
            Password = "Alfred!#123",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User email is invalid.");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordMinLenghtIsInvalid()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "!AVCxd3",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordDoesNotContainsLowerCharactere()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "!AVCASDSA3",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordDoesNotContainsUpperCharactere()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "!dasda321@3",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordDoesNotContainsNumberCharactere()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "!dasdaFSD#@#",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordDoesNotContainsSpecialCharactere()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "123dasdaFSD",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserPasswordMaxLenghtIsInvalid()
    {
        var userMock = new User() { Email = "newmail@gmail.com" };

        _userRepository.Setup(p => p.AddUser(userMock));

        //Validation password lenght < 8
        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "newmail@gmail.com",
            Password = "123d@#$dfsdf3242R$@#fdssdfw@#$dsfsdfsd3$423$vdfdgd#@$fgxfgxfgdg@#$ZCSDFSfsdfSD@#$vdcV$@#$dfsdfasdaFSD",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User password must contain at least 8 characters (upper/lower case letters and digits).");
    }

    [Fact]
    public void ShouldReturnErrorUserAlreadyExists()
    {
        var userMock = MockUser();

        _userRepository.Setup(p => p.GetUserByEmail("teste@teste.com"))
            .Returns(userMock);

        _userRepository.Setup(p => p.AddUser(userMock));

        var accountService = new Domain.Service.AccountService(_userRepository.Object, _tokenService.Object);
        var result = accountService.CreateUser(new Domain.ViewModel.Input.SingupViewModel()
        {
            Email = "teste@teste.com",
            Password = "Alfred!#123",
        });

        result.Should().NotBe(null);
        result.Data.Should().BeNull();
        result.Errors.Should().Contain("User already exists.");
    }
}
