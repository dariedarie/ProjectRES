using InterfaceLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBuffer
{
    public class StorageDeltaCD
    {
        private DeltaCD dumpingDeltaCD;
        public DeltaCD DumpingDeltaCD { get => dumpingDeltaCD; set => dumpingDeltaCD = value; }



        public StorageDeltaCD()
        {
            DumpingDeltaCD = new DeltaCD();
        }
    }
}
