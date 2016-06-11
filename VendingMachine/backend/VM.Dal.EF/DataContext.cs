using System;
using System.Data.Entity;
using VM.Business.Entities;

namespace VM.Dal.EF
{
    public class DataContext : DbContext
    {
        const string schema = "dbo";

        public DataContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.Log = q => Console.WriteLine(q);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VendingMachine>().ToTable("VendingMachines", schema);
            modelBuilder.Entity<User>().ToTable("Users", schema);
            modelBuilder.Entity<Wallet>().ToTable("Wallets", schema);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
