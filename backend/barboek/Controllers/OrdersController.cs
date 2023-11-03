using barboek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private ILogger<OrdersController> _logger;
        private OrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public IActionResult AddItem([FromBody] OrderBody body)
        {
            _orderService.CreateOrder(Guid.Parse(body.AccountOrdered), body.OrderItems);
            return Ok();
        }

        public struct OrderBody
        {
            public string AccountOrdered { get; set; }
            public Dictionary<Guid, int> OrderItems { get; set; }
        };

    }
}
