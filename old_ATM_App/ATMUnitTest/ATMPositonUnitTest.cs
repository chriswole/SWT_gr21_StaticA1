using System;
using System.Runtime.Remoting.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Internal;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Interfaces;


namespace ATMUnitTest
{
    [TestFixture]
    [Author("Mette Jacobsen, Edited by members of group 21")]

    public class ATMPositonUnitTest
    {
        public Position uut;

        private int x, y;

        [SetUp]
        public void Setup()
        {
            x = 2000;
            y = 2000;
            uut = new Position(x, y);
        }

        [Test]
        public void PositionConstructorAndGetTest()
        {
            Assert.That(uut.x_,Is.EqualTo(x));
            Assert.That(uut.y_, Is.EqualTo(y));
        }

        [Test]
        public void PositionSetAndGetTest()
        {
            int x2 = 4000;
            int y2 = 1000;

            uut.x_ = x2;
            uut.y_ = y2;

            Assert.That(uut.x_,Is.EqualTo(x2));
            Assert.That(uut.y_,Is.EqualTo(y2));
        }

        //Comparison returns true
        [TestCase(1000, 1000, 1000, 1000, true)] //equals
        [TestCase(2000, 2000, 1000, 1000, true)] // greater
        //Comparison returns false
        [TestCase(500, 500, 1000, 1000, false)] //lesser on both x and y
        [TestCase(500, 2000, 1000, 1000, false)] //lesser on x
        [TestCase(2000, 500, 1000, 1000, false)] //lesser on y

        public void GreaterEquals_Operator_Test(int x1, int y1, int x2, int y2, bool result)
        {
            Position pos1 = new Position(x1, y1);
            Position pos2 = new Position(x2, y2);
            Assert.That(pos1 >= pos2, Is.EqualTo(result));
        }

        //Comparison returns true
        [TestCase(9000, 9000, 9000, 9000, true)] //equals
        [TestCase(2000, 2000, 9000, 9000, true)] // lesser
        //Comparison returns false
        [TestCase(10000, 10000, 9000, 9000, false)] //greater on both x and y
        [TestCase(10000, 2000, 9000, 9000, false)] //greater on x
        [TestCase(2000, 10000, 1000, 1000, false)] //greater on y

        public void LesserEquals_Operator_Test(int x1, int y1, int x2, int y2, bool result)
        {
            Position pos1 = new Position(x1, y1);
            Position pos2 = new Position(x2, y2);
            Assert.That(pos1 <= pos2, Is.EqualTo(result));
        }

    }
}