using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class CollectionDescription
    {
        private int id;
        private int dataset;
        private DumpingPropertyCollection dumpingPropertyCollection;

        

        public int Id { get => id; set => id = value; }
        public int Dataset { get => dataset; set => dataset = value; }
        
        
        public DumpingPropertyCollection DumpingPropertyCollection { get => dumpingPropertyCollection; set => dumpingPropertyCollection = value; }

        public CollectionDescription()
        {
            Id = 0;
            dataset = 0;
            dumpingPropertyCollection = new DumpingPropertyCollection();

        }

        public CollectionDescription(int iD, int dataSet, DumpingPropertyCollection dumpingPropertyCollection)
        {
            Id = iD;
            Dataset = dataSet;
            DumpingPropertyCollection = new DumpingPropertyCollection();
            
        }

        

    }
}
