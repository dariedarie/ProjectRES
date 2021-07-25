using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLibrary1;
using DumpingBuffer;
using Logger;

namespace Test
{
    [TestFixture]
    public class DumpingBufferClassTest
    {


        Mock<Dictionary<int, CollectionDescription>> temp;
        Mock<DumpingProperty> prop;


        [SetUp]
        public void SetUp()
        {
            temp = new Mock<Dictionary<int, CollectionDescription>>();
        }


        [SetUp]
        public void SetUp2()
        {
            prop = new Mock<DumpingProperty>();
        }



        [Test]
        [TestCase(3, 333)]
        [TestCase(6, 444)]
        [TestCase(8, 222)]

        public void AcceptFromWritter_OK(ECode code, int value)
        {
            DumpingPropertyCollection dpc = new DumpingPropertyCollection
            {
                ListaDumpingProperty = new List<DumpingProperty>
                {
                    new DumpingProperty
                    {
                        Code = ECode.CODE_ANALOG,
                        DumpingValue = 111
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_DIGITAL,
                        DumpingValue = 0
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_ANALOG,
                        DumpingValue = 111
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_DIGITAL,
                        DumpingValue = 0
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_ANALOG,
                        DumpingValue = 111
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_DIGITAL,
                        DumpingValue = 0
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_ANALOG,
                        DumpingValue = 111
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_DIGITAL,
                        DumpingValue = 0
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_ANALOG,
                        DumpingValue = 111
                    },
                    new DumpingProperty
                    {
                        Code = ECode.CODE_DIGITAL,
                        DumpingValue = 0
                    }
                }
            };

            DumpingBufferClass dump = new DumpingBufferClass();
            dump.WriteToHistory();
            dump.AcceptFromWritter(code, value);
        }

        [Test]
        [TestCase(0, 222)]
        [TestCase(11, 333)]

        public void AcceptFromWritter_Lose(ECode code, int value)
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                dump.AcceptFromWritter(code, value);
            });
        }

        [Test]
        [TestCase(2, 0)]
        [TestCase(3, -3)]


        public void AcceptFromWritter_Lose2(ECode code, int value)
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentException>(() =>
            {
                dump.AcceptFromWritter(code, value);
            });
        }

        [Test]
        [TestCase()]

        public void RepackDeltaCD_OK()
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            DumpingBufferClass dump2 = new DumpingBufferClass
            {
                brojacPristiglihVrednosti = 0,
                nijeSpremnoZaSlanje = false,
                storageDeltaCD = new StorageDeltaCD(),
                l = new LoggerClass()
            };
            dump.RepackDeltaCD(temp.Object);
            dump2.RepackDeltaCD(temp.Object);
        }

        [Test]

        public void RepackDeltaCd_Test()
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            StorageDeltaCD storageDeltaCD = new StorageDeltaCD
            {
                DumpingDeltaCD = new DeltaCD()
                {
                    TransactionId = 0,
                    Add = new Dictionary<int, CollectionDescription>(),
                    Update = new Dictionary<int, CollectionDescription>()


                }

            };
            dump.RepackDeltaCD(storageDeltaCD.DumpingDeltaCD.Update);
            dump.RepackDeltaCD(storageDeltaCD.DumpingDeltaCD.Add);


        }


        [Test]
        public void RepackDeltaCd_Test2()
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            DumpingPropertyCollection lista = new DumpingPropertyCollection
            {
                ListaDumpingProperty = new List<DumpingProperty>
                {
                    new DumpingProperty
                    {
                        Code=ECode.CODE_MULTIPLENODE,
                        DumpingValue=333

                    }

                }

            };
            dump.RepackDeltaCD(temp.Object);

        }






        [Test]
        [TestCase()]

        public void RepackDeltaCD_Lose()
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentNullException>(() =>
            {
                dump.RepackDeltaCD(null);
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
            DumpingBufferClass dump = new DumpingBufferClass();
            dump.DataSetDefine(code);
        }



        [Test]
        [TestCase(11)]
        [TestCase(0)]

        public void DataSetDefine_Lose(ECode code)
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                dump.DataSetDefine(code);
            });
        }

        /*    [Test]
            [TestCase(3,333,2)]

            public void SameCode_OK(ECode code,int value,int dataset)
            {
                DumpingBufferClass dump = new DumpingBufferClass();
                Assert.AreEqual(false, dump.SameCode(code, value,dataset));
            }*/

        [Test]
        [TestCase(0, 222, 2)]
        [TestCase(11, 333, 3)]

        public void SameCode_Lose(ECode code, int value, int dataset)
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                dump.SameCode(code, value, dataset);
            });

        }

        [Test]
        [TestCase(1, 0, 1)]
        [TestCase(3, -3, 2)]

        public void SameCode_Lose2(ECode code, int value, int dataset)
        {
            DumpingBufferClass dump = new DumpingBufferClass();
            Assert.Throws<ArgumentException>(() =>
            {
                dump.SameCode(code, value, dataset);
            });

        }







        /*   [Test]
           [TestCase()]

           public void SameCode2_Lose()
           {
               DumpingBufferClass dump = new DumpingBufferClass();
               Assert.Throws<ArgumentNullException>(() =>
               {
                   dump.SameCode2(new KeyValuePair<int, CollectionDescription>(), new DumpingProperty());
               });

           }*/

        /*  [Test]
          [TestCase()]

          public void SameCode2_OK()
          {
              KeyValuePair<int,CollectionDescription> temp3 = new KeyValuePair<int, CollectionDescription>();
              DumpingBufferClass dump = new DumpingBufferClass();
              Assert.Throws<ArgumentNullException>(() =>
              {
                  dump.SameCode2(temp3 , new DumpingProperty(ECode.CODE_ANALOG,22));
              });

          }*/

        [Test]
        public void WriteToHistory()
        {
            DumpingBufferClass dump = new DumpingBufferClass
            {
                brojacPristiglihVrednosti = 0,
                nijeSpremnoZaSlanje = false,
                storageDeltaCD = new StorageDeltaCD
                {
                    DumpingDeltaCD = new DeltaCD
                    {
                        Add = new Dictionary<int, CollectionDescription>(),
                        Update = new Dictionary<int, CollectionDescription>()
                    }
                }
            };

            dump.WriteToHistory();



        }


    }

}
