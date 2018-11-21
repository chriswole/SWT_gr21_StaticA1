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

        [SetUp]
        public void Setup()
        {
            _uut = new LogToFile();
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
    }
}
