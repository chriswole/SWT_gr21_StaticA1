using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Events;
using ATM_App2.Interfaces;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Channels;
using NUnit.Framework;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace ATM_App2_UnitTest.UnitTestEvents
{
    [TestFixture]
    [Author("Mette Jacobsen, Edited by members of group 21")]
    [TestClass]
    public class UnitTestTrackLeftAirspace
    {
        private TrackLeftAirspace _uut;
        private ILogToFile _log;
        private Track _data1;
        private Track _data2;

        [SetUp]
        public void Setup()
        {
            _uut = new TrackLeftAirspace(_log = Substitute.For<ILogToFile>());
            _data1=new Track("plane1", new Position(20000, 50000), 10000, 50000, 56, "1234872334");
            _data2 = new Track("plane2");
        }

        [Test]
        public void DetectNotification_NoData_NoEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track>(), new Collection<Track>());
            Assert.That(notificationEventCalled, Is.EqualTo(0));
        }

        [Test]
        public void DetectNotification_OneLeaves_OneEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;
            
            _uut.DetectNotification(new Collection<Track>{_data1}, new Collection<Track> ());
            Assert.That(notificationEventCalled, Is.EqualTo(1));
        }
        [Test]
        public void DetectNotification_ThreeLeaves_ThreeEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track> { _data1, _data2, _data1 }, new Collection<Track>());
            Assert.That(notificationEventCalled, Is.EqualTo(3));
        }

        [Test]
        public void DetectNotification_ExistingFlight_NoEvent()
        {
            int notificationEventCalled = 0;
            ATMNotification.NotificationEvent += (sender, args) => notificationEventCalled++;

            _uut.DetectNotification(new Collection<Track> { _data1 }, new Collection<Track> { _data1 });
            Assert.That(notificationEventCalled, Is.EqualTo(0));
        }

        [Test]
        public void DetectNotification_OneTrackEntereds_EventLoggedOnce()
        {
            _uut.DetectNotification(new Collection<Track> { _data1 }, new Collection<Track>());
            _log.Received(1).Log("1234872334 TrackLeftAirspace Notification plane1");
        }
    }
    
}
