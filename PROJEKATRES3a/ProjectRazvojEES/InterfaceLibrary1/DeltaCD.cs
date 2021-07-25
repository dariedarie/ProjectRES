using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class DeltaCD
    {
        private int transactionId;
        private Dictionary<int, CollectionDescription> add;
        private Dictionary<int, CollectionDescription> update;

        public DeltaCD()
        {
            Add = new Dictionary<int, CollectionDescription>();
            Update = new Dictionary<int, CollectionDescription>();
        }

        public int TransactionId { get => transactionId; set => transactionId = value; }
        public Dictionary<int, CollectionDescription> Add { get => add; set => add = value; }
        public Dictionary<int, CollectionDescription> Update { get => update; set => update = value; }

        public DeltaCD(int transactionId, Dictionary<int, CollectionDescription> add, Dictionary<int, CollectionDescription> update)
        {
            this.transactionId = transactionId;
            this.add = add;
            this.update = update;
        }

    }
}
