using barboek.Api.Models;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Api;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public ActionResult<List<Order>> Get()
    {
        return Ok(_orderService.GetAll(DateTime.Now, DateTime.Now));
    }
    

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Guid is empty");

        Order order = _orderService.GetById(id);

        if (order.Id == Guid.Empty) return NotFound();

        return Ok(order);
    }

    [HttpGet("customer/{userId}")]
    public ActionResult<List<Order>> GetByCustomer(Guid userId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        if (userId == Guid.Empty) return BadRequest("Guid is empty");

        return Ok(_orderService.GetByCustomer(userId, startTime, endTime));
    }

    [HttpGet("seller/{sellerId}")]
    public ActionResult<List<Order>> GetBySeller(Guid sellerId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
    {
        if (sellerId == Guid.Empty) return BadRequest("Guid is empty");

        return Ok(_orderService.GetBySeller(sellerId, startTime, endTime));
    }

    [HttpPost]
    public ActionResult Create([FromBody] ApiOrderDetails apiOrderDetails)
    {
        if (apiOrderDetails.PriceTypeId == Guid.Empty) return BadRequest();

        if (apiOrderDetails.ItemIdsWithAmounts.Count == 0) return BadRequest();

        _orderService.Create(apiOrderDetails.ItemIdsWithAmounts, apiOrderDetails.PriceTypeId);

        return Ok();
    }
}