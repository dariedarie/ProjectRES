using DumpingBuffer;
using Historical;
using InterfaceLibrary1;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Writter
{
    public class WritterClass : IWritter
    {
        private ECode code;
        private int value;
        IHistorical historikal = new HistoricalClass();
        public LoggerClass l = new LoggerClass();


        public ECode Code { get => code; set => code = value; }
        public int Value { get => value; set => this.value = value; }

        public WritterClass()
        {

        }


        public void ManualWriteToHistory(ECode code, int value, int dataset)
        {
            if ((int)code < 1 || (int)code > 10)
            {

                throw new ArgumentOutOfRangeException("Opseg [1, 10].");
            }

            if (value <= 0)
            {

                throw new ArgumentException("Ne sme biti negativan ili 0.");
            }

            if (dataset < 1 || dataset > 5)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 5].");
            }

            l.LogEvent("Writter", "Salji podatke direktno...");
            historikal.ManualWriting(code, value, dataset);
            
        }

        public void WriteToDumpingBuffer()
        {
            IDumpingBuffer db = new DumpingBufferClass();


            while (true)
            {
                ECode c;
                int v;
                Random rr = new Random();
                c = (ECode)rr.Next(1, 10);
                v = rr.Next(1, 500);
                db.AcceptFromWritter(c, v);
                Thread.Sleep(2000);
                l.LogEvent("Writter", "Salji podatke preko DumpingBuffera");
            }
        }
    }
}
