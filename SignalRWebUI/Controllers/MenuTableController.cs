using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using SignalRWebUI.Dtos.MenuTableDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class MenuTableController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public MenuTableController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/MenuTables"); // API URL'si

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisini oku
				var values = JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData); // JSON verisini nesnelere dönüştür

				//JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData) → JSON string’ini List<ResultMenuTableDto> nesnelerine dönüştürür.
				//Buradaki ResultMenuTableDto sınıfı JSON alanlarıyla eşleşmelidir.
				return View(values);
			}
			return View();
		}
		[HttpGet]
		public IActionResult CreateMenuTable()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateMenuTable(CreateMenuTableDto createMenuTableDto)
		{
			createMenuTableDto.Status = false; // Yeni eklenen menü masası varsayılan olarak kullanılmıyor
			var client = _httpClientFactory.CreateClient(); // Yeni istemci oluşturulur
			var jsonData = JsonConvert.SerializeObject(createMenuTableDto); // DTO nesnesi JSON string'ine dönüştürülür
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON içeriği hazırlanır

			var responseMessage = await client.PostAsync("https://localhost:7031/api/MenuTables", stringContent); // POST isteği gönderilir
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteMenuTable(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/MenuTables/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		// İlk önce güncellemek istediğim kategoriyi göstermek için
		public async Task<IActionResult> UpdateMenuTable(int id)
		{
			var client = _httpClientFactory.CreateClient();
			// İlk önce güncellemek istediğim kategoriyi göstermek için
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/MenuTables/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateMenuTableDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateMenuTable(UpdateMenuTableDto updateMenuTableDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateMenuTableDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PutAsync("https://localhost:7031/api/MenuTables", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("UpdateMenuTable");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> TableListByStatus()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/MenuTables"); // API URL'si

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisini oku
				var values = JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData); // JSON verisini nesnelere dönüştür

				//JsonConvert.DeserializeObject<List<ResultMenuTableDto>>(jsonData) → JSON string’ini List<ResultMenuTableDto> nesnelerine dönüştürür.
				//Buradaki ResultMenuTableDto sınıfı JSON alanlarıyla eşleşmelidir.
				return View(values);
			}
			return View();
		}
	}
}
