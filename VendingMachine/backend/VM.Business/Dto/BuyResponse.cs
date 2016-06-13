
using VM.Business.Entities;

namespace VM.Business.Dto
{
    public enum ResponseCode
    {
        Ok = 0,
        /// <summary>
        /// мало денег в манетоприёмнике
        /// </summary>
        SmallCash = 1,
        /// <summary>
        /// нет сдачи
        /// </summary>
        NoTrifle = 2,
        NoGood = 3,
            /// <summary>
            /// мало денег у пользователя
            /// </summary>
        UserSmallCash = 4,

    };
    public class BuyResponse
    {
        public ResponseCode Code { get; set; }

        public User User { get; set; }

        public VendingMachine VendingMachine { get; set; }
    }
}
