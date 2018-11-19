using System;
using System.Collections.Generic;
using ATM_App2.Events;
using ATM_App2.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework;
using ATM_App2.Interfaces;
using Assert = NUnit.Framework.Assert;

namespace ATM_App2_UnitTest.UnitTestClasses
{
    [TestClass]
    public class UnitTestInOutTrack
    {
        private InOutTrack _uut;
        private IInAirSpaceObserver fakeAirSpace_;


        [SetUp]
        public void Setup()
        {
            fakeAirSpace_ = Substitute.For<IInAirSpaceObserver>();
            _uut = new InOutTrack();
            fakeAirSpace_.EnteredTrack += _uut.OnEnteredTrack;
            fakeAirSpace_.LeavingTrack += _uut.OnLeavingTrack;
        }
        [Test]
        public void OnEnteredTrack_TestEmptyList_OneTrackAdded()
        {
            List<Track> newList = new List<Track>(); 
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");
            //Track enteredTrack = new Track();
           
            _uut.listInUpdated += (o, args) => { newList = args.listEntered; };

            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(newList[0] == inserTrack, Is.EqualTo(true));
          
            //update list, kommer der et nyt track ind i en tom list når event bliver kaldt en gang?

        }
        //[Test]
        //public void TestMethod1()
        //{
        //    //update list, kommer der nye tracks ind i en tom list når event bliver kaldt tre gange?

        //}

        //[Test]
        //public void TestMethod1()
        //{
        //    //update list, kommer der et nyt track ind i en ikke tom list når event bliver kaldt en gang?

        //}
        //[Test]
        //public void TestMethod1()
        //{
        //    //update list, kommer der nye track ind i en ikke tom list når event bliver kaldt tre gang?

        //}


    }
}
