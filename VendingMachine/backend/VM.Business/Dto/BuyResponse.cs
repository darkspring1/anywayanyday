
using VM.Business.Entities;

namespace VM.Business.Dto
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
