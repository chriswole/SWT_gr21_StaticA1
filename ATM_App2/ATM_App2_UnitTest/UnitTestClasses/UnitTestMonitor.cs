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

        private Monitor _uut;
        private IInAirSpaceObserver fakeAirSpace_;

        [SetUp]
        public void Setup()
        {
            fakeAirSpace_ = Substitute.For<IInAirSpaceObserver>();
            _uut = new Monitor();



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

    }



}
