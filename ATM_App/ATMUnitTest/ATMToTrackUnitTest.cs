using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMUnitTest
{
    class ATMToTrackUnitTest
    {

        private ITransponderReceiver _fakeTransponderReceiver;
        private ToTrack _uut;
        [SetUp]
        [Author("Kasper Andersen, Edited by members of group 21")]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            // Inject the fake TDR
            _uut = new ToTrack(_fakeTransponderReceiver);
        }

        [Test]
        public void ParseDataString_Test()
        {
            string[] tokens_;

            string data = "ATR423;39045;8500;14000;20151006213456789";
            string[] rightTokens = new string[] {"ATR423", "39045", "8500", "14000", "20151006213456789"};
            tokens_ = _uut.ParseDataString(data);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

            data = "ATR500;39045;7500;14000;20151006213456789";

             rightTokens = new string[] { "ATR500", "39045", "7500", "14000", "20151006213456789" };
            tokens_ = _uut.ParseDataString(data);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

            data = "ATR600;500;500;14000;20151006213456789";

            rightTokens = new string[] { "ATR600", "500", "500", "14000", "20151006213456789" };
            tokens_ = _uut.ParseDataString(data);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

            data = "ATR700;12000;12000;14000;20151006213456789";

            rightTokens = new string[] { "ATR700", "12000", "12000", "14000", "20151006213456789" };
            tokens_ = _uut.ParseDataString(data);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

            data = "ATR500;8000;7000;14000;20151006213456789";

            rightTokens = new string[] { "ATR500", "8000", "7000", "14000", "20151006213456789" };
            tokens_ = _uut.ParseDataString(data);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(tokens_[i], Is.EqualTo(rightTokens[i]));
            }

        }

        [Test]
        public void BuildTrack_Test()
        {
            string[] tokens1 = new string[] { "ATR423", "39045", "8500", "14000", "20151006213456789" };
            string[] tokens2 = new string[] { "ATR500", "39045", "7500", "14000", "20151006213456789" };
            string[] tokens3 = new string[] { "ATR600", "500", "500", "14000", "20151006213456789" };
            string[] tokens4 = new string[] { "ATR500", "8000", "7000", "14000", "20151006213456789" };
            
            Track[] rightTracks = new Track[]
            {
                new Track( "ATR423", new Position(39045, 8500), 14000, 0, 0, "20151006213456789"), 
                new Track("ATR500", new Position(39045, 7500), 14000, 0, 0,"20151006213456789" ),
                new Track("ATR600", new Position(500,500), 14000,0, 0, "20151006213456789"),
                new Track("ATR500", new Position(8000, 7000), 14000, 0, 0, "20151006213456789"), 
            };

            Track[] tracks = new Track[4];

            tracks[0] = _uut.BuildTrack(tokens1);
            tracks[1] = _uut.BuildTrack(tokens2);
            tracks[2] = _uut.BuildTrack(tokens3);
            tracks[3] = _uut.BuildTrack(tokens4);

            Assert.That(tracks[0] == rightTracks[0], Is.EqualTo(true));
            Assert.That(tracks[1] == rightTracks[1], Is.EqualTo(true));
            Assert.That(tracks[2] == rightTracks[2], Is.EqualTo(true));
            Assert.That(tracks[3] == rightTracks[3], Is.EqualTo(true));

        }

        [Test]
        public void InScope_Test()
        {
            Track[] Tracks = new Track[]
            {
                new Track( "ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR500", new Position(16000, 16000), 14000, 0, 0,"20151006213456789" ),
                new Track("ATR600", new Position(500,500), 14000,0, 0, "20151006213456789"),
                new Track("ATR500", new Position(22000, 22000), 14000, 0, 0, "20151006213456789"),
            };

            Assert.That(_uut.InScope(Tracks[0]), Is.EqualTo(true));
            Assert.That(_uut.InScope(Tracks[1]), Is.EqualTo(true));
            Assert.That(_uut.InScope(Tracks[2]), Is.EqualTo(false));
            Assert.That(_uut.InScope(Tracks[3]), Is.EqualTo(true));
        }





         [Test]
         public void TestDataReception()
         {
             // Setup test data
             List<string> testData = new List<string>();
             testData.Add("ATR423;12000;12000;14000;20151006213456789");
             testData.Add("ATR500;16000;16000;14000;20151006213456789");
             testData.Add("ATR600;500;500;14000;20151006213456789");
             testData.Add("ATR500;22000;22000;14000;20151006213456789");

             Track[] Tracks = new Track[]
             {
                 new Track( "ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                 new Track("ATR500", new Position(16000, 16000), 14000, 0, 0,"20151006213456789" ),
                 new Track("ATR600", new Position(500,500), 14000,0, 0, "20151006213456789"),
                 new Track("ATR500", new Position(22000, 22000), 14000, 0, 0, "20151006213456789"),
             };

            // Act: Trigger the fake object to execute event invocation
             _fakeTransponderReceiver.TransponderDataReady
                 += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
             
             //check only that two Tracks are in the List, first added and last updated:
             Assert.That(_uut.Tracks.Count, Is.EqualTo(2));

             //Chedk that its right tracks:
              Assert.That(_uut.Tracks[0] == Tracks[0], Is.EqualTo(true));
              Assert.That(_uut.Tracks[1] == Tracks[3], Is.EqualTo(true));

             //Chedk that its not the two invalid tracks:
            for (int i = 0; i < _uut.Tracks.Count; i++)
             {
                Assert.That(_uut.Tracks[i] != Tracks[1], Is.EqualTo(true));
                 Assert.That(_uut.Tracks[i] != Tracks[2], Is.EqualTo(true));

            }


        }


    }
}
