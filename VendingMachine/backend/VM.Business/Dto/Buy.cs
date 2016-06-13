using VM.Business.Entities;

namespace VM.Business.Dto
{
    public class Buy
    {
        public int UserId { get; set; }
        
        public int GoodId { get; set; }

        /// <summary>
        /// манетоприёмник
        /// </summary>
        public Wallet CashBox { get; set; }
    }
}
