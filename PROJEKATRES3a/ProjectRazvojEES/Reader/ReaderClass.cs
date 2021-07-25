using Historical;
using InterfaceLibrary1;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    public class ReaderClass : IReader
    {
        LoggerClass l = new LoggerClass();

        public void GetChangeForInterval(DateTime pocetak, DateTime kraj, int dataset)
        {
            if ((int)dataset < 1 || (int)dataset > 5)
            {
                Console.WriteLine("Pogresan dataset");
                throw new ArgumentOutOfRangeException("Opseg [1, 5].");
            }

            if (pocetak == null || kraj == null)
            {
                throw new ArgumentNullException("Datum ne sme biti null.");
            }


            //DataBase database = new DataBase();
            HistoricalClass historikal = new HistoricalClass();
            List<HistoricalProperty> h = historikal.ProcitajIzBaze(pocetak, kraj, dataset);
            foreach (var item in h)
            {
                Console.WriteLine("ECode : {0}, Value : {1}", item.Code, item.HistoricalValue);
            }
            l.LogEvent("Reader", "Citaj iz baze...");

        }
    }
}
