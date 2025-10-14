using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("totalordercount")]
		public IActionResult TotalOrderCount()
		{
			var result = _orderService.TTotalOrderCount();
			return Ok(result);
		}
		[HttpGet("activeordercount")]
		public IActionResult ActiveOrderCount()
		{
			var result = _orderService.TActiveOrderCount();
			return Ok(result);
		}
		[HttpGet("lastorderprice")]
		public IActionResult LastOrderPrice()
		{
			var result = _orderService.LastOrderPrice();
			return Ok(result);
		}

		[HttpGet("TodayTotalPrice")]
		public IActionResult TodayTotalPrice()
		{
			var result= _orderService.TodayTotalPrice();
			return Ok(result);
		}
	}
}
