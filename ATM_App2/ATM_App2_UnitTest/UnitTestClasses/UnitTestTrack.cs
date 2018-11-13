using System;
using ATM_App2.Classes;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ATMTrackUnitTest
{
    [TestFixture]
    [Author("Mette Jacobsen, Edited by members of group 21")]


    class TrackTest
    {

        Track uut;
        string tag = "MJ2412", timestamp = "20181224";
        Position pos = new Position(50000, 40000);
        int altitude = 20000, hori_velocity = 400, course = 90;


        [SetUp] //Husk stort U!
        public void Setup()
        {
            uut = new Track(tag, pos, altitude, hori_velocity, course, timestamp);



        }

        [Test]
        public void TrackConstrucctorAndGetTest()
        {
            Assert.That(uut.tag_, Is.EqualTo(tag));
            Assert.That(uut.pos_, Is.EqualTo(pos));
            Assert.That(uut.altitude_, Is.EqualTo(altitude));
            Assert.That(uut.hori_velocity_, Is.EqualTo(hori_velocity));
            Assert.That(uut.course_, Is.EqualTo(course));
            Assert.That(uut.timestamp_, Is.EqualTo(timestamp));

        }
        /*
        [Test]
        public void TrackSetAndGetTest()
        {

            Position pos2 = new Position(2000, 2000);
            string tag2 = "Mc238";
            int altitude2 = 10000;
            int hori_velocity2 = 200;
            int course2 = 89;
            string timestamp2 = "20180423";

            uut.tag_ = tag2;
            uut.pos_ = pos2;
            uut.altitude_ = altitude2;
            uut.hori_velocity_ = hori_velocity2;
            uut.course_ = course2;
            uut.timestamp_ = timestamp2;

            Assert.That(uut.tag_, Is.EqualTo(tag2));
            Assert.That(uut.pos_, Is.EqualTo(pos2));
            Assert.That(uut.altitude_, Is.EqualTo(altitude2));
            Assert.That(uut.hori_velocity_, Is.EqualTo(hori_velocity2));
            Assert.That(uut.course_, Is.EqualTo(course2));
            Assert.That(uut.timestamp_, Is.EqualTo(timestamp2));

        }

        [Test]
        public void Equals_Operator_Result_True()
        {
            //Test true after default ctor:
            bool result;
            Track track1 = new Track();
            Track track2 = new Track();
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(true));

            //Test true after explicit ctor:
            track1 = new Track(tag, pos, altitude, hori_velocity, course, timestamp);
            track2 = new Track(tag, pos, altitude, hori_velocity, course, timestamp);

            result = track1 == track2;
            Assert.That(result, Is.EqualTo(true));

            //Test true self comparison
            result = track1 == track1;
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Equals_Operator_Result_False()
        {
            bool result;

            //Test false  on one diffferent param:
            Track track1 = new Track();
            Track track2 = new Track(tag: "tag");
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

            track2 = new Track(pos: new Position(100, 100));
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

            track2 = new Track(altitude: 100);
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

            track2 = new Track(horiVelocity: 100);
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

            track2 = new Track(course: 100);
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

            track2 = new Track(timestamp: "Timestamp");
            result = track1 == track2;
            Assert.That(result, Is.EqualTo(false));

        }

        [Test]
        public void Not_Equals_Operator_Result_True()
        {
            bool result;

            //Test true on one different param:
            Track track1 = new Track();
            Track track2 = new Track(tag: "tag");
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

            track2 = new Track(pos: new Position(100, 100));
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

            track2 = new Track(altitude: 100);
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

            track2 = new Track(horiVelocity: 100);
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

            track2 = new Track(course: 100);
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

            track2 = new Track(timestamp: "Timestamp");
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(true));

        }


        public void Not_Equals_Operator_Result_False()
        {
            //Test true after default ctor:
            bool result;
            Track track1 = new Track();
            Track track2 = new Track();
            result = track1 != track2;
            Assert.That(result, Is.EqualTo(false));

            //Test true after explicit ctor:
            track1 = new Track(tag, pos, altitude, hori_velocity, course, timestamp);
            track2 = new Track(tag, pos, altitude, hori_velocity, course, timestamp);

            result = track1 != track2;
            Assert.That(result, Is.EqualTo(false));

            //Test true self comparison
            result = track1 != track1;
            Assert.That(result, Is.EqualTo(false));
        }*/

    }

}