using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace ATM_App2_UnitTest.UnitTestClasses
{

    class UnitTestTrackFactory
    {

        private ITransponderReceiver _fakeTransponderReceiver;
        private TrackFactory _uut;
        
        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            // Inject the fake TDR
            _uut = new TrackFactory(_fakeTransponderReceiver, new StringParser());

           

        }

        [Test]
        public void DataReceived_RightNumberOfTracks()
        {

            //setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;12000;12000;14000;20151006213456789");
            testData.Add("ATR500;16000;16000;14000;20151006213456789");
            testData.Add("ATR600;500;500;14000;20151006213456789");
            testData.Add("ATR500;22000;22000;14000;20151006213456789");

            int _eventCounter = 0;
            
            //simulate eventReceiver
            _uut.TrackCreated += (o, arg) =>
            {
                _eventCounter++;
            };

            //act 
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            //Assert
            Assert.That(_eventCounter, Is.EqualTo(testData.Count));
        }

        [Test]
        public void DataReceivedAndTrackSent_RightTrack()
        {

            //setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;12000;12000;14000;20151006213456789");
            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");

            Track track = new Track();
            _uut.TrackCreated += (o, arg) => { track = arg.Track; };

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));


        }

    }
}

