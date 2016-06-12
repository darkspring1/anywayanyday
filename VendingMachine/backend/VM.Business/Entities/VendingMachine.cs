using System.Collections.Generic;

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
