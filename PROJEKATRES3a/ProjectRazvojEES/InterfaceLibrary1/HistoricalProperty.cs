using InterfaceLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class HistoricalProperty
    {
        private ECode code;
        private int historicalValue;

        public ECode Code { get => code; set => code = value; }
        public int HistoricalValue { get => historicalValue; set => historicalValue = value; }

        public HistoricalProperty()
        {

        }

        public HistoricalProperty(ECode code, int historicalValue)
        {
            if ((int)code < 1 || (int)code > 10)
            {
                throw new ArgumentOutOfRangeException("Argument code must be in range [1, 10].");
            }

            if (historicalValue <= 0)
            {
                throw new ArgumentNullException("Argument value can't be null.");
            }

            this.code = code;
            this.historicalValue = historicalValue;
        }
    }
}
