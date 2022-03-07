using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserAccount.Domain.Entity;
using UserAccount.Domain.Service;
using UserAccount.Domain.Service.Interfaces;
using UserAccount.Domain.ViewModel.Input;
using UserAccount.Domain.ViewModel.Output;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAccountApi.Controllers;

[ApiController()]
[Route("v1/accounts/")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResultViewModel<User>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultViewModel<User>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultViewModel<User>))]
    public IActionResult Post([FromBody] SingupViewModel user)
    {
        try
        {
            var result = _accountService.CreateUser(user);
            if ((result.Errors == null) || (result.Errors.Count() == 0))
                return Created("user", result);

            return BadRequest(result);
        } 
        catch(Exception ex)
        {
            _logger.LogError("Singup user error ", ex);
            return StatusCode(500, new ResultViewModel<User>("500X01 - Internal server error"));
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultViewModel<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultViewModel<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultViewModel<string>))]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginViewModel login)
    {
        try
        {
            var result = _accountService.Login(login);

            if ((result.Errors == null) || (result.Errors.Count() == 0))
                return Ok(result);

            return BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Login user error ", ex);
            return StatusCode(500, new ResultViewModel<string>("500X01 - Internal server error"));
        }
    }
}

