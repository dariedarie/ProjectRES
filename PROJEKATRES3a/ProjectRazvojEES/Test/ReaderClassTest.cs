using Historical;
using InterfaceLibrary1;
using Logger;
using NUnit.Framework;
using Reader;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class ReaderClassTest
    {

        private List<HistoricalProperty> tabela1 = new List<HistoricalProperty>();
        private List<HistoricalProperty> tabela2 = new List<HistoricalProperty>();
        private List<HistoricalProperty> tabela3 = new List<HistoricalProperty>();
        private List<HistoricalProperty> tabela4 = new List<HistoricalProperty>();
        private List<HistoricalProperty> tabela5 = new List<HistoricalProperty>();


        [SetUp]
        public void SetUp()
        {
            tabela1.Add(new HistoricalProperty(ECode.CODE_ANALOG, 202));
            tabela2.Add(new HistoricalProperty(ECode.CODE_CONSUMER, 205));
            tabela3.Add(new HistoricalProperty(ECode.CODE_LIMITSET, 204));
            tabela4.Add(new HistoricalProperty(ECode.CODE_SENSOR, 203));
            tabela5.Add(new HistoricalProperty(ECode.CODE_SOURCE, 206));
        }

        [Test]
        public void GetChangeForInterval_Test()
        {
            LoggerClass l = new LoggerClass();
            Mock<IReader> rr = new Mock<IReader>();
            rr.Setup(r => r.GetChangeForInterval(new DateTime(2020, 1, 1), new DateTime(2020, 12, 11), 1));



        }




        [Test]
        [TestCase("2020/6/6", "2020/6/8", 1)]

        public void ReaderMetodaSlucaj_Dobar(DateTime pocetak, DateTime kraj, int d)
        {

            Mock<IReader> r = new Mock<IReader>();
            r.Object.GetChangeForInterval(pocetak, kraj, d);
        }


        [Test]
        [TestCase(2)]
        public void ReaderMetodaSlucaj_DobarDataSet(int d)
        {
            Mock<IReader> r = new Mock<IReader>();
            r.Object.GetChangeForInterval(new DateTime(2020, 6, 10), new DateTime(2020, 6, 12), d);
        }



        [Test]
        [TestCase(0)]

        public void ReaderSlucaj_LosDataSet(int d)
        {
            ReaderClass r = new ReaderClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                r.GetChangeForInterval(DateTime.Now, DateTime.Now, d);
            });
        }


        [Test]
        [TestCase(null, null, 7)]

        public void ReaderSlucaj_Los2(DateTime pocetak, DateTime kraj, int d)
        {
            ReaderClass r = new ReaderClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                r.GetChangeForInterval(pocetak, kraj, d);
            });
        }





    }
}
