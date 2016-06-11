using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Entities;

namespace VM.Business.Contracts
{
    public class Buy
    {
        public int UserId { get; set; }
        
        public int GoodId { get; set; }
        public Wallet CashBox { get; set; }
    }
}
