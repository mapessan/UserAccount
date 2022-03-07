using System;
using UserAccount.Domain.Entity;

namespace UserAccount.Domain.Repository.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);

    User GetUserByEmail(string email);
}