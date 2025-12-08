using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/Product/ProductListWithCategory");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/Category"); // Kategorileri almak için API'ye GET isteği gönderilir
			var jsonData= await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // JSON verisi nesnelere dönüştürülür
			List<SelectListItem> values2 = (from x in values
											select new SelectListItem
										  {
											  Text = x.CategoryName,
											  Value = x.CategoryId.ToString()
										  }).ToList(); // Kategoriler SelectListItem listesine dönüştürülür
			ViewBag.v = values2; // ViewBag aracılığıyla kategoriler view'a gönderilir
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			createProductDto.ProductStatus = true;

			var client = _httpClientFactory.CreateClient(); // Yeni istemci oluşturulur
			var jsonData = JsonConvert.SerializeObject(createProductDto); // DTO nesnesi JSON string'ine dönüştürülür
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON içeriği hazırlanır

			var responseMessage = await client.PostAsync("https://localhost:7031/api/Product", stringContent); // POST isteği gönderilir
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/Product/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		// İlk önce güncellemek istediğim kategoriyi göstermek için
		[HttpGet]
		public async Task<IActionResult> UpdateProduct(int id)
		{
			var client1 = _httpClientFactory.CreateClient();
			var responseMessage1 = await client1.GetAsync("https://localhost:7031/api/Category");
			var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
			var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);
			List<SelectListItem> values2 = (from x in values1
											select new SelectListItem
											{
												Text = x.CategoryName,
												Value = x.CategoryId.ToString()
											}).ToList();
			ViewBag.v = values2;

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Product/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			updateProductDto.ProductStatus = true;
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PutAsync("https://localhost:7031/api/Product", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
