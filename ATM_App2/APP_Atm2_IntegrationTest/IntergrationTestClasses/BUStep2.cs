﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using TransponderReceiver;
using NSubstitute;

namespace APP_Atm2_IntegrationTest.IntergrationTestClasses
{
    [TestFixture]
    class BUStep2
    {
        private ITransponderReceiver receiver_;
        private IStringParser parser_;
        private ITrackOpticsProvider opticsProvider_;
        private ITrackFactory factory_;
        private InAirSpaceObserver inAirSpaceObserver_;
        

        [SetUp]
        public void Setup()
        {
            receiver_ = Substitute.For<ITransponderReceiver>();
            parser_ = new StringParser();
            opticsProvider_ = new TrackOpticsProvider();
            factory_ = new TrackFactory(receiver_, parser_);
            inAirSpaceObserver_ = new InAirSpaceObserver(opticsProvider_);

            factory_.TrackCreated += inAirSpaceObserver_.OnTrackCreated;
        }

        #region OnTrackCreated_ResultsEnteringTrack_Tests

        [Test]
        public void OnTrackCreated_ResultEnteringTrack_CorrectEnteredTrackEventSent()
        {

            List<string> testData_ = new List<string>( ){"ATR423;12000;12000;14000;20151006213456789",};
            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");

            Track enteredTrack = new Track();
           
            inAirSpaceObserver_.EnteredTrack += (o, arg) => { enteredTrack = arg.newTrack_; };


            receiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData_));

