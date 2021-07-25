using InterfaceLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class DumpingProperty
    {
        private ECode code;
        private int dumpingValue;

        public ECode Code { get => code; set => code = value; }
        public int DumpingValue { get => dumpingValue; set => dumpingValue = value; }



        public DumpingProperty() { }

        public DumpingProperty(ECode code, int dumpingValue)
        {
            if ((int)code < 1 || (int)code > 10)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 10].");
            }

            if (dumpingValue <= 0)
            {
                throw new ArgumentException("Nula ili minus");
            }



            Code = (ECode)code;
            DumpingValue = dumpingValue;
        }

        public override string ToString()
        {
            return "Code : " + Code + " Value " + DumpingValue + " " + "\n";
        }
    }
}
