using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Entities;

namespace VM.Business.Contracts
{
    public enum ResponseCode
    {
        Ok = 0,
        /// <summary>
        /// мало денег
        /// </summary>
        SmallCash = 1,
        /// <summary>
        /// нет сдачи
        /// </summary>
        NoTrifle = 2,
        NoGood = 3

    };
    public class BuyResponse
    {
        public ResponseCode Code { get; set; }

        public User User { get; set; }

        public VendingMachine VendingMachine { get; set; }
    }
}
