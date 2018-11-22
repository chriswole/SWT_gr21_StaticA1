using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Classes;
using ATM_App2.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;
using Assert = NUnit.Framework.Assert;

namespace ATM_App2_UnitTest
{

    [TestClass]
    class UnitTestInAirSpace
    {

        private ITrackFactory fakeTrackFactory_;
        private ITrackOpticsProvider fakeOpticsProvider_;
        private InAirSpaceObserver _uut;
        
        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            fakeOpticsProvider_ = Substitute.For<ITrackOpticsProvider>();

            fakeTrackFactory_ = Substitute.For<ITrackFactory>();

            // Inject the fake TDR
            _uut = new InAirSpaceObserver(fakeOpticsProvider_);

            fakeTrackFactory_.TrackCreated += _uut.OnTrackCreated;

        }


        
        #region OnTrackCreated_ResultsEnteringTrack_Tests
        
        [Test]
        public void OnTrackCreated_ResultEnteringTrack_CorrectEnteredTrackEventSent()
        {

            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");

            Track enteredTrack = new Track();
            List<Track> newAirspace = new List<Track>();
            _uut.EnteredTrack += (o, arg) => { enteredTrack = arg.newTrack_; };

            fakeTrackFactory_.TrackCreated
                += Raise.EventWith(this, new TrackArgs(compareTrack));

            Assert.That(enteredTrack == compareTrack, Is.EqualTo(true));
        }

        [Test]
        public void OnTrackCreated_ResultEnteringTrack_BeginWithEmptyAirspaceList_CorrectUpdatedAirSpaceEventSent()
        {

            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");
            

            
            List<Track> newAirspace = new List<Track>();
           
            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };

            fakeTrackFactory_.TrackCreated
                += Raise.EventWith(this, new TrackArgs(compareTrack));


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

            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) =>
            {
                newAirspace = args.TracksInAirSpace;
            };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            Assert.That(newAirspace[0] != _testTracks[0], Is.EqualTo(true));
            Assert.That(newAirspace[0] == _testTracks[_testTracks.Length - 1], Is.EqualTo(true));
        }


        [Test]
        public void OnTrackCreated_ResultsEnteringTrack_BeginWithTracksInAirSpaceList_RightNumberInListSentInUpdateEvent()
        {
            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("BB8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("CC8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
            };

            List<Track> newAirspace = new List<Track>();

            _uut.AirspaceUpdated += (o, args) =>
            {
                newAirspace = args.TracksInAirSpace;
            };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            Assert.That(newAirspace.Count, Is.EqualTo(_testTracks.Length));
        }

        [Test]
        public void OnTrackCreated_ResultsEnteringTrack_RightNumberOfEventsSent_IncludingAirspaceUpdated()
        {
            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("BB8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("CC8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
            };

            int eventcounter = 0;

            List<Track> newAirspace = new List<Track>();

            _uut.EnteredTrack += (o, args) => { eventcounter++; };

            _uut.AirspaceUpdated += (o, args) => { eventcounter++; };

            
            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }

            Assert.That(eventcounter, Is.EqualTo(2 * _testTracks.Length));

        }
        #endregion

        #region OnTrackCreated_ResultsUpdatedAirspace_Tests

        [Test]
        public void OnTrackCreated_ResultsUpdatedAirspace_BeginWithSigleTrackInAirSpacList_SinglTrackInListSentInUpdateEvent()
        {

           Track[] _testTracks = new Track[]
           {
               new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
               new Track("ATR423", new Position(14000, 14000), 14000, 0, 0, "20151006213456790")
           };

           
            List<Track> newAirspace = new List<Track>();
            
            _uut.AirspaceUpdated += (o, args) =>{ newAirspace = args.TracksInAirSpace; };


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
                if (track.tag_ == _testTracks[_testTracks.Length -1].tag_)
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
        //    int eventcounter_Leaving = 0;

            Track LeavingTrack = new Track();
            List<Track> newAirspace = new List<Track>();


            _uut.LeavingTrack += (o, arg) =>
            {
        //        eventcounter_Leaving++;
                LeavingTrack = arg.newTrack_;
            };

            _uut.AirspaceUpdated += (o, args) =>
            {
                eventcounter++;
                newAirspace = args.TracksInAirSpace;
            };


            for(int i = 0; i < 2; i++)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(_testTracks[i]));
            }


            // Assert.That(eventcounter_Leaving, Is.EqualTo(1));
            Assert.That(eventcounter, Is.EqualTo(_testTracks.Length));

            Assert.That(newAirspace.Count == 0, Is.EqualTo(true));
            Assert.That(LeavingTrack.tag_ == _testTracks[1].tag_, Is.EqualTo(true));
            Assert.That(LeavingTrack.pos_ == _testTracks[1].pos_, Is.EqualTo(true));


        }
        
        #endregion
    }
}



