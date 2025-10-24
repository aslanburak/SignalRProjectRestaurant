using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
		
        public BasketController(IBasketService basketService, IProductService productService)
		{
			_basketService = basketService;
			_productService = productService;
		}

        [HttpGet("id")]
        public IActionResult GetBasketByMenuTableId(int id) 
        { 
            var values= _basketService.TGetBasketByMenuTableNumber(id);
            return Ok(values);

        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
		{
			var product= _productService.TGetById(createBasketDto.ProductId);

			_basketService.TAdd(new Basket()
			{
				ProductId = createBasketDto.ProductId,
				Count = 1,
				MenuTableId = 14,
				Price = product.Price,
				TotalPrice = createBasketDto.TotalPrice
			});

			return Ok();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteBasket(int id)
		{var value=_basketService.TGetById(id);
            _basketService.TDelete(value);
            return Ok("Sepetteki Seçilen Ürün Silindi");
			
		}


    }
}
