using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibrary1;
using Writter;

namespace Test
{
    [TestFixture]
    public class WritterClassTest
    {

        [Test]
        [TestCase(4, 222, 2)]
        [TestCase(5, 444, 3)]
        [TestCase(6, 333, 3)]

        public void WritterManualWriteToHistory_Dobri(ECode code, int value, int dataset)
        {
            Mock<IWritter> w = new Mock<IWritter>();
            w.Object.ManualWriteToHistory(code, value, dataset);
        }

        [Test]
        [TestCase(-1, 44, 3)]
        [TestCase(11, 33, 4)]

        public void WritterManualWriteToHistory_Lose(ECode code, int value, int dataset)
        {
            WritterClass w = new WritterClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                w.ManualWriteToHistory(code, value, dataset);
            });

            //Mock<Writter> Writter = new Mock<Writter>();
            //Writter.Object.ManualWriteToHistory(c, ValueMock.Object);
        }

        [Test]
        [TestCase(2, -3, 1)]
        [TestCase(3, 0, 2)]
        [TestCase(8, -10, 4)]

        public void WritterManualWriteToHistory_Lose2(ECode code, int value, int dataset)
        {
            WritterClass w = new WritterClass();
            Assert.Throws<ArgumentException>(() =>
            {
                w.ManualWriteToHistory(code, value, dataset);
            });
        }

        [Test]
        [TestCase(3, 400, 6)]
        [TestCase(4, 222, 0)]
        [TestCase(7, 333, 8)]

        public void WritterManualWriteToHistory_Lose3(ECode code, int value, int dataset)
        {
            WritterClass w = new WritterClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                w.ManualWriteToHistory(code, value, dataset);
            });
        }


    }
}

