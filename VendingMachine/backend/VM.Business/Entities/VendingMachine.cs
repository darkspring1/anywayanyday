using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Business.Entities
{
    public class VendingMachine
    {
        public int Id { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<Good> Goods { get; set; }

    }
}
