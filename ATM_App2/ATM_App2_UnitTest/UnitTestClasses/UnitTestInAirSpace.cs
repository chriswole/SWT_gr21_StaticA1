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
        private InAirSpace _uut;
        
        [SetUp]
        [Author("Kasper Andersen")]
        public void Setup()
        {
            fakeOpticsProvider_ = Substitute.For<ITrackOpticsProvider>();

            fakeTrackFactory_ = Substitute.For<ITrackFactory>();

            // Inject the fake TDR
            _uut = new InAirSpace(fakeOpticsProvider_);

            fakeTrackFactory_.TrackCreated += _uut.OnTrackCreated;

        }

        [Test]
        public void CorrectEnteredTrackEventSent()
        {

            Track compareTrack = new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789");



            int eventcounter = 0;
            Track enteredTrack = new Track();
            List<Track> newAirspace = new List<Track>();
            _uut.EnteredTrack += (o, arg) =>
            {
                enteredTrack = arg.newTrack_;
                eventcounter++;
            };
            _uut.AirspaceUpdated += (o, args) => { newAirspace = args.TracksInAirSpace; };

            fakeTrackFactory_.TrackCreated
                += Raise.EventWith(this, new TrackArgs(compareTrack));
            

            Assert.That(eventcounter, Is.EqualTo(1));

            Assert.That(enteredTrack == compareTrack, Is.EqualTo(true));
            Assert.That(newAirspace[0] == compareTrack, Is.EqualTo(true));


        }

        [Test]
        public void CorrectAirUpdatedEventSent()
        {

           Track[] _testTracks = new Track[]
           {
               new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
               new Track("BB8832", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
               new Track("ATR423", new Position(14000, 14000), 14000, 0, 0, "20151006213456790")
           };

            int eventcounter = 0;
           
            List<Track> newAirspace = new List<Track>();
            
            _uut.AirspaceUpdated += (o, args) =>
            {
                eventcounter++;
                newAirspace = args.TracksInAirSpace;
            };


            foreach (var track in _testTracks)
            {
                fakeTrackFactory_.TrackCreated
                    += Raise.EventWith(this, new TrackArgs(track));
            }
            


            Assert.That(eventcounter, Is.EqualTo(_testTracks.Length));

            Assert.That(newAirspace[0] != _testTracks[0], Is.EqualTo(true));
            Assert.That(newAirspace.Count, Is.EqualTo(2));
            Assert.That(newAirspace[0].tag_ == _testTracks[0].tag_, Is.EqualTo(true));


        }

    /*    [Test]
        public void CorrectLeavingTrackEventSent()
        {

            Track[] _testTracks = new Track[]
            {
                new Track("ATR423", new Position(12000, 12000), 14000, 0, 0, "20151006213456789"),
                new Track("ATR423", new Position(0, 0), 14000, 0, 0, "20151006213456789"),
            };

            int eventcounter = 0;

            Track LeavingTrack = new Track();
            List<Track> newAirspace = new List<Track>();


            _uut.EnteredTrack += (o, arg) => { LeavingTrack = arg.newTrack_; };

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



            Assert.That(eventcounter, Is.EqualTo(_testTracks.Length));

            Assert.That(newAirspace.Count == 0, Is.EqualTo(true));
            Assert.That(LeavingTrack.tag_ == _testTracks[1].tag_, Is.EqualTo(true));
            Assert.That(LeavingTrack.pos_ == _testTracks[1].pos_, Is.EqualTo(true));


        } */


    } 
}



