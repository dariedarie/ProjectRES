using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLibrary1
{
    public class Description
    {
        private int id;
        private List<HistoricalProperty> historicalProperties;
        private int dataset;

        public int Id { get => id; set => id = value; }
        public List<HistoricalProperty> HistoricalProperties { get => historicalProperties; set => historicalProperties = value; }
        public int Dataset { get => dataset; set => dataset = value; }

        public Description()
        {
            historicalProperties = new List<HistoricalProperty>();
        }

        public Description(int id, List<HistoricalProperty> historicalProperties, int dataset)
        {
            this.id = id;
            this.historicalProperties = historicalProperties;
            this.dataset = dataset;
        }

    }
}
