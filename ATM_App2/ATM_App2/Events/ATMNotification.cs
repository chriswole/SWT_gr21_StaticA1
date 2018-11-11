//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ATM_App2.Classes
//{
//    public abstract class ATMNotification
//    {
//        public static event EventHandler<NotificationEventArgs> NotificationEvent;

//        abstract public void DetectNotification(ICollection<IATMTransponderData> oldTransponderDatas, ICollection<IATMTransponderData> newTransponderDatas);

//        protected virtual void Notify(NotificationEventArgs e)
//        {
//            NotificationEvent?.Invoke(this, e);
//        }
//    }
//}
