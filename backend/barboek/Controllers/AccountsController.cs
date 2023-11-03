using barboek.Interface.Models;
using barboek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private AccountService _accountService;

    public AccountsController(ILogger<AccountsController> logger, AccountService accountService)
    {
        _logger = logger; 
        _accountService = accountService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public IActionResult List([FromBody]ListBody body)
    {
        List<Account> accounts = _accountService.GetFromFilter(body.Filter);
        object returnObject = new {
            body = accounts,
        };
        return Ok(returnObject);
    }

    [HttpPost]
    [Route("test_AddUser")]
    [Authorize]
    public IActionResult AddUser([FromBody]AddUserBody body)
    {
        _accountService.AddUser(body.Name);
        return Ok();
    }

    public struct AddUserBody
    {
        public string Name { get; set; }
    }

    public struct ListBody
    {
        public string Filter { get; set;}
    }
}
