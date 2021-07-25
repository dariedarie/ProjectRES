using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public interface IDumpingBuffer
    {
        void AcceptFromWritter(ECode code, int value);
        void WriteToHistory();
    }
}
