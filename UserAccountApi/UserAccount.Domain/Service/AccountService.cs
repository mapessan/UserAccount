using UserAccount.Domain.Entity;
using UserAccount.Domain.Repository.Interfaces;
using UserAccount.Domain.Service.Interfaces;
using SecureIdentity.Password;
using UserAccount.Domain.ViewModel.Input;
using UserAccount.Domain.ViewModel.Output;

namespace UserAccount.Domain.Service;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AccountService(IUserRepository userRepository,
        ITokenService tokenService)
	{
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public ResultViewModel<User> CreateUser(SingupViewModel user)
    {
        var errors = IsValidUser(user);
      
        if (errors.Count() > 0)
            return new ResultViewModel<User>(errors);

        var userEntity = UserConverter(user);

        userEntity.PasswordHash = PasswordHasher.Hash(user.Password);
        
        _userRepository.AddUser(userEntity);

        return new ResultViewModel<User>(userEntity);
    }

    public ResultViewModel<string> Login(LoginViewModel login)
    {
        if ((string.IsNullOrWhiteSpace(login.Email)) || (string.IsNullOrWhiteSpace(login.Password)))
            return new ResultViewModel<string>("User or password is invalid.");

        var user = _userRepository.GetUserByEmail(login.Email);

        if ((user == null) || (!PasswordHasher.Verify(user.PasswordHash, login.Password)))
            return new ResultViewModel<string>("User or password is invalid.");

        var token = _tokenService.GenerateToken(user);
        return new ResultViewModel<string>(token, null);
    }

    private User UserConverter(SingupViewModel user)
    {
        return new User()
        {
            Email = user.Email,
            Roles = user.Roles
        };
    }

    private List<string> IsValidUser(SingupViewModel user)
    {
        var errors = new List<string>();

        if (user == null)
            errors.Add("User data is invalid.");
        else
        {
            if ((string.IsNullOrWhiteSpace(user.Email)) || (!Utils.Utils.IsEmail(user.Email)))
                errors.Add("User email is invalid.");

            if (!IsValidPassword(user.Password))
                errors.Add("User password must contain at least 8 characters (upper/lower case letters and digits).");

            if (errors.Count() > 0)
                return errors;

            var registreredUser = _userRepository.GetUserByEmail(user.Email);
            if (registreredUser != null)
                errors.Add("User already exists.");
        }

        return errors;
    }

    private bool IsValidPassword(string password)
    {
        return ((!string.IsNullOrWhiteSpace(password)) && (Utils.Utils.IsPassword(password)));
    }
}

