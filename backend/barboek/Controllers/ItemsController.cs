using barboek.Interface.Models;
using barboek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


    [HttpPost]
    [Route("test_AddItem")]
    [Authorize]
    public IActionResult AddItem([FromBody] AddItemBody body)
    {
        _itemService.AddItem(body.Name, body.Price);
        return Ok();
    }

    public struct AddItemBody
    {
        public string Name { get; set; }
        public Guid Price { get; set; }
    }
}