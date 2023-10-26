using barboek.Interface.Models;
using barboek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Controllers;
[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private ILogger<ItemsController> _logger;
    private ItemService _itemService;

    public ItemsController(ILogger<ItemsController> logger, ItemService itemService)
    {
        _logger = logger;
        _itemService = itemService;
    }

    [HttpGet]
    [Route("list")]
    [Authorize]
    public IActionResult List()
    {
        List<Item> Items = _itemService.GetItems();
        object returnObject = new
        {
            body = Items,
        };
        return Ok(returnObject);
    }
}