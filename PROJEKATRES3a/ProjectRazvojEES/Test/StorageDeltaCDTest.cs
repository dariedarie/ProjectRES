using DumpingBuffer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class StorageDeltaCDTest
    {
        [Test]
        public void StorageDeltaCDPrazanKonstruktor()
        {
            StorageDeltaCD storage = new StorageDeltaCD();
            Assert.AreNotEqual(storage.DumpingDeltaCD, null);
        }

    }
}

