using barboek.Api.Models;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Api;

[ApiController]
[Route("api/[controller]")]
public class ItemCategoryController : ControllerBase
{
    private IItemCategoryService _itemCategoryService;
    public ItemCategoryController(IItemCategoryService itemCategoryService)
    {
        _itemCategoryService = itemCategoryService;
    }
    [HttpGet]
    public ActionResult<List<ItemCategory>> Get()
    {
        return Ok(_itemCategoryService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<ItemCategory> GetById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("id is not valid");

        ItemCategory itemCategory = _itemCategoryService.GetById(id);

        if (itemCategory.Id == Guid.Empty) return NotFound();

        return Ok(itemCategory);
    }

    [HttpPost("{name}")]
    public ActionResult<ItemCategory> Create(string name, [FromBody] string iconString = "")
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("name is invalid");
        // TODO: Add more validation requirements

        ItemCategory itemCategory = _itemCategoryService.Create(name, iconString);

        if (itemCategory.Id == Guid.Empty) return StatusCode(500);

        return Ok(itemCategory);

    }

    [HttpPut("{id}")]
    public ActionResult<ItemCategory> Edit(Guid id, [FromBody] ApiItemCategoryDetails details)
    {
        ActionResult<ItemCategory> getByIdResult = GetById(id);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        if (string.IsNullOrEmpty(details.Name)) return BadRequest("name is invalid");
        if (string.IsNullOrEmpty(details.IconString)) details.IconString = "question_mark";
        // TODO: Add more validation requirements

        ItemCategory itemCategory = _itemCategoryService.Edit(id, details.Name, details.IconString);

        if (itemCategory.Id == Guid.Empty) return StatusCode(500);

        return Ok(itemCategory);
    }

    [HttpPut("setactive/{id}")]
    public ActionResult<ItemCategory> SetActive(Guid id, [FromBody] bool active = true)
    {
        ActionResult<ItemCategory> getByIdResult = GetById(id);

        if (getByIdResult.Result is BadRequestObjectResult || getByIdResult.Result is NotFoundResult) return getByIdResult;

        ItemCategory itemCategory = _itemCategoryService.SetActive(id, active);

        if (itemCategory.Id == Guid.Empty) return StatusCode(500);

        return Ok(itemCategory);
    }

    [HttpPut("setorder")]
    public ActionResult SetOrder([FromBody] List<ItemCategory> categories)
    {
        List<ItemCategory> itemCategories = new List<ItemCategory>();
        foreach (ItemCategory category in categories)
        {
            int index = categories.IndexOf(category);
            ItemCategory temp = new ItemCategory
            {
                Id = category.Id,
                Order = index
            };
            itemCategories.Add(temp);
        }
        _itemCategoryService.MassUpdateOrders(itemCategories);

        return Ok();
    }
}