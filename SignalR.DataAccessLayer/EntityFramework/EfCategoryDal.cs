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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(SignalRContext context) : base(context)
        {
        }

		public int CategoryCount() //Kategorinin sayısını dönecek olan sınıf 
		{
			using var context = new SignalRContext();
			return context.Categories.Count();
		}
		public int ActiveCategoryCount()
		{
			using var context = new SignalRContext();
			return context.Categories.Count(x => x.CategoryStatus == true);	
		}

		public int PassiveCategoryCount()
		{
			using var context = new SignalRContext();
			return context.Categories.Count(x => x.CategoryStatus == false);
		}
	}
}
