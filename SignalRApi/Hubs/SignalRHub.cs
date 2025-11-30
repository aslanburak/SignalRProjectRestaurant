using Azure.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
	namespace SignalRApi.Hubs
	{
		public class SignalRHub : Hub
		{

			private readonly ICategoryService _categoryService;
			private readonly IProductService _productService;
			private readonly IOrderService _orderService;
			private readonly IMoneyCaseService _moneyCaseService;
			private readonly IMenuTableService _menuTableService;
			private readonly IBookingService _bookingService;
			private readonly INotificationService _notificationService;

            public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
            {
                _categoryService = categoryService;
                _productService = productService;
                _orderService = orderService;
                _moneyCaseService = moneyCaseService;
                _menuTableService = menuTableService;
                _bookingService = bookingService;
                _notificationService = notificationService;
            }

            public async Task SendDashboard()
			{
				var categoryCount = _categoryService.TCategoryCount();
				var productCount = _productService.TProductCount();
				var activeCategoryCount = _categoryService.TActiveCategoryCount();
				var passiveCategoryCount= _categoryService.TPassiveCategoryCount();
				var productCountByCategoryNameHamburger = _productService.TProductCountByCategoryNameHamburger();
				var productCountByCategoryNameDrink = _productService.TProductCountByCategoryNameDrink();
				var productPriceAvg = _productService.TProductPriceAvg();
				var productNameByMaxPrice = _productService.TProductNameByMaxPrice();
				var productNameByMinPrice = _productService.TProductNameByMinPrice();
				var totalOrderCount = _orderService.TTotalOrderCount();

				var activeOrderCount = _orderService.TActiveOrderCount();
				var lastOrderPrice= _orderService.LastOrderPrice();
				var totalMoneyCaseAmount = _moneyCaseService.TTotalMoneyCaseAmount();
				var todayTotalPrice = _orderService.TodayTotalPrice();
				var menuTableCount = _menuTableService.TMenuTableCount();
				

				await Clients.All.SendAsync("ReceiveDashboard", new
				{
					CategoryCount = categoryCount,
					ProductCount = productCount,
					ActiveCategoryCount = activeCategoryCount,
					PassiveCategoryCount = passiveCategoryCount,
					ProductCountByCategoryNameHamburger = productCountByCategoryNameHamburger,
					ProductCountByCategoryNameDrink = productCountByCategoryNameDrink,
					ProductPriceAvg = productPriceAvg,
					ProductNameByMaxPrice = productNameByMaxPrice,
					ProductNameByMinPrice = productNameByMinPrice,
					TotalOrderCount = totalOrderCount,
					ActiveOrderCount = activeOrderCount,
					LastOrderPrice = lastOrderPrice,
					TotalMoneyCaseAmount = totalMoneyCaseAmount,
					TodayTotalPrice = todayTotalPrice,
					MenuTableCount = menuTableCount

				});
			}


			public async Task SendProgress()
			{
                var totalMoneyCaseAmount = _moneyCaseService.TTotalMoneyCaseAmount();
                var activeOrderCount = _orderService.TActiveOrderCount();
				var menuTableCount= _menuTableService.TMenuTableCount();


                await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", new
				{
					TotalMoneyCaseAmount= totalMoneyCaseAmount,
                    ActiveOrderCount = activeOrderCount,
					MenuTableCount= menuTableCount,

                });
            }


			public async Task GetBookingList() {
			
				var value = _bookingService.TGetList();
                await Clients.All.SendAsync("ReceiveBookingList", value);
			}

			public async Task SendNotification()
			{
				var value = _notificationService.TNotificationCountByStatusFalse();
				await Clients.All.SendAsync("ReceiveNotificationCountByFalse", value);

				var notificationListByFalse= _notificationService.TGetAllNotificationByFalse();
				await Clients.All.SendAsync("ReceiveNotificationListByFalse", notificationListByFalse);
			}
		}
	}
}


// client tarafında SendCategoryCount metoduna istek atacak, invoke ile çağıracak
// bu metodun içindeki ReceiveCategoryCount metoduna value değerini gönderecek