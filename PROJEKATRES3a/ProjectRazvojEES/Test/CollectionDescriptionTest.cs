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
    public class CollectionDescriptionTest
    {
        [Test]

        public void CollectionDescriptionPrazanKontruktor()
        {
            CollectionDescription cd = new CollectionDescription();
            Assert.AreNotEqual(cd.DumpingPropertyCollection, null);
        }


        [Test]
        public void KonstruktorSaParametrima()
        {

            Assert.IsNotNull(new CollectionDescription(5, 3, new DumpingPropertyCollection()));

        }



    }
}
