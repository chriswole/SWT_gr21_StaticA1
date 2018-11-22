using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;
using Assert = NUnit.Framework.Assert;


namespace ATM_App2_UnitTest.UnitTestClasses
{
    
    [TestClass]
    class UnitTestMonitor
    {

        private InOutTrackHandler _uut;
        private IInAirSpaceObserver fakeAirSpace_;
        private ILogToFile log_;

        [SetUp]
        public void Setup()
        {
            fakeAirSpace_ = Substitute.For<IInAirSpaceObserver>();
            log_ = Substitute.For<ILogToFile>();
            _uut = new InOutTrackHandler();

            fakeAirSpace_.EnteredTrack += _uut.OnEnteredTrack;
            fakeAirSpace_.LeavingTrack += _uut.OnLeavingTrack;

        }

        [Test]
        public void MonitorErrorOnNull()
        {
            List<Monitor> airspaceMonitored = new List<Monitor>();
            List<Monitor> updatedAirspaceMonitored = new List<Monitor>();

            Assert.IsEmpty(airspaceMonitored);
        }
        [Test]
        public void ExtendingMonitoredUnits()
        {

            List<Monitor> airspaceMonitored = new List<Monitor>();
            List<Monitor> updatedAirspaceMonitored = new List<Monitor>();
           

            airspaceMonitored.Add(new Monitor());
            
            updatedAirspaceMonitored.AddRange(airspaceMonitored);
            updatedAirspaceMonitored.ForEach(Console.WriteLine);

            Assert.That(updatedAirspaceMonitored.Any( x => airspaceMonitored.Any(y => x.Equals(y))));
        }
        /*
        [Test]
        public void PlanesEnteringAirspace()
        {


            
            List<Track> recievedPlane = new List<Track>();
            InOutTrack updateMonitor = new InOutTrack();



            updateMonitor.listInUpdated += delegate(object sender, EnteredTrackArgs e)
            {
                recievedPlane.Add(_uut.OnEnteredTrack());
            };

            Assert.AreEqual(2, recievedPlane.Count);
            Assert.AreEqual("RecievedPlane", recievedPlane[0]);

            

            
            InOutTrack entering = new InOutTrack();
            

            
            InOutTrack leaving = new InOutTrack();
            leaving.listOutUpdated += new System.EventHandler<LeftTrackArgs>(OnDeleted);


            
            monitoring.DangerListUpdated += new EventHandler<DangerlistArgs>(OnNewDanger);
            
        }

        private void OnEntered(object sender, EnteredTrackArgs e)
        {
        }

        private void OnDeleted(object sender, LeftTrackArgs e)
        {
        }

        private void OnNewDanger(object sender, DangerlistArgs e)
        {
        }*/
    }



}
