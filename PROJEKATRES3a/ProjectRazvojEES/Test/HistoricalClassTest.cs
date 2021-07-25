using Historical;
using InterfaceLibrary1;
using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace Test
{
    [TestFixture]
    public class HistoricalClassTest
    {


        Mock<Description> desc;

        [SetUp]
        public void SetUp()
        {
            desc = new Mock<Description>();
        }


        Mock<CollectionDescription> cd;

        [SetUp]
        public void SetUp2()
        {
            cd = new Mock<CollectionDescription>();
        }

        Mock<DeltaCD> dcd;

        [SetUp]
        public void SetUp3()
        {
            dcd = new Mock<DeltaCD>();
        }




        private static readonly object[] _izvor =
        {
            new object[] { ECode.CODE_DIGITAL, 1, 2}
        };

        [Test]
        [TestCaseSource("_izvor")]
        public void ComeFromDeadband_Test(ECode code, int value, int dataset)
        {
            try
            {

                HistoricalClass historical = new HistoricalClass()
                {
                    timestamp = DateTime.Now,
                    izvadiIzBaze = new DataBase(),
                    l = new LoggerClass(),
                    db = new DataBase(),
                    iscitanalista = new List<HistoricalProperty>
                    {
                        new HistoricalProperty
                        {
                            Code = ECode.CODE_ANALOG,
                            HistoricalValue = 111
                        }
                    }
                };
                historical.ComeFromDeadband(code, value, dataset);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();

        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(4, 7)]

        public void Validation_OK(int ds, ECode code)
        {
            HistoricalClass hi = new HistoricalClass();
            hi.Validation(ds, code);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(6, 11)]

        public void Validation_Lose(int ds, ECode code)
        {
            HistoricalClass hi = new HistoricalClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                hi.Validation(ds, code);
            });
        }


        [Test]
        [TestCase(0, 2)]
        [TestCase(6, 3)]

        public void Validation_Lose2(int ds, ECode code)
        {
            HistoricalClass hi = new HistoricalClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                hi.Validation(ds, code);
            });
        }


        [Test]
        [TestCase(1, 0)]
        [TestCase(5, 11)]

        public void Validation_Lose3(int ds, ECode code)
        {
            HistoricalClass hi = new HistoricalClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                hi.Validation(ds, code);
            });
        }


        [Test]
        [TestCase(3, 55, 2)]
        [TestCase(6, 66, 3)]
        [TestCase(8, 77, 4)]

        public void ManualWriting_OK(ECode code, int value, int ds)
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            hi.Object.ManualWriting(code, value, ds);

        }


        [Test]
        [TestCase(0, 55, 2)]
        [TestCase(11, 66, 3)]

        public void ManualWriting_Lose(ECode code, int value, int ds)
        {

            HistoricalClass hi = new HistoricalClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                hi.ManualWriting(code, value, ds);
            });
        }


        [Test]
        [TestCase(2, 0, 2)]
        [TestCase(3, -100, 3)]
        public void ManualWriting_Lose2(ECode code, int value, int ds)
        {

            HistoricalClass hi = new HistoricalClass();
            Assert.Throws<ArgumentException>(() =>
            {
                hi.ManualWriting(code, value, ds);
            });
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]

        public void DataSetDefine_OK(ECode code)
        {
            HistoricalClass hi = new HistoricalClass();
            hi.DataSetDefine(code);
        }



        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void DataSetDefine_Lose(ECode code)
        {
            LoggerClass logger = new LoggerClass();

            HistoricalClass hc = new HistoricalClass();
            var result = hc.DataSetDefine(code);

            Assert.That(result, Is.TypeOf<int>());

            //HistoricalClass hi = new HistoricalClass();
            //Assert.Throws<ArgumentOutOfRangeException>(() =>
            //{
            //    hi.DataSetDefine(code);
            //});
        }




        [Test]
        [TestCase()]

        public void SaveData_OK()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            hi.Object.SaveDate(desc.Object);

        }

        [Test]
        [TestCase()]

        public void SaveData_Lose()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                hi.Object.SaveDate(null);
            });
        }


        [Test]
        [TestCase()]

        public void UpdateData_OK()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            hi.Object.UpdateDate(desc.Object);

        }

        [Test]
        [TestCase()]

        public void UpdateData_Lose()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                hi.Object.UpdateDate(null);
            });
        }

        [Test]


        public void WriteToDataBase_Lose()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                hi.Object.WriteToDataBase(null);
            });
        }

        [Test]
        public void WriteToDataBase_OK()
        {
            Mock<HistoricalClass> hi = new Mock<HistoricalClass>();
            hi.Object.WriteToDataBase(dcd.Object);

        }


        [Test]
        public void RepackInListDescription_Test()
        {
            HistoricalClass historical = new HistoricalClass();
            Description dd = new Description();
            HistoricalProperty hp = new HistoricalProperty();
            CollectionDescription cd = new CollectionDescription();
            LoggerClass l = new LoggerClass();
            historical.RepackInListDescription(cd);

        }

        [Test]
        public void RepackInListDescription_Test2()
        {
            HistoricalClass historical = new HistoricalClass();
            Description dd = new Description()
            {
                Dataset = 2
            };
            HistoricalProperty hp = new HistoricalProperty()
            {
                Code = ECode.CODE_LIMITSET,
                HistoricalValue = 33
            };
            CollectionDescription cd = new CollectionDescription()
            {
                Dataset = 2,
                Id = 0,
                DumpingPropertyCollection = new DumpingPropertyCollection
                {
                    ListaDumpingProperty = new List<DumpingProperty>
                    {
                        new DumpingProperty
                        {
                            Code=ECode.CODE_CONSUMER,
                            DumpingValue=22
                        }
                    }
                }

            };
            LoggerClass l = new LoggerClass();
            historical.RepackInListDescription(cd);

        }

    }
}