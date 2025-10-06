using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.DiscountDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class DiscountController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;
		public DiscountController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/Discount");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jasonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultDiscountDto>>(jasonData); //Json data veri serisini çöz ve nesneye dönüştür
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateDiscount()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateDiscount(CreateDiscountDto createDiscountDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createDiscountDto); // DTO nesnesini JSON string'ine dönüştürür
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //Json İçeriği hazırlar
			var responseMessage = await client.PostAsync("https://localhost:7031/api/Discount", stringContent); //POST isteği gönderir

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();

		}


		[HttpGet]
		public async Task<IActionResult> UpdateDiscount(int id) //Listeleme ile aynı hemen hemen 
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Discount/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateDiscountDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto updateDiscountDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateDiscountDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7031/api/Discount", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();

		}


		[HttpDelete]
		public async Task<IActionResult> DeleteDiscount(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/Discount/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
