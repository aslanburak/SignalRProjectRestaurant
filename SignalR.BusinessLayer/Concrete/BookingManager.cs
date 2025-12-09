using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public void TAdd(Booking entity)
        {
            _bookingDal.Add(entity);
        }

        public void TBookingStatusApproved(int id)
        {
            _bookingDal.BookingStatusApproved(id);
        }

        public void TBookingStatusCancelled(int id)
        {
            _bookingDal.BookingStatusCancelled(id);
        }

        public void TDelete(Booking entity)
        {
            _bookingDal.Delete(entity);
        }

        public Booking TGetById(int id)
        {
            return _bookingDal.GetById(id);
        }

        public List<Booking> TGetList()
        {
            return _bookingDal.GetAll();
        }

        public void TUpdate(Booking entity)
        {
            _bookingDal.Update(entity);
        }
    }
}
