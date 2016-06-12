using VM.Business.Dal;
using VM.Business.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VM.Business.Contracts;

namespace VM.Business.Services
{
    public class VendingMachineService
    {
        private readonly IRepository<VendingMachine> _vendingMachineRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Good> _goodRepository;
        public VendingMachineService(IRepository<VendingMachine> vendingMachineRepository, IRepository<User> userRepository,
            IRepository<Good> goodRepository)
        {
            _vendingMachineRepository = vendingMachineRepository;
            _userRepository = userRepository;
            _goodRepository = goodRepository;
        }


        void MoveAllCoins (Wallet destinationW, Wallet sourceW)
        {
            destinationW.r1 += sourceW.r1;
            destinationW.r2 += sourceW.r2;
            destinationW.r5 += sourceW.r5;
            destinationW.r10 += sourceW.r10;

            sourceW.r1 = 0;
            sourceW.r2 = 0;
            sourceW.r5 = 0;
            sourceW.r10 = 0;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="minuendW">уменьшаемое</param>
        /// <param name="subtrahend">вычитаемое</param>
        void SubWallets(Wallet minuendW, Wallet subtrahend)
        {
            minuendW.r1 -= subtrahend.r1;
            minuendW.r2 -= subtrahend.r2;
            minuendW.r5 -= subtrahend.r5;
            minuendW.r10 -= subtrahend.r10;

        }

        /// <summary>
        /// возвращаем сдачу
        /// </summary>
        /// <param name="source"></param>
        /// <param name="trifle"></param>
        /// <returns></returns>

        Wallet GetTrifle(Wallet source, int trifle)
        {
            Wallet trifleWallet = new Wallet();

            int r10 = trifle / 10;
            if (r10 != 0 && source.r10 >= r10)
            {
                trifleWallet.r10 += r10;
                source.r10 -= r10;
                trifle -= r10 * 10;
            }

            int r5 = trifle / 5;
            if (r5 != 0 && source.r5 >= r5)
            {
                trifleWallet.r5 += r5;
                source.r5 -= r5;
                trifle -= r5 * 5;
            }

            int r2 = trifle / 2;
            if (r2 != 0 && source.r2 >= r2)
            {
                trifleWallet.r2 += r2;
                source.r2 -= r2;
                trifle -= r2 * 2;
            }

            if (trifle != 0 && source.r1 >= trifle)
            {
                trifleWallet.r1 += trifle;
                source.r1 -= trifle;
                trifle = 0;
            }

            return trifle == 0 ? trifleWallet : null;

        }

        private Wallet GetInitedWallet()
        {
            return new Wallet
            {
                r1 = 100,
                r2 = 100,
                r5 = 100,
                r10 = 100
            };
        }

        public User CreateNewUser()
        {
            User u = new User {Wallet = GetInitedWallet()};
            _userRepository.Add(u);
            _userRepository.SaveChanges();
            return u;
        }


        public User GetUserById(int id)
        {
            return _userRepository
                .GetAll()
                .Include(u => u.Wallet)
                .FirstOrDefault(u => u.Id == id);
        }

        public VendingMachine GetVendingMachine()
        {
            return _vendingMachineRepository
                .GetAll()
                .Include(vm => vm.Wallet)
                .Include(vm => vm.Goods)
                .FirstOrDefault();
        }

        public VendingMachine CreateVendingMachine()
        {
            var vm = new VendingMachine
            {
                Wallet = GetInitedWallet(),
                Goods = new List<Good>()
                {
                    new Good {Name = "Чай", Count = 10, Price = 13},
                    new Good {Name = "Кофе", Count = 20, Price = 18},
                    new Good {Name = "Кофе с молоком", Count = 21, Price = 20},
                    new Good {Name = "Сок", Count = 15, Price = 35}
                }
            };


            _vendingMachineRepository.Add(vm);
            _vendingMachineRepository.SaveChanges();
            return vm;
        }

        public BuyResponse Buy(Buy contract)
        {

            var good = _goodRepository.GetById(contract.GoodId);

            if (good.Count <= 0)
            {
                return new BuyResponse {Code = ResponseCode.NoGood};
            }

            if (good.Price > contract.CashBox.Total())
            {
                return new BuyResponse {Code = ResponseCode.SmallCash};
            }


            var vm = GetVendingMachine();

            int trifle = contract.CashBox.Total() - good.Price;

            var usr = GetUserById(contract.UserId);

            SubWallets(usr.Wallet, contract.CashBox);
            MoveAllCoins(vm.Wallet, contract.CashBox);

            var trifleWallet = GetTrifle(vm.Wallet, trifle);

            if (trifleWallet == null)
            {
                return new BuyResponse {Code = ResponseCode.NoTrifle};
            }


            MoveAllCoins(usr.Wallet, trifleWallet);
            good.Count--;
            _goodRepository.SaveChanges();

            return new BuyResponse
            {
                Code = ResponseCode.Ok,
                User = usr,
                VendingMachine = vm
            };

        }

    }
}
