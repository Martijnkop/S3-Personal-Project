using barboek.Interface.Models;
using barboek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancesController : ControllerBase
{
    private ILogger<FinancesController> _logger;
    private FinanceService _financeService;
    private ItemService _itemService;

    public FinancesController(ILogger<FinancesController> logger, FinanceService financeService, ItemService itemService)
    {
        _logger = logger;
        _financeService = financeService;
        _itemService = itemService;
    }

    [HttpPost]
    [Route("createpricetype")]
    [Authorize]
    public IActionResult CreatePriceType([FromBody] PriceType pType)
    {
        if (pType.Name == null) return BadRequest();
        PriceType priceType = _financeService.CreatePriceType(pType.Name);

        if (priceType == null) return BadRequest();
        Object returnObject = new
        {
            priceType
        };
        return Ok(returnObject);
    }

    [HttpPost]
    [Route("createprice")]
    [Authorize]
    public IActionResult CreatePrice([FromBody] CreatePriceInput input)
    {
        if (input.Price == null || input.PriceTypeId == null || input.PriceTypeId.Equals(Guid.Empty)) return BadRequest();
        PriceType priceType = _financeService.GetPriceTypeById(input.PriceTypeId);
        if (priceType == null) return BadRequest();

        Price price = _financeService.CreatePrice(input.Price, input.BeginDate, input.EndDate, priceType, input.Name);

        Object returnObject = new
        {
            price
        };
        return Ok(returnObject);
    }

    [HttpPost]
    [Route("createpricewithitem")]
    [Authorize]
    public IActionResult CreatePriceWithItem([FromBody] CreatePriceWithItemInput input)
    {
        if (input.ItemId.Equals(Guid.Empty)) return BadRequest();
        Item item = _itemService.GetItemById(input.ItemId);
        
        if (input.Price == null || input.PriceTypeId == null || input.PriceTypeId.Equals(Guid.Empty)) return BadRequest();
        PriceType priceType = _financeService.GetPriceTypeById(input.PriceTypeId);
        if (priceType == null) return BadRequest();

        Price price = _financeService.CreatePriceWithItem(input.Price, input.BeginDate, input.EndDate, priceType, input.Name, item);

        Object returnObject = new
        {
            price
        };
        return Ok(returnObject);
    }

    public class CreatePriceWithItemInput : CreatePriceInput
    {
        public Guid ItemId { get; set; }
    }

    public class CreatePriceInput
    {
        public double Price { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PriceTypeId { get; set; }
        public string? Name { get; set; } = null;
    }
}
