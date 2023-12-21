using barboek.Api.Models;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Api;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private IItemService _itemService;
    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public ActionResult<List<Item>> Get()
    {
        return Ok(_itemService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();

        Item item = _itemService.GetById(id);

        if (item.Id == Guid.Empty) return NotFound();

        return Ok(item);
    }

    [HttpGet("allwithprice/{pricetypeid}")]
    public ActionResult<List<Item>> GetItemsWithPrice(Guid priceTypeId)
    {
        if (priceTypeId == Guid.Empty) return BadRequest();

        return Ok(_itemService.GetAllWithActivePrice(priceTypeId));
    }

    [HttpGet("withprice/{itemId}")]
    public ActionResult<ItemWithPrice> GetItemWithPrice(Guid itemId, [FromBody] Guid priceTypeId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("category/{categoryId}/{priceTypeId}")]
    public ActionResult<List<Item>> GetByCategory(Guid categoryId, Guid priceTypeId)
    {
        if (categoryId == Guid.Empty) return BadRequest();
        if (priceTypeId == Guid.Empty) return BadRequest();

        return Ok(_itemService.GetByCategory(categoryId, priceTypeId));
    }

    [HttpGet("filter/{filter}")]
    public ActionResult<List<Item>> GetByFilter(string filter)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{name}")]
    public ActionResult<Item> Create(string name, [FromForm] ApiItemDetails apiItemDetails)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", apiItemDetails.Image.FileName);

        try
        {
            Console.WriteLine(path);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                apiItemDetails.Image.CopyTo(stream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }

        if (apiItemDetails.Name == "") return BadRequest();

        Dictionary<Guid, float> itemPrices = new Dictionary<Guid, float>();

        foreach (var itemPrice in apiItemDetails.ItemPrices)
        {
            if (!float.TryParse(itemPrice.Value, out float price)) continue;
            itemPrices.Add(itemPrice.Key, price);
        }

        if (itemPrices.Count < apiItemDetails.ItemPrices.Count) return BadRequest();

        Item item = _itemService.Create(name, apiItemDetails.Image.FileName, itemPrices, apiItemDetails.CategoryId, apiItemDetails.TaxTypeId);

        return Ok(item);
    }

    [HttpPut("edit/{itemId}")]
    public ActionResult<Item> Edit(Guid itemId, [FromBody] ApiItemDetails apiItemDetails)
    {
        throw new NotImplementedException();
    }

    [HttpPut("setname/{itemId}")]
    public ActionResult<Item> SetName(Guid itemId, [FromBody] string name)
    {
        throw new NotImplementedException();
    }

    [HttpPut("seticon/{itemId}")]
    public ActionResult<Item> SetIcon(Guid itemId, [FromBody] string iconString)
    {
        throw new NotImplementedException();
    }

    [HttpPut("setcategory/{itemId}")]
    public ActionResult<Item> SetCategory(Guid itemId, [FromBody] Guid categoryId)
    {
        throw new NotImplementedException();
    }

    [HttpPut("setactive/{itemId}")]
    public ActionResult<Item> SetActive(Guid itemId, [FromBody] bool active)
    {
        throw new NotImplementedException();
    }

    [HttpPut("setprices/{itemId}")]
    public ActionResult<Item> SetPrices(Guid itemId, [FromBody] Dictionary<Guid, float> ItemPrices)
    {
        throw new NotImplementedException();
    }
}