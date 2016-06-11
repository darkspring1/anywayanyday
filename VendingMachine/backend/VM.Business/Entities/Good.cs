using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Business.Entities
{
    public class Good
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public int VendingMachineId { get; set; }
        public VendingMachine VendingMachine { get; set; }
    }
}
