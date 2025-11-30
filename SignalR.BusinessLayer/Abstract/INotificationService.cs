using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface INotificationService:IGenericService<Notification>
    {
        int TNotificationCountByStatusFalse();
        List<Notification> TGetAllNotificationByFalse();
		void TUpdate(Discount value);

		void TNotificationStatusChangeToTrue(int id); //bildirim türünü true yapar
		void TNotificationStatusChangeToFalse(int id);
	}
}
