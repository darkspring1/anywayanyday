using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VM.Business.Services;
using VM.Business.Entities;
using VM.Test.Mock;
using VM.Business.Contracts;
using System.Collections.Generic;

namespace VM.Test
{
    [TestClass]
    public class VendingMachineServiceTest
    {

        VendingMachineService _vmService;
        PrivateObject _vmServicePrivateObject;

        Wallet EmptyWallet { get { return new Wallet(); } }
        Wallet FullWallet { get { return new Wallet { r1 = 100, r2 = 100, r5 = 100, r10 = 100 }; } }

        public VendingMachineServiceTest()
        {
            _vmService = new VendingMachineService(rp);
            _vmServicePrivateObject = new PrivateObject(_vmService);
        }

        Wallet GetTrifle(Wallet vmWallet, int trifle)
        {
            Object[] arg = { vmWallet, trifle };
            return (Wallet)_vmServicePrivateObject.Invoke("GetTrifle", arg);
        }

        [TestMethod]
        public void GetTrifleTest()
        {
            var wallet = new Wallet { r1 = 10, r2 = 0, r5 = 1, r10 = 1 };
            var wallet15 = new Wallet { r1 = 0, r2 = 0, r5 = 1, r10 = 1 };
            var wallet30 = new Wallet { r1 = 4, r2 = 3, r5 = 2, r10 = 1 };
            
            Assert.AreEqual(GetTrifle(FullWallet, 0).Total(), 0);
            Assert.IsNull(GetTrifle(EmptyWallet, 4));
            Assert.AreEqual(GetTrifle(FullWallet, 1).Total(), 1);

            //не удается набрать сдачу
            Assert.IsNull(GetTrifle(wallet15, 13).Total());
            Assert.IsNull(GetTrifle(wallet15, 13).Total());
        }

        [TestMethod]
        public void Buy()
        {
            int endedGoodId = GoodRepository.Goods.First(g => g.Count == 0).Id;
            int goodId = GoodRepository.Goods.First(g => g.Count > 0).Id;

            int noMoneyId = UserRepository.Users.First(u => u.Wallet.Total() == 0).Id;

            var contractSmallCash = new Buy
            {
                CashBox = EmptyWallet,
                GoodId = goodId,
                UserId = noMoneyId
            };

            var contractNoGood = new Buy
            {
                CashBox = FullWallet,
                GoodId = endedGoodId,
                UserId = noMoneyId
            };

            
            var contractOk = new Buy
            {
                CashBox = new Wallet { r10 = 1, r5 = 1, r2 = 2, r1 = 1 },
                GoodId = goodId,
                UserId = noMoneyId
            };

            var contractNoTrifle = new Buy
            {
                CashBox = new Wallet { r10 = 2 },
                GoodId = goodId,
                UserId = noMoneyId
            };
             

            Assert.AreEqual(_vmService.Buy(contractSmallCash).Code, ResponseCode.SmallCash);
            Assert.AreEqual(_vmService.Buy(contractNoGood).Code, ResponseCode.NoGood);
            Assert.AreEqual(_vmService.Buy(contractNoTrifle).Code, ResponseCode.NoTrifle);
            Assert.AreEqual(_vmService.Buy(contractOk).Code, ResponseCode.Ok);
            
        }
    }
}
