using Microsoft.AspNetCore.SignalR;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
	public class SignalRHub:Hub
	{
		SignalRContext context= new SignalRContext();

		public async Task SendCategoryCount()
		{
			var value= context.Categories.Count();
			await Clients.All.SendAsync("ReceiveCategoryCount",value); 
		}

		// client tarafında SendCategoryCount metoduna istek atacak, invoke ile çağıracak bu metodun içindeki ReceiveCategoryCount metoduna value değerini gönderecek
	}
}
