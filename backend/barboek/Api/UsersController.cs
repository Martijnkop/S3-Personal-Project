using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Api;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{filter}")]
    public ActionResult<List<User>> GetWithFilter(string filter)
    {
        if (filter == null) filter = "";
        return Ok(_userService.GetWithFilter(filter));
    }

    [HttpGet("getbyid/{id}")]
    public ActionResult<User> GetById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Guid not valid");
        }

        User user = _userService.GetById(id);

        if (user.Id == Guid.Empty) return NotFound();

        return Ok(user);
    }

    [HttpPost("add/{name}")]
    public ActionResult<User> Add(string name)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("Name required");
        // TODO: Add more validation requirements

        User user = _userService.Add(name);

        if (user.Id == Guid.Empty) return StatusCode(500);
        return Ok(user);
    }

    [HttpPut("update/{id}")]
    public ActionResult<User> Update(Guid id, [FromQuery] string name)
    {
        ActionResult<User> getByIdResult = GetById(id);
        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;
        
        if (string.IsNullOrEmpty(name)) return BadRequest("Name required");
        // TODO: Add more validation requirements

        User user = _userService.Update(id, name);

        if (user.Id == Guid.Empty) return NotFound();

        return Ok(user);
    }

    [HttpPut("addbalance/{id}")]
    public ActionResult<User> AddBalanceToUser(Guid id, [FromQuery] float balanceToAdd)
    {
        ActionResult<User> getByIdResult = GetById(id);
        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        if (balanceToAdd <= 0) return BadRequest("balance to add cannot be lower than 0");
        // TODO: Add more validation requirements

        User user = _userService.AddBalance(id, balanceToAdd);

        if (user.Id == Guid.Empty) return StatusCode(500);
        return Ok(user);
    }
}