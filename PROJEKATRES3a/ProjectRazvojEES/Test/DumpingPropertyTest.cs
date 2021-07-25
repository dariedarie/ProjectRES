using InterfaceLibrary1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class DumpingPropertyTest
    {
        [Test]
        [TestCase()]
        public void DumpingPrazan_konstruktor()
        {
            DumpingProperty dp = new DumpingProperty();
            Assert.AreEqual(null, null);
        }

        [Test]
        [TestCase(2, 33)]
        [TestCase(3, 55)]
        [TestCase(4, 66)]

        public void DumpingKonstruktorSlucaj_Dobar(ECode cc, int value)
        {
            DumpingProperty dp = new DumpingProperty(cc, value);
            Assert.AreEqual(dp.Code, cc);
        }

        [Test]
        [TestCase(1, -3)]
        [TestCase(10, 0)]

        public void DumpingKonstruktorSlucaj_Los(ECode cc, int value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                DumpingProperty dp = new DumpingProperty(cc, value);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        [TestCase(999)]

        public void DumpingKonstruktorSlucaj_Los2(ECode cc)
        {
            int value = 1;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                DumpingProperty dp = new DumpingProperty(cc, value);
            });
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public void DumpingKonstruktorSlucaj_Los3(ECode cc)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                DumpingProperty dp = new DumpingProperty(cc, 0);
            });
        }

        [Test]
        public void DumpingProp_Test()
        {
            DumpingProperty prop = new DumpingProperty();
            prop.ToString();

        }

    }
}

