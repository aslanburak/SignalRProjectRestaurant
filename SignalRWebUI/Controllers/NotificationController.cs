using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.NotificationDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class NotificationController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public NotificationController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client= _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/Notifications");
			if (responseMessage.IsSuccessStatusCode) 
			{ 
					var jsonData= await responseMessage.Content.ReadAsStringAsync();
					var values=JsonConvert.DeserializeObject<List<ResultNotificationDto>>(jsonData);
				return View(values);
			}

			return View();
		}

		[HttpGet]
		public IActionResult CreateNotification()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateNotification(CreateNotificationDto createNotificationDto) // Gelen veri nesne
		{
			var client= _httpClientFactory.CreateClient(); // Yeni istemci oluşturulur 
			var jsonData = JsonConvert.SerializeObject(createNotificationDto); // Gelen veriyi json' a çevirmemiz gerek
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON içeriği hazırlanır
			var responceMessage = await client.PostAsync("https://localhost:7031/api/Notifications", stringContent);

			if (responceMessage.IsSuccessStatusCode) 
			{ 
			return RedirectToAction("Index");
			}

			return View();

		}

		public async Task<IActionResult> DeleteNotification(int id)
		{
			var client= _httpClientFactory.CreateClient();
			var responceMessage = await client.DeleteAsync($"https://localhost:7031/api/Notifications/{id}");
			if (responceMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		//İlk önce güncelleme sayfasını getireceğiz
		[HttpGet] 
		public async Task<IActionResult> UpdateNotification(int id)
		{
			var client= _httpClientFactory.CreateClient();
			var responceMessage = await client.GetAsync($"https://localhost:7031/api/Notifications/{id}");
			if (responceMessage.IsSuccessStatusCode)
			{
				var jsonData = await responceMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateNotificationDto>(jsonData);
				return View(values);
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> UpdateNotification(UpdateNotificationDto updateNotificationDto)
		{
			var client= _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateNotificationDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responceMessage = await client.PutAsync("https://localhost:7031/api/Notifications", stringContent);
			if (responceMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> NotificationStatusChangeToTrue(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responceMessage = await client.GetAsync($"https://localhost:7031/api/Notifications/NotificationStatusChangeToTrue/{id}");
			
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> NotificationStatusChangeToFalse(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responceMessage = await client.GetAsync($"https://localhost:7031/api/Notifications/NotificationStatusChangeToFalse/{id}");

			return RedirectToAction("Index");
		}

	}
}
