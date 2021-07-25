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
    public class DescriptionTest
    {
        [Test]

        public void DescriptionPrazan_konstruktor()
        {
            try
            {
                Description dd = new Description();
                Description dd2 = new Description
                {
                    Id = 1,
                    Dataset = 1,
                    HistoricalProperties = new List<HistoricalProperty>
                    {
                        new HistoricalProperty
                        {
                            Code = ECode.CODE_ANALOG,
                            HistoricalValue = 111
                        }
                    }
                };
                //Assert.AreNotEqual(dd.HistoricalProperties, null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();


        }
        private static readonly object[] _izvor =
        {
            new object[] { 0, new List<HistoricalProperty> { new HistoricalProperty { Code = ECode.CODE_ANALOG, HistoricalValue= 111 } }, 2 },
            new object[] { 1, new List<HistoricalProperty> { new HistoricalProperty { Code = ECode.CODE_ANALOG, HistoricalValue= 111 } }, 2 },
            new object[] { 2, new List<HistoricalProperty> { new HistoricalProperty { Code = ECode.CODE_ANALOG, HistoricalValue= 111 } }, 2 }
        };
        [Test]
        [TestCaseSource("_izvor")]
        public void KonstruktorSaParametrima(int id, List<HistoricalProperty> historicalProperties, int dataset)
        {
            try
            {
                Description description = new Description(id, historicalProperties, dataset);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();
            //Assert.IsNotNull(new Description(2,new List<HistoricalProperty>(),2));

        }


    }
}

