using InterfaceLibrary1;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Historical
{
    public class HistoricalClass : IHistorical
    {
        public DateTime timestamp { get; set; }
        public DataBase izvadiIzBaze = new DataBase();
        public LoggerClass l = new LoggerClass();
        public DataBase db = new DataBase();

        public List<HistoricalProperty> iscitanalista = new List<HistoricalProperty>();

        public HistoricalClass()
        {
            timestamp = DateTime.Now;
            l.LogEvent("Historical", "Kreiraj timestamp..");
        }


        public void ManualWriting(ECode code, int value, int dataset)
        {


            List<Description> desc = new List<Description>();

            if ((int)code < 1 || (int)code > 10)
            {
                throw new ArgumentOutOfRangeException("Argument code must be in range [1, 10].");
            }

            if (value <= 0)
            {
                throw new ArgumentException("Argument value can't be null.");
            }
            if (!Validation(dataset, code))
            {
                throw new ArgumentNullException("Argument is not validate");
            }
            /*   if(!ComeFromDeadband(new HistoricalProperty(code, value), dataset))
               {
                   throw new ArgumentNullException("Argument is out of deadband");
               }*/

            

           
                Description dd = new Description();
                dd.Dataset = dataset;
                HistoricalProperty hp = new HistoricalProperty(code, value);
                dd.HistoricalProperties.Add(hp);
                l.LogEvent("Historical", "Primi podatke od writera.");
                SaveDate(dd);
           
            //db.WriteToBase(code, value, dataset.ToString());

        }

        public bool Validation(int DS, ECode c)
        {

            bool retVal = false;
            if ((int)c < 1 || (int)c > 10)
            {
                throw new ArgumentOutOfRangeException("GRESKA, Code mora biti u granicama [1, 10].");
            }

            if ((int)DS < 1 || (int)DS > 5)
            {
                throw new ArgumentOutOfRangeException("GRESKA, DataSet mora biti u granicama [1, 5].");
            }

            if (DS == DataSetDefine(c))
            {
                retVal = true;
            }
            l.LogEvent("Historical", "Validiraj.");
            return retVal;

        }

        public int DataSetDefine(ECode kod)
        {
            if ((int)kod < 1 || (int)kod > 10)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 10].");
            }

            int dataSET = 0;

            if ((int)kod == 1 || (int)kod == 2)
            {
                dataSET = 1;
            }
            if ((int)kod == 3 || (int)kod == 4)
            {
                dataSET = 2;
            }
            if ((int)kod == 5 || (int)kod == 6)
            {
                dataSET = 3;
            }
            if ((int)kod == 7 || (int)kod == 8)
            {
                dataSET = 4;
            }
            if ((int)kod == 9 || (int)kod == 10)
            {
                dataSET = 5;
            }

            l.LogEvent("Historical", "Definisi DataSet");
            return dataSET;

        }



        public void WriteToDataBase(DeltaCD delta)
        {
            if (delta == null)
            {
                throw new ArgumentNullException("Ne sme biti null");
            }

            List<Description> desc = new List<Description>();
            foreach (var kolekcija in delta.Add.Values)
            {
                Description dd = RepackInListDescription(kolekcija);
                desc.Add(dd);
            }

            foreach (var item in desc)
            {
                SaveDate(item);
                l.LogEvent("Historical", "Sacuvaj u bazu...");
            }

            List<Description> desc2 = new List<Description>();
            foreach (var kolekcija in delta.Update.Values)
            {
                Description dd = RepackInListDescription(kolekcija);
                desc2.Add(dd);
            }

            foreach (var item in desc2)
            {
                //UpdateDate(item);
                l.LogEvent("Historical", "Azuriraj bazu...");
            }
        }


        public Description RepackInListDescription(CollectionDescription cd)
        {
            Description dd = new Description();
            dd.Dataset = cd.Dataset;
            foreach (var item in cd.DumpingPropertyCollection.ListaDumpingProperty)
            {
                int value = item.DumpingValue;
                HistoricalProperty hp = new HistoricalProperty(item.Code, value);
                dd.HistoricalProperties.Add(hp);
                l.LogEvent("Historical", "Prepakuj strukturu od DumpingBuffera..");
            }

            return dd;
        }


        public virtual void SaveDate(Description dd)
        {
            if (dd == null)
            {
                throw new ArgumentNullException("Ne sme biti null.");
            }



            foreach (var item in dd.HistoricalProperties)
            {
                ECode code = item.Code;
                int value = item.HistoricalValue;
                int dataset = dd.Dataset;
                db.CreateBase(dataset.ToString());
                db.WriteToBase(code, value, dataset.ToString());

            }
        }

        public virtual void UpdateDate(Description dd)
        {
            if (dd == null)
            {
                throw new ArgumentNullException("Ne sme biti null.");
            }


            iscitanalista = ProcitajIzBaze2(dd.Dataset);
            foreach (var item in dd.HistoricalProperties)
            {
                foreach (var item2 in iscitanalista)
                {
                    if (item2.Code == item.Code)
                    {
                        item2.HistoricalValue = item.HistoricalValue;
                        //db.CreateBase(dd.Dataset.ToString());
                        db.WriteToBase(item2.Code, item2.HistoricalValue, dd.Dataset.ToString());
                    }
                }
            }
        }

        public bool ComeFromDeadband(ECode code, int value, int dataset)
        {

            iscitanalista = ProcitajIzBaze2(dataset);

            Description dd = new Description();


            if (iscitanalista == null)
            {
                throw new ArgumentNullException("Prazna lista...");
            }

            if (value <= 0)
            {
                throw new ArgumentException("Prazan historicalproperty...");
            }

            if ((int)dataset < 1 || (int)dataset > 5)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 5].");
            }

            foreach (var item in iscitanalista)
            {
                if (item.Code == code)
                {
                    if ((item.HistoricalValue * 0.98 > value) || (value > item.HistoricalValue * 1.02))
                    {
                        dd.Dataset = dataset;
                        item.HistoricalValue = value;
                        HistoricalProperty hp = new HistoricalProperty(item.Code, item.HistoricalValue);
                        dd.HistoricalProperties.Add(hp);
                        //UpdateDate(dd);
                        return true;
                    }
                }
            }
            return false;
        }

        public List<HistoricalProperty> ProcitajIzBaze(DateTime pocetak, DateTime kraj, int dataset)
        {
            List<HistoricalProperty> retVal = new List<HistoricalProperty>();
            retVal = db.ReturnFromBase(pocetak, kraj, dataset.ToString());
            return retVal;
        }

        public List<HistoricalProperty> ProcitajIzBaze2(int dataset)
        {
            List<HistoricalProperty> retVal = new List<HistoricalProperty>();
            retVal = db.ReturnFromBase2(dataset.ToString());
            return retVal;
        }
    }
}

