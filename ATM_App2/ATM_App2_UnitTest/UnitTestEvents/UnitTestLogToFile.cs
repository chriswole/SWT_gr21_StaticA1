using System;
using System.IO;
using System.Linq;
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
    public class UnitTestLogToFile
    {
        private LogToFile _uut;
        private IInAirSpaceObserver fakeInAirSpaceObserver_;
        private ISeparation fakeSeparation_;

        [SetUp]
        public void Setup()
        {
            _uut = new LogToFile();
            fakeInAirSpaceObserver_ = Substitute.For<IInAirSpaceObserver>();
            fakeSeparation_ = Substitute.For<ISeparation>();

        }

        [Test]
        public void Log_CheckFileExists_ReturnTrue()
        {
            if (File.Exists("SeparationLog.txt"))
                File.Delete("SeparationLog.txt");
            _uut.Log("hello");
            var fileExists = (File.Exists("SeparationLog.txt"));
            Assert.That(fileExists, Is.EqualTo(true));
        }

        [Test]
        public void Log_CheckTextAppend_ReturnHello()
        {
            File.WriteAllText("SeparationLog.txt", string.Empty);
            _uut.Log("Hello");
            Assert.That((File.ReadLines("SeparationLog.txt")).Last().Contains("Hello"), Is.True);
        }

        [Test]
        public void Log_CheckLogDanger_RetrunTrue()
        {

        }
        [Test]
        public void OnAirSpaceUpdated_1SeparationInList_1DangersMade()
        {
            // Arrange 
            List<Danger> Dangerlist = new List<Danger>();
            List<Track> trackList = new List<Track>();
            Track track1 = new Track("track1", new Position(15000, 40000), 500, 1000, 30, "20180403103240");
            Track track2 = new Track("track2", new Position(50000, 20000), 850, 1000, 120, "20180403103241");
            Track track3 = new Track("track3", new Position(80000, 65000), 4700, 1000, 210, "20180403103243");
            Track track4 = new Track("track4", new Position(12000, 41000), 300, 1000, 300, "20180403103245");
            Danger testdanger = new Danger(track1, track4, 3162); // distance calculated in mathcad. 
            
            Dangerlist.Add(testdanger);
         //  string testlog=(File.ReadLines("SeparationLog.txt")).Last();
           // Console.WriteLine(testlog);
 
            File.WriteAllText("SeparationLog.txt", string.Empty);
            _uut.Log(testdanger);
            Assert.That((File.ReadLines("SeparationLog.txt")).Last().Contains(testdanger.track1_.tag_+ " and "+ testdanger.track2_.tag_ + " Distance: 3162"), Is.True);
          
        }
       
    }
}
