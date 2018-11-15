using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Channels;
using System.Threading;
using ATM_App2.Classes;
using NUnit.Framework;
using NSubstitute;
using ATM_App2.Events;
using ATM_App2.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;
namespace ATM_App2_UnitTest.UnitTestEvents
{
    [TestClass]
    public class UnitTestNotificationEventArgs
    {
        private NotificationEventArgs _notificationEventArgs;

        [SetUp]
        public void Setup()
        {
            _notificationEventArgs = new NotificationEventArgs("F12", "testEvent", "20156789", 100);

        }

        [Test]
     public void StopMeEvent_NotCalled_Before100milliSecond()
        {
            AutoResetEvent eventRaised = new AutoResetEvent(false);
            _notificationEventArgs.StopMeEvent += (sender, eventArgs) => { eventRaised.Set(); };
            Assert.IsFalse(eventRaised.WaitOne(TimeSpan.FromMilliseconds(1)));
        }

        [Test]
       public void StopMeEvent_IsCalled_After100milliSecond()
        {
            AutoResetEvent eventRaised = new AutoResetEvent(false);
            _notificationEventArgs.StopMeEvent += (sender, eventArgs) =>
            {
                if (((NotificationEventArgs)sender).Timestamp == "20156789")
                    eventRaised.Set();
            };
            Assert.IsTrue(eventRaised.WaitOne(TimeSpan.FromMilliseconds(300)));
        }
    }
}
