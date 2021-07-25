using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class DumpingPropertyCollection
    {
        private List<DumpingProperty> listaDumpingProperty;

        public List<DumpingProperty> ListaDumpingProperty { get => listaDumpingProperty; set => listaDumpingProperty = value; }

        public DumpingPropertyCollection()
        {
            listaDumpingProperty = new List<DumpingProperty>();
        }

    }
}
