using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibrary1;

namespace Test
{
    [TestFixture]
    public class HistoricalPropertyTest
    {


        [Test]
        [TestCase()]
        public void HistoricalPrazan_konstruktor()
        {
            HistoricalProperty hp = new HistoricalProperty();
            Assert.AreEqual(null, null);
        }



        [Test]
        [TestCase(2, 3)]
        [TestCase(5, 4)]
        [TestCase(9, 5)]

        public void HistoricalPropertyConstructor_GoodParameters(ECode c, int value)
        {
            HistoricalProperty hp = new HistoricalProperty(c, value);
            Assert.AreEqual(hp.Code, c);
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(10, 4)]

        public void HistoricalSlucaj_LosKod(ECode c, int value)
        {
            HistoricalProperty hp = new HistoricalProperty(c, value);
            Assert.AreEqual(hp.Code, c);
        }

        [Test]
        [TestCase(-1, -10)]
        [TestCase(-2, 0)]
        [TestCase(-3, -5)]

        public void HistoricalSlucaj_LosaVrednost(ECode c, int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                HistoricalProperty hp = new HistoricalProperty(c, value);
            });
        }

        [Test]
        [TestCase(3, null)]
        [TestCase(4, null)]
        [TestCase(7, null)]

        public void HistoricalPropertyConstructor_BadParameters2(ECode c, int value)
        {

            Assert.Throws<ArgumentNullException>(() =>
            {
                HistoricalProperty hp = new HistoricalProperty(c, value);
            });
        }



    }
}
