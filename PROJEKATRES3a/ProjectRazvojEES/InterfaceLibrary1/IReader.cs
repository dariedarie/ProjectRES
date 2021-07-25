using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public interface IReader
    {
        void GetChangeForInterval(DateTime pocetak,DateTime kraj,int dataset);
    }
}
