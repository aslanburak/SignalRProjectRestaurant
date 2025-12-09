using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class BookingController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BookingController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7031/api/Booking"); // API URL'si

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisini oku
				var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData); // JSON verisini nesnelere dönüştür

				//JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData) → JSON string’ini List<ResultBookingDto> nesnelerine dönüştürür.
				//Buradaki ResultBookingDto sınıfı JSON alanlarıyla eşleşmelidir.
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateBooking()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
		{
			createBookingDto.Description = "Rezervasyon Alındı";
			var client = _httpClientFactory.CreateClient(); // Yeni istemci oluşturulur
			var jsonData = JsonConvert.SerializeObject(createBookingDto); // DTO nesnesi JSON string'ine dönüştürülür
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // JSON içeriği hazırlanır

			var responseMessage = await client.PostAsync("https://localhost:7031/api/Booking", stringContent); // POST isteği gönderilir
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteBooking(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7031/api/Booking/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		// İlk önce güncellemek istediğim kategoriyi göstermek için
		public async Task<IActionResult> UpdateBooking(int id)
		{
			var client = _httpClientFactory.CreateClient();
			// İlk önce güncellemek istediğim kategoriyi göstermek için
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Booking/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
		{
			updateBookingDto.Description = "Rezervasyon Güncellendi";
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateBookingDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PutAsync("https://localhost:7031/api/Booking", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> BookingStatusApproved(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Booking/BookingStatusApproved/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> BookingStatusCancelled(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Booking/BookingStatusCancelled/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();

		}
	}
}
