using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Dal;
using VM.Business.Entities;

namespace VM.Test.Mock
{
    public class VendingMachineRepository : IRepository<VendingMachine>
    {


        public static List<VendingMachine> VendingMachines = new List<VendingMachine>
        {
            new VendingMachine { Id = 1, Wallet = new Wallet() },
        };


        public IQueryable<VendingMachine> GetAll()
        {
            return VendingMachines.AsQueryable();
        }

        public VendingMachine GetById(object key)
        {
            return VendingMachines.FirstOrDefault(u => u.Id == (int)key);
        }

        public void Add(VendingMachine entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            
        }


    }
}
