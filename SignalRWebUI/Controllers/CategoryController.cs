using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

		public CategoryController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> IndexAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7031/api/Category"); // API URL'si

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData= await responseMessage.Content.ReadAsStringAsync(); // JSON verisini oku
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // JSON verisini nesnelere dönüştür

				//JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData) → JSON string’ini List<ResultCategoryDto> nesnelerine dönüştürür.
                //Buradaki ResultCategoryDto sınıfı JSON alanlarıyla eşleşmelidir.
				return View(values); 
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateCategory()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client= _httpClientFactory.CreateClient(); // Yeni istemci oluşturulur
            var jsonData= JsonConvert.SerializeObject(createCategoryDto); // DTO nesnesi JSON string'ine dönüştürülür
            StringContent stringContent= new StringContent(jsonData,Encoding.UTF8,"application/json"); // JSON içeriği hazırlanır

            var responseMessage= await client.PostAsync("https://localhost:7031/api/Category",stringContent); // POST isteği gönderilir
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            		return View();
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage= await client.DeleteAsync($"https://localhost:7031/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        // İlk önce güncellemek istediğim kategoriyi göstermek için
        public async Task<IActionResult> UpdateCategory(int id)
		{
			var client = _httpClientFactory.CreateClient();
            // İlk önce güncellemek istediğim kategoriyi göstermek için
			var responseMessage = await client.GetAsync($"https://localhost:7031/api/Category/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
				return View(values);
			}
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PutAsync("https://localhost:7031/api/Category", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("UpdateCategory");
			}
			return View();
		}

	}
}
