using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework.Internal;


namespace ATMUnitTest
{
    [TestFixture]
    class ATMSeparationUnitTest
    {
       

        ISeparation TestSeparation;
        List<Track> trackList;
        Position pos1, pos2, pos3, pos4;
        Track T1, T2, T3, T4;
        Danger D1;


        [SetUp]
        public void Setup()
        {
            TestSeparation = new Separation();
            trackList = new List<Track>();
            pos1 = new Position(27541, 25884);
            pos2 = new Position(22513, 75141);
            pos3 = new Position(15245, 24154);
            pos4 = new Position(30245, 28884);
            T1 = new Track("H5JS", pos1, 5422, 251, 150, "20181005");
            T2 = new Track("J8HD", pos2, 2481, 241, 45, "20180528");
            T3 = new Track("K9JR", pos3, 5385, 301, 126, "20180321");
            T4 = new Track("N8DY", pos4, 5632, 130, 75, "20180421");
            trackList.Add(T1);
            trackList.Add(T2);
            trackList.Add(T3);
            trackList.Add(T4);
            D1 = new Danger(T1,T4,4038);//  Yes i calculated it myself.
            // Skal test ingen danger 
            // alt too close, dist out of range
            // alt too close dist too close , not same tag

        }

        [Test]
        public void TrackAlarmTrue()
        {
            //Testing if Old and new Dangers are Equal
            Assert.AreEqual(TestSeparation.newDangers_, TestSeparation.OldDangers_);
        }


        [Test]
        public void TrackAlarmFalse()
        {
            //Adding elements to lists
            TestSeparation.newDangers_.Add(new Danger(T1, T2, 5000));
            TestSeparation.OldDangers_.Add(new Danger(T3, T2, 4000));

            //Testing if Old and new dangers are Equal
            Assert.AreNotEqual(TestSeparation.newDangers_, TestSeparation.OldDangers_);

            //Cleaning up Lists
            TestSeparation.newDangers_.Clear();
            TestSeparation.OldDangers_.Clear();
        }

        [Test]
        public void ClearingDeactivateAlarm()
        {
            //Emptying Old danger list
            TestSeparation.OldDangers_.Clear();

            //Testing if list is empty
            CollectionAssert.IsEmpty(TestSeparation.OldDangers_);
        }

        [Test]
        public void TestCalculateDistance()
        {
            TestSeparation.calculateDistances(trackList);

            Assert.That(TestSeparation.newDangers_,Is.Not.Empty);
            Assert.That(TestSeparation.newDangers_.Count,Is.EqualTo(2));
            Assert.That(TestSeparation.newDangers_[0]==D1,Is.EqualTo(true));
            /*
            foreach (var Dang in TestSeparation.newDangers_)
            {
                Dang.print();
            }*/

            

        }

    }

}

