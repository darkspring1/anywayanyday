

using VM.Business.Entities;

namespace VM.Business.Dto
{
    public class InitResponse
    {
        public User User { get; set; }

        public VendingMachine VendingMachine { get; set; }
    }
}
