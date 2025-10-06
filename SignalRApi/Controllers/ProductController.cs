using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _mapper.Map<List<ResultProductDto>>(_productService.TGetList()); // _productService.TGetList() List<Product> döner
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            _productService.TAdd(value);
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetById(id);
            _productService.TDelete(value);
            return Ok("Silme Başarılı");
        }
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _productService.TUpdate(value);
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetById(id);
            return Ok(_mapper.Map<GetProductDto>(value));
        }

		[HttpGet("ProductListWithCategory")]
		public IActionResult ProductListWithCategory()
		{
			var context = new SignalRContext();
			var values = context.Products.Include(x => x.Category);
			var items = _mapper.Map<List<ResultProductWithCategory>>(values);
			return Ok(items.ToList());
		}
	}
}
