using Historical;
using InterfaceLibrary1;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBuffer
{
    public class DumpingBufferClass : IDumpingBuffer
    {
        Dictionary<int, CollectionDescription> temp;
        public int brojacPristiglihVrednosti = 0;
        public bool nijeSpremnoZaSlanje = false;
        public StorageDeltaCD storageDeltaCD = new StorageDeltaCD();
        public LoggerClass l = new LoggerClass();

        public DumpingBufferClass()
        {
            temp = new Dictionary<int, CollectionDescription>() { { 1, null }, { 2, null }, { 3, null }, { 4, null }, { 5, null } };
        }



        public void AcceptFromWritter(ECode code, int value)
        {

            if ((int)code < 1 || (int)code > 10)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 10].");
            }

            if (value <= 0)
            {
                throw new ArgumentException("Ne sme biti null");
            }


            l.LogEvent("DumpingBuffer", "DumpingBuffer prima podatke od Writtera.");


            brojacPristiglihVrednosti++;

            DumpingProperty dp = new DumpingProperty(code, value);
            int dataset = DataSetDefine(code);
            l.LogEvent("DumpingBuffer", "DumpingBuffer definise DataSet na osnovu Coda");
            if (temp[dataset] == null)
            {

                temp[dataset] = new CollectionDescription();
                temp[dataset].DumpingPropertyCollection.ListaDumpingProperty.Add(dp);
            }
            else
            {
                if (!SameCode(code, value, dataset))
                {
                    l.LogEvent("DumpingBuffer", "DumpingBuffer skladisti svoje podatke u memoriju");
                    temp[dataset].DumpingPropertyCollection.ListaDumpingProperty.Add(dp);
                    

                }

            }
            if ((temp[dataset].DumpingPropertyCollection.ListaDumpingProperty.Count % 2) == 0)
            {
                if (!nijeSpremnoZaSlanje)
                {
                    RepackDeltaCD(temp);
                    l.LogEvent("DumpingBuffer", "DumpingBuffer podatke prepakuje u svoju strukturu ");
                    temp.Clear();
                    temp = new Dictionary<int, CollectionDescription>() { { 1, null }, { 2, null }, { 3, null }, { 4, null }, { 5, null } };
                }

            }
            if (brojacPristiglihVrednosti == 10)
            {
                l.LogEvent("DumpingBuffer", "prosleđuje Historical komponenti");
                WriteToHistory();
                
            }
        }

        public bool SameCode(ECode code, int value, int dataset)
        {
            if ((int)code < 1 || (int)code > 10)
            {
                throw new ArgumentOutOfRangeException("Opseg [1, 10].");

            }

            if (value <= 0)
            {
                throw new ArgumentException("Ne sme biti null");

            }

            foreach (var item in temp[dataset].DumpingPropertyCollection.ListaDumpingProperty)
            {
                if (item.Code == code)
                {
                    item.DumpingValue = value;
                    return true;
                }


            }

            return false;

        }



        public void RepackDeltaCD(Dictionary<int, CollectionDescription> temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("Ne sme biti null");
            }


            foreach (KeyValuePair<int, CollectionDescription> item in temp)
            {
                if (item.Value == null)
                {
                    continue;
                }
                foreach (var item2 in item.Value.DumpingPropertyCollection.ListaDumpingProperty)
                {
                    if (storageDeltaCD.DumpingDeltaCD.Add == null)
                        storageDeltaCD.DumpingDeltaCD.Add = new Dictionary<int, CollectionDescription>(); 
                    if (!storageDeltaCD.DumpingDeltaCD.Add.ContainsKey(item.Key))
                    {
                        storageDeltaCD.DumpingDeltaCD.Add[item.Key] = new CollectionDescription();
                        storageDeltaCD.DumpingDeltaCD.Add[item.Key].Dataset = item.Key;

                    }
                    bool isti = true;
                    foreach (DumpingProperty DumpProp in storageDeltaCD.DumpingDeltaCD.Add[item.Key].DumpingPropertyCollection.ListaDumpingProperty)
                    {
                        if (DumpProp.Code == item2.Code)
                        {
                            isti = false;
                            break;
                        }
                    }
                    if (isti)
                    {
                        storageDeltaCD.DumpingDeltaCD.Add[item.Key].DumpingPropertyCollection.ListaDumpingProperty.Add(item2);
                    }
                   
                    else
                    {
                        if (!storageDeltaCD.DumpingDeltaCD.Update.ContainsKey(item.Key))
                        {
                            storageDeltaCD.DumpingDeltaCD.Update[item.Key] = new CollectionDescription();
                            storageDeltaCD.DumpingDeltaCD.Update[item.Key].Dataset = item.Key;
                        }
                        storageDeltaCD.DumpingDeltaCD.Update[item.Key].DumpingPropertyCollection.ListaDumpingProperty.Add(item2);
                    }
                }


            }
        }


       


        public int DataSetDefine(ECode kod)
        {
           if ((int)kod < 1 || (int)kod > 10)
            {
                throw new ArgumentOutOfRangeException("Kod mora biti u opsegu [1, 10]");
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

            return dataSET;

        }

        public void WriteToHistory()
        {
            brojacPristiglihVrednosti = 0;
            if (storageDeltaCD.DumpingDeltaCD.Add.Count == 0 && storageDeltaCD.DumpingDeltaCD.Update.Count == 0)
            {
                nijeSpremnoZaSlanje = true;
                return;
            }
            nijeSpremnoZaSlanje = false;
            IHistorical historikal = new HistoricalClass();
            historikal.WriteToDataBase(storageDeltaCD.DumpingDeltaCD);
            storageDeltaCD = new StorageDeltaCD();
        }
    }
}
