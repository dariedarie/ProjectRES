using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public interface IHistorical
    {
        bool Validation(int DS, ECode c);


        bool ComeFromDeadband(ECode code, int value, int dataset);


        int DataSetDefine(ECode code);


        void ManualWriting(ECode code, int value, int dataset);


        void WriteToDataBase(DeltaCD delta);


    }
}