            Assert.That(enteredTrack == compareTrack, Is.EqualTo(true));
        }

        [Test]
        public void OnTrackCreated_ResultEnteringTrack_BeginWithEmptyAirspaceList_CorrectUpdatedAirSpaceEventSent()
        {
            List<string> testData_ = new List<string>() { "ATR423;12000;12000;14000;20151006213456789", };
            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");

            
            List<Track> newAirspace = new List<Track>();

            inAirSpaceObserver_.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };

            receiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData_));


            Assert.That(newAirspace.Count, Is.EqualTo(1));
            Assert.That(newAirspace[0] == compareTrack, Is.EqualTo(true));
        }

        [Test]
        public void OnTrackCreated_ResultEnteringTrack_BeginWithTracksInAirSpaceList_LastTrackCreatedFirstInListSentInUpdateEvent()
        {
            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("BB8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("CC8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
            };

            List<string> testData_ = new List<string>()
            {
                "ATR423;12000;12000;14000;20151006213456789",
                "BB8832;12000;12000;14000;20151006213456789",
                "CC8832;12000;12000;14000;20151006213456789"
            };

            List<Track> newAirspace = new List<Track>();

            inAirSpaceObserver_.AirspaceUpdated += (o, args) =>
            {
                newAirspace = args.TracksInAirSpace;
            };

            receiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData_));

            Assert.That(newAirspace[0] != _testTracks[0], Is.EqualTo(true));
            Assert.That(newAirspace[0] == _testTracks[_testTracks.Length - 1], Is.EqualTo(true));
        }



        [Test]
        public void OnTrackCreated_ResultsEnteringTrack_BeginWithTracksInAirSpaceList_RightNumberInListSentInUpdateEvent()
        {
            
            List<string> testData_ = new List<string>()
            {
                "ATR423;12000;12000;14000;20151006213456789",
                "BB8832;12000;12000;14000;20151006213456789",
                "CC8832;12000;12000;14000;20151006213456789"
            };

            List<Track> newAirspace = new List<Track>();

            inAirSpaceObserver_.AirspaceUpdated += (o, args) =>
            {
                newAirspace = args.TracksInAirSpace;
            };

            receiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData_));

            Assert.That(newAirspace.Count, Is.EqualTo(testData_.Count));
        }

        

        [Test]
        public void OnTrackCreated_ResultsEnteringTrack_RightNumberOfEventsSent_IncludingAirspaceUpdated()
        {
            List<string> testData_ = new List<string>()
            {
                "ATR423;12000;12000;14000;20151006213456789",
                "BB8832;12000;12000;14000;20151006213456789",
                "CC8832;12000;12000;14000;20151006213456789"
            };

            int eventcounter = 0;

            List<Track> newAirspace = new List<Track>();

            inAirSpaceObserver_.EnteredTrack += (o, args) => { eventcounter++; };

            inAirSpaceObserver_.AirspaceUpdated += (o, args) => { eventcounter++; };


            receiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData_));

            Assert.That(eventcounter, Is.EqualTo(2 * testData_.Count));

        }
       #endregion

       /* #region OnTrackCreated_ResultsUpdatedAirspace_Tests
        
        
        [Test]
        public void OnTrackCreated_ResultsUpdatedAirspace_BeginWithSigleTrackInAirSpacList_SinglTrackInListSentInUpdateEvent()
        {

            Track[] _testTracks = new Track[]
            {
               new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
               new Track("ATR423", new Position(14000, 14000), 14000, 0, 0, "20151006213456790")
            };


            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            Assert.That(newAirspace.Count, Is.EqualTo(1));

        }

        [Test]
        public void OnTrackCreated_ResultsUpdatedAirspace_BeginWithSigleTrackInAirSpacList_TrackInListHasUpdatedCourseAndVelocity()
        {

            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR423", new Position(14000, 12000), 14000, 0, 0, "20151006213456790")
            };


            fakeOpticsProvider_.GetTrackCourse(_testTracks[1], _testTracks[0]).Returns(90);
            fakeOpticsProvider_.GetTrackVelocity(_testTracks[1], _testTracks[0]).Returns(2000);


            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            Assert.That(newAirspace[0] != _testTracks[0]);
            Assert.That(newAirspace[0].hori_velocity_, Is.EqualTo(2000));
            Assert.That(newAirspace[0].course_, Is.EqualTo(90));
        }

        [Test]
        public void OnTrackCreated_ResultsUpdatedAirspace_TracksAlreadyInAirSpacList_UpdatedTrackIsOnlyInListOnce()
        {

            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("BB423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR423", new Position(14000, 12000), 14000, 0, 0, "20151006213456790")
            };


            fakeOpticsProvider_.GetTrackCourse(Arg.Any<Track>(), Arg.Any<Track>()).ReturnsForAnyArgs(90);
            fakeOpticsProvider_.GetTrackVelocity(Arg.Any<Track>(), Arg.Any<Track>()).ReturnsForAnyArgs(2000);


            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            int trackCount = 0;

            foreach (var track in newAirspace)
            {
                if (track.tag_ == _testTracks[_testTracks.Length - 1].tag_)
                {
                    trackCount++;
                }
            }

            Assert.That(trackCount, Is.EqualTo(1));
        }

        [Test]
        public void OnTrackCreated_ResultsUpdatedAirspace_TracksAlreadyInAirSpacList_UpdatedTrackIsFirstInListSent()
        {

            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("BB423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR423", new Position(14000, 12000), 14000, 0, 0, "20151006213456790")
            };


            fakeOpticsProvider_.GetTrackCourse(Arg.Any<Track>(), Arg.Any<Track>()).ReturnsForAnyArgs(90);
            fakeOpticsProvider_.GetTrackVelocity(Arg.Any<Track>(), Arg.Any<Track>()).ReturnsForAnyArgs(2000);


            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }


            Assert.That(newAirspace[0].tag_ == _testTracks[_testTracks.Length - 1].tag_, Is.EqualTo(true));
        }

        #endregion

        #region OnTrackCreated_ResultsLeavingTrack_Tests


        [Test]
        public void OnTrackCreated_ResultsTrackLeaving_BeginWithSigleTrackInAirSpacList_CorrectlyHandled()
        {

            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR423", new Position(0, 0), 14000, 0, 0, "20151006213456789"),
            };

            int eventcounter = 0;

            Track LeavingTrack = new Track();
            List<Track> newAirspace = new List<Track>();


            _uut.LeavingTrack += (o, arg) => { LeavingTrack = arg.newTrack_; };

            _uut.AirspaceUpdated += (o, args) =>
            {
                eventcounter++;
                newAirspace = args.TracksInAirSpace;
            };


            for (int i = 0; i < 2; i++)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(_testTracks[i]));
            }



            Assert.That(eventcounter, Is.EqualTo(_testTracks.Length));

            Assert.That(newAirspace.Count == 0, Is.EqualTo(true));
            Assert.That(LeavingTrack.tag_ == _testTracks[1].tag_, Is.EqualTo(true));
            Assert.That(LeavingTrack.pos_ == _testTracks[1].pos_, Is.EqualTo(true));


        }

        #endregion */
    } 
}
