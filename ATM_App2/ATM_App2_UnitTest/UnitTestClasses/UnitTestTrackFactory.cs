using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM_App2_UnitTest.UnitTestClasses
{

    class UnitTestTrackFactory
    {

        private ITransponderReceiver _fakeTransponderReceiver;
        private TrackFactory _uut;
        private IStringParser _fakeStringParser;
        
        //fields for test of BuildTrack method:
        private string[,] _tokens = new string[,]
        {
            {"ATR423", "39045", "8500", "14000", "20151006213456789"},
            { "ATR500", "39045", "7500", "14000", "20151006213456789" },
            { "ATR600", "500", "500", "14000", "20151006213456789" },
            { "ATR500", "8000", "7000", "14000", "20151006213456789" }
        };

        private Track[] _tracks = new Track[]
        {
            new Track( "ATR423", new Position(39045, 8500), 14000, 0, 0, "20151006213456789"),
            new Track("ATR500", new Position(39045, 7500), 14000, 0, 0,"20151006213456789" ),
            new Track("ATR600", new Position(500,500), 14000,0, 0, "20151006213456789"),
            new Track("ATR500", new Position(8000, 7000), 14000, 0, 0, "20151006213456789"),
        };

        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _fakeStringParser = Substitute.For<IStringParser>();

            // Inject the fake TDR
            _uut = new TrackFactory(_fakeTransponderReceiver, _fakeStringParser);
        }

        [Test]
        public void BuildTrack_Test()
        {
         

        }

  





        //[Test]
        //public void TestDataReception()
        //{
        //    // Setup test data
        //    List<string> testData = new List<string>();
        //    testData.Add("ATR423;12000;12000;14000;20151006213456789");
        //    testData.Add("ATR500;16000;16000;14000;20151006213456789");
        //    testData.Add("ATR600;500;500;14000;20151006213456789");
        //    testData.Add("ATR500;22000;22000;14000;20151006213456789");

        //    Track[] Tracks = new Track[]
        //    {
        //         new Track( "ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
        //         new Track("ATR500", new Position(16000, 16000), 14000, 0, 0,"20151006213456789" ),
        //         new Track("ATR600", new Position(500,500), 14000,0, 0, "20151006213456789"),
        //         new Track("ATR500", new Position(22000, 22000), 14000, 0, 0, "20151006213456789"),
        //    };

        //    // Act: Trigger the fake object to execute event invocation
        //    _fakeTransponderReceiver.TransponderDataReady
        //        += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

        //    //check only that two Tracks are in the List, first added and last updated:
        //    Assert.That(_uut.Tracks.Count, Is.EqualTo(2));

        //    //Chedk that its right tracks:
        //    Assert.That(_uut.Tracks[0] == Tracks[0], Is.EqualTo(true));
        //    Assert.That(_uut.Tracks[1] == Tracks[3], Is.EqualTo(true));

        //    //Chedk that its not the two invalid tracks:
        //    for (int i = 0; i < _uut.Tracks.Count; i++)
        //    {
        //        Assert.That(_uut.Tracks[i] != Tracks[1], Is.EqualTo(true));
        //        Assert.That(_uut.Tracks[i] != Tracks[2], Is.EqualTo(true));

        //    }


        //}


    }
}

