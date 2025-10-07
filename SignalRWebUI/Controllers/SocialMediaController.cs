using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SocialMediaDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class SocialMediaController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public SocialMediaController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("http://localhost:7031/api/SocialMedia");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
				return View(result);
			}
			return View();
		}
		[HttpGet]
		public IActionResult CreateSocialMedia()
		{
			return View();
		
		}

		[HttpPost]
		public IActionResult  CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createSocialMediaDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = client.PostAsync("http://localhost:7031/api/SocialMedia", stringContent).Result;
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

	}
}
