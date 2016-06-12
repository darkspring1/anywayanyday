namespace VM.Business.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
