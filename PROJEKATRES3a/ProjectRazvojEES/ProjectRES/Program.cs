using Historical;
using InterfaceLibrary1;
using Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Writter;

namespace ProjectRES
{
    class Program
    {

        public static void ManualWriteToHistory()
        {
            IWritter writer = new WritterClass();
            Console.WriteLine("Ponudjeni kodovi:");
            for (int i = 1; i < 11; i++)
            {
                ECode ispisKoda = (ECode)i;
                Console.WriteLine(i + "---" + ispisKoda);
            }
            Console.WriteLine("Kod koji unosite: ");
            string unosKoda = Console.ReadLine();
            ECode konvertKod;
            konvertKod = (ECode)Convert.ToInt32(unosKoda);
            Console.WriteLine("Uniste vrednost: ");
            int vrednost = int.Parse(Console.ReadLine());
            IHistorical historikal = new HistoricalClass();

            HistoricalProperty prop = new HistoricalProperty(konvertKod, vrednost);
            int dataset = historikal.DataSetDefine(konvertKod);
            if (unosKoda != "1" || unosKoda != "CODE_DIGITAL")
            {
                if (historikal.Validation(dataset, konvertKod) && historikal.ComeFromDeadband(konvertKod, vrednost, dataset))
                {
                    writer.ManualWriteToHistory(konvertKod, vrednost, dataset);
                }
            }
            else
            {
                if (historikal.Validation(dataset, konvertKod))
                {
                    writer.ManualWriteToHistory(konvertKod, vrednost, dataset);
                }
            }
        }

        public static void GetChangeForInterval()
        {
            IReader reader = new ReaderClass();
            Console.WriteLine("Odaberite za koju tabelu zelite podatke : ");
            int dataset = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Unesite dan od kad zelite da prikazete podatke");
            int dan = Int32.Parse(Console.ReadLine());
            if ((int)dan < 1 || (int)dan > 31)
            {
                throw new ArgumentOutOfRangeException("Dan moze biti u intervalu [1, 31].");
            }

            Console.WriteLine("Unesite mesec od kad zelite da prikazete podatke");
            int mesec = Int32.Parse(Console.ReadLine());
            if ((int)mesec < 1 || (int)mesec > 12)
            {
                throw new ArgumentOutOfRangeException("Mesec moze biti u intervalu [1, 12].");
            }
            Console.WriteLine("Unesite godina od kad zelite da prikazete podatke");
            int godina = Int32.Parse(Console.ReadLine());
            if ((int)godina < 1)
            {
                throw new ArgumentOutOfRangeException("Godina mora biti pozitivan broj [1, 12].");
            }
            DateTime pocekat = new DateTime(godina, mesec, dan);

            Console.WriteLine("Unesite dan do kad zelite da prikazete podatke");
            dan = Int32.Parse(Console.ReadLine());
            if ((int)dan < 1 || (int)dan > 31)
            {
                throw new ArgumentOutOfRangeException("Dan moze biti u intervalu [1, 31].");
            }
            Console.WriteLine("Unesite mesec do kad zelite da prikazete podatke");
            mesec = Int32.Parse(Console.ReadLine());
            if ((int)mesec < 1 || (int)mesec > 12)
            {
                throw new ArgumentOutOfRangeException("Mesec moze biti u intervalu [1, 12].");
            }
            Console.WriteLine("Unesite godina do kad zelite da prikazete podatke");
            godina = Int32.Parse(Console.ReadLine());
            if ((int)godina < 1)
            {
                throw new ArgumentOutOfRangeException("Godina mora biti pozitivan broj [1, 12].");
            }

            DateTime kraj = new DateTime(godina, mesec, dan);
            reader.GetChangeForInterval(pocekat, kraj, dataset);
        }



        public static void StartThreadForWritter()
        {
            IWritter writer = new WritterClass();

            Task t = Task.Run(() => { writer.WriteToDumpingBuffer(); });

        }


        static void Main(string[] args)
        {
            StartThreadForWritter();
            int izbor2 = 0;
            do
            {
                Console.WriteLine("1.Upisi u bazu\n2.Prikazi iz baze\n0.Exit");
                izbor2 = Int32.Parse(Console.ReadLine());
                switch (izbor2)
                {
                    case 1:
                        try
                        {
                            ManualWriteToHistory();
                        }
                        catch
                        {
                            Console.WriteLine("Pogresan unos");
                        }

                        break;
                    case 2:
                        try
                        {
                            GetChangeForInterval();
                        }
                        catch
                        {
                            Console.WriteLine("Pogresan datum");
                        }
                        break;
                    default:
                        Console.WriteLine("Morate uneti 1 ili 2 ili 0...");
                        break;
                }
            } while (izbor2 != 0);
        }
    }
}
