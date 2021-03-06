﻿using System;
using System.Collections.Generic;
using ATM_App2.Events;
using ATM_App2.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework;
using ATM_App2.Interfaces;
using Assert = NUnit.Framework.Assert;
using System.Threading;

namespace ATM_App2_UnitTest.UnitTestClasses
{
    [TestClass]
    public class UnitTestInOutTrackHandler
    {
        private InOutTrackHandler _uut;
        private IInAirSpaceObserver fakeAirSpace_;
        private ILogToFile log_;


        [SetUp]
        public void Setup()
        {
            fakeAirSpace_ = Substitute.For<IInAirSpaceObserver>();
            log_ = Substitute.For<ILogToFile>();
            _uut = new InOutTrackHandler(log_);

            fakeAirSpace_.EnteredTrack += _uut.OnEnteredTrack;
            fakeAirSpace_.LeavingTrack += _uut.OnLeavingTrack;
        }
        [Test]
        public void OnEnteredTrack_TestEvntcalledOnes()
        {
            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");

            int _evenCounter = 0;

            _uut.listInUpdated += (o, args) =>
            {
                _evenCounter++;
                newList = args.listEntered;
            };

            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(_evenCounter, Is.EqualTo(1));

        }

        [Test]
        public void OnEnteredTrack_TestEvntcalledTheeTimes()
        {
            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12050, 10, 34, "2016111912343892");
            Track inserTrack1 = new Track("MCJ523", new Position(15000, 13002), 12400, 10, 34, "2016111932343892");
            Track inserTrack2 = new Track("MCJ523", new Position(15000, 13040), 12300, 10, 34, "2016111945343892");


            int _evenCounter = 0;

            _uut.listInUpdated += (o, args) =>
            {
                _evenCounter++;
                newList = args.listEntered;
            };
            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));
            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack1));
            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack2));

            Assert.That(_evenCounter, Is.EqualTo(newList.Count));
            Assert.That(_evenCounter, Is.EqualTo(3));
            Assert.That(newList.Count, Is.EqualTo(3));
            Assert.That(newList[0] == inserTrack, Is.True);
            Assert.That(newList[1] == inserTrack1, Is.True);
            Assert.That(newList[2] == inserTrack2, Is.True);
        }


        [Test]
        public void OnLeavingTrack_TestEvntcalledOnes()
        {

            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");

            int _evenCounter = 0;

            _uut.listOutUpdated += (o, args) => { _evenCounter++; newList = args.listLeft; };

            fakeAirSpace_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(_evenCounter, Is.EqualTo(newList.Count));

        }

        [Test]
        public void OnLeavingTrack_TestEvntcalledTree()
        {

            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12050, 10, 34, "2016111912343892");
            Track inserTrack1 = new Track("MCJ533", new Position(15000, 13002), 12400, 10, 34, "2016111932343892");
            Track inserTrack2 = new Track("MCJ553", new Position(15000, 13040), 12300, 10, 34, "2016111945343892");

            int _evenCounter = 0;

            _uut.listOutUpdated += (o, args) =>
            {
                _evenCounter++;
                newList = args.listLeft;
            };

            fakeAirSpace_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack));
            fakeAirSpace_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack1));
            fakeAirSpace_.LeavingTrack += Raise.EventWith(this, new TrackArgs(inserTrack2));

            Assert.That(_evenCounter, Is.EqualTo(newList.Count));
            Assert.That(_evenCounter, Is.EqualTo(3));
            Assert.That(newList.Count, Is.EqualTo(3));
            Assert.That(newList[0] == inserTrack, Is.True);
            Assert.That(newList[1] == inserTrack1, Is.True);
            Assert.That(newList[2] == inserTrack2, Is.True);
        }
        [Test]
        public void OnEnteredTrack_TestEvntcalledOnes_TestgetList()
        {
            List<Track> newList = new List<Track>();
            Track inserTrack = new Track("MCJ523", new Position(15000, 13000), 12000, 10, 34, "2016111912343892");

            int _evenCounter = 0;

            _uut.listInUpdated += (o, args) =>
            {
                _evenCounter++;
                newList = args.listEntered;
            };

            fakeAirSpace_.EnteredTrack += Raise.EventWith(this, new TrackArgs(inserTrack));

            Assert.That(_evenCounter, Is.EqualTo(1));
            Assert.That(newList.Count, Is.EqualTo(1));
            Assert.That(newList[0] == inserTrack, Is.True);
        }

    }
}
