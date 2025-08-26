using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {
        private readonly SignalRContext _context;

        public GenericRepository(SignalRContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity); // varlığı ekle
            _context.SaveChanges(); // ekleme işlemi sonrası değişiklikleri kaydet
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList(); // T tipindeki tüm varlıkları listele
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id); // Find metodu primary key'e göre arama yapar
            
        }

        
    }
}
