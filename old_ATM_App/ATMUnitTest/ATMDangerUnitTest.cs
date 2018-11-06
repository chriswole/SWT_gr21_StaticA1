using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Interfaces;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    class ATMDangerUnitTest
    {
        public Danger _uut;

        public Track track1_ { get; }
        public Track track2_ { get; }
        public int distance_ { get; }

        
        Track track1;
        Track track2;
        int dist;


        [SetUp]
        public void Setup()
        {
            Position pos1= new Position(20110,54000);
            Position pos2= new Position(54210, 48214);
            track1 = new Track("TAG4S", pos1, 10000, 587, 85,"20180215");
            track2 = new Track("KRS8S", pos2, 14005, 421, 254, "20181005");
            dist = 2500;
            _uut = new Danger(track1, track2,dist );
        }

        
        [Test]
        public void DangerConstructorAndGetTest()
        { 
            Assert.That(_uut.track1_, Is.EqualTo(track1));
            Assert.That(_uut.track2_, Is.EqualTo(track2));
            Assert.That(_uut.distance_, Is.EqualTo(dist));
        }
    }
}
