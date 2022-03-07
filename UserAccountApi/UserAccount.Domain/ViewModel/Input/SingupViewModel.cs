using System;
using UserAccount.Domain.Entity;

namespace UserAccount.Domain.ViewModel.Input;

public class SingupViewModel
{
    public string Password { get; set; }

    public string Email { get; set; }

    public IList<Role> Roles { get; set; }
}

