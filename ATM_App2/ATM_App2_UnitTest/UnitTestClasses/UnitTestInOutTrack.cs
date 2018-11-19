using System;
using System.Collections.Generic;
using ATM_App2.Events;
using ATM_App2.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework;
using ATM_App2.Classes;

namespace ATM_App2_UnitTest.UnitTestClasses
{
    [TestClass]
    public class UnitTestInOutTrack
    {
        private InOutTrack _uut;
        private EnteredTrackArgs _enteredTrack;
        private LeftTrackArgs _leftTrack;
        private List<Track> _track1;
        private List<Track> _track2;


        [SetUp]
        public void Setup()
        {
            _uut = new InOutTrack();

        }
        [Test]
        public void TestMethod1()
        {
            
        }
    }
}
