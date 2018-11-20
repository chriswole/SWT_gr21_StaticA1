using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;

namespace ATM_App2.Events
{
    public abstract class ATMNotification
    {
        public static event EventHandler<NotificationEventArgs> NotificationEvent;

        //abstract public void DetectNotification(Collection<Track> oldTransponderDatas, Collection<Track> newTransponderDatas);

        protected virtual void Notify(NotificationEventArgs e)
        {
            NotificationEvent?.Invoke(this, e);
        }
    }
}
