using System;
using UserAccount.Domain.Entity;
using UserAccount.Domain.ViewModel.Input;
using UserAccount.Domain.ViewModel.Output;

namespace UserAccount.Domain.Service.Interfaces;

public interface IAccountService
{
    ResultViewModel<User> CreateUser(SingupViewModel user);

    ResultViewModel<string> Login(LoginViewModel login);
}

