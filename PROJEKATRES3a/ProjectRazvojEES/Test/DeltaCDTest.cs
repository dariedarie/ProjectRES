
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
    public class DeltaCDTest
    {
        [Test]

        public void DeltaCDPrazanKonstruktor()
        {
            DeltaCD delta = new DeltaCD();
            Assert.AreNotEqual(delta.Add, null);
            Assert.AreNotEqual(delta.Update, null);
        }

        [Test]
        public void KonstruktorSaParametrima()
        {

            Assert.IsNotNull(new DeltaCD(5, new Dictionary<int, CollectionDescription>(), new Dictionary<int, CollectionDescription>()));

        }


    }
}

