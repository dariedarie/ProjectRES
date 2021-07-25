using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public interface IWritter
    {
        void ManualWriteToHistory(ECode code,int value, int dataset);

        void WriteToDumpingBuffer();
    }
}
