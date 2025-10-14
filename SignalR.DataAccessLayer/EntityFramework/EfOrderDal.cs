using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
	public class EfOrderDal : GenericRepository<Order>, IOrderDal
	{
		public EfOrderDal(SignalRContext context) : base(context)
		{
		}

		public int ActiveOrderCount()
		{
			using var context = new SignalRContext();
			return context.Orders.Count(o => o.Description=="Müşteri Masada");
		}

		public decimal LastOrderPrice()
		{
			using var context = new SignalRContext();
			return context.Orders.OrderByDescending(o => o.OrderId).Take(1).Select(o => o.TotalPrice).FirstOrDefault();
		}

		public decimal TodayTotalPrice()
		{
			using var con = new SignalRContext();
			// Bugünün başlangıcı (00:00)
			var today = DateTime.Today;

			// Yarın (bugünün bitişi)
			var tomorrow = today.AddDays(1);

			// Bugünün toplam tutarı
			var total = con.Orders
						   .Where(o => o.OrderDate >= today && o.OrderDate < tomorrow && o.Description=="Hesap Kapatıldı")
						   .Sum(o => (decimal?)o.TotalPrice) ?? 0;
			return total;
		}

		public int TotalOrderCount()
		{
			using var context = new SignalRContext();
			return context.Orders.Count();
		}
	}
}
