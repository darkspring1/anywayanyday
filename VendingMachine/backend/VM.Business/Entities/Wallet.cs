using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Business.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public int r1 { get; set; }
        public int r2 { get; set; }
        public int r5 { get; set; }
        public int r10 { get; set; }

        public int Total() 
        {
            return r1 + r2 * 2 + r5 * 5 + r10 * 10;
        }
        
    }
}
