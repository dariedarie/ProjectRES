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
    public class DumpingPropertyCollectionTest
    {
        [Test]

        public void DumpingPropertyCollectionPrazanKonstruktor()
        {
            DumpingPropertyCollection kolekt = new DumpingPropertyCollection();
            Assert.AreNotEqual(kolekt.ListaDumpingProperty, null);
        }
    }
}

