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
    public class EfAboutDal : GenericRepository<About>, IAboutDal // Abouta özel metotlar eklenebilir
    {
        public EfAboutDal(SignalRContext context) : base(context) // base classın constructorını çağırır
        {
            //GenericRepository de bir constructor oluşturmuştuk, burda da ondan miras aldığımız için base ile çağırıyoruz
            // Constructor içinde → üst sınıfın constructor’ını çağırmak için base anahtar kelimesi kullanılır.

        }
    }
}
