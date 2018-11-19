using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ATM_App2_UnitTest.UnitTestClasses
{
    /*
    class UnitTestMonitor
    {
        

        private Monitor _uut;
        private IInAirSpaceObserver fakeAirSpace_;
        private ITrackOpticsProvider fakeOpticsProvider_;


        [SetUp]
        public void setup()
        {
            fakeOpticsProvider_ = Substitute.For<ITrackOpticsProvider>();

            _uut = Substitute.For<ITrackFactory>();

            
            _uut = new InAirSpaceObserver(fakeOpticsProvider_);

            fakeTrackFactory_.TrackCreated += _uut.OnTrackCreated;
        }

        [Test]
        public void initMonitorRecieveData()
        {
            Assert.IsNull(_uut);
        }
        
    }*/
}
