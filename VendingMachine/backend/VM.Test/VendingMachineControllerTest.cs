using Microsoft.VisualStudio.TestTools.UnitTesting;
using VM.Business.Entities;
using VM.Api.Controllers;
using VM.Business.Dal;
using VM.Test.Dal;
using VM.Test.Ioc;
using Container = StructureMap.Container;
using IContainer = StructureMap.IContainer;
using VM.Business.Dto;
using System.Linq;

namespace VM.Test
{
    [TestClass]
    public class VendingMachineControllerTest
    {
        private readonly IContainer _container;
        private readonly VendingMachineController _controller;
        private readonly IRepository<VendingMachine> _vmRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Good> _goodRepository;

        Wallet EmptyWallet { get { return new Wallet(); } }
        Wallet FullWallet { get { return new Wallet { r1 = 100, r2 = 100, r5 = 100, r10 = 100 }; } }

        void AddGoodsToRepository()
        {
            _goodRepository.Add(new Good { Id = 1, Name = "Чай", Count = 0, Price = 13 });
            _goodRepository.Add(new Good { Id = 2, Name = "Кофе", Count = 1, Price = 18 });
            _goodRepository.Add(new Good { Id = 3, Name = "Кофе с молоком", Count = 1, Price = 20 });
            _goodRepository.Add(new Good { Id = 4, Name = "Сок", Count = 1, Price = 35 });
        }

        public VendingMachineControllerTest()
        {
            _container = new Container(new TestRegistry());
            _controller = _container.GetInstance<VendingMachineController>();
            _vmRepository = _container.GetInstance<IRepository<VendingMachine>>();
            _userRepository = _container.GetInstance<IRepository<User>>();
            _goodRepository = _container.GetInstance<IRepository<Good>>();
        }

        [TestMethod]
        public void InitForNewUserTest()
        {
            InitResponse result = _controller.Init();
            Assert.IsNotNull(result.VendingMachine);
            Assert.IsNotNull(result.User);
        }


        [TestMethod]
        public void InitForExistUserTest()
        {
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = new Wallet() });
            _userRepository.Add(new User { Id = 1, Wallet = new Wallet() });
            var result =  _controller.Init(1);
            Assert.AreEqual(result.VendingMachine.Id, 1);
            Assert.AreEqual(result.User.Id, 1);
        }

        [TestMethod]
        public void InitForNotExistUserTest()
        {
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = new Wallet() });
            var result = _controller.Init(1);
            Assert.AreEqual(result.VendingMachine.Id, 1);
            Assert.AreEqual(result.User.Id, 0);
        }

        [TestMethod]
        public void BuySmallCashTest()
        {
            AddGoodsToRepository();
            int noMoneyId = 1;
            int goodId = _goodRepository.GetAll().First(g => g.Count > 0).Id;
            _userRepository.Add(new User { Id = noMoneyId, Wallet = new Wallet() });

            var contractSmallCash = new Buy
            {
                CashBox = EmptyWallet,
                GoodId = goodId,
                UserId = noMoneyId
            };

            Assert.AreEqual(_controller.Buy(contractSmallCash).Code, ResponseCode.SmallCash);
        }

        [TestMethod]
        public void BuyNoGoodTest()
        {
            AddGoodsToRepository();
            int endedGoodId = _goodRepository.GetAll().First(g => g.Count == 0).Id;
            int noMoneyId = 1;
            _userRepository.Add(new User { Id = noMoneyId, Wallet = new Wallet() });

            var contractNoGood = new Buy
            {
                CashBox = FullWallet,
                GoodId = endedGoodId,
                UserId = noMoneyId
            };

            Assert.AreEqual(_controller.Buy(contractNoGood).Code, ResponseCode.NoGood);
        }


        [TestMethod]
        public void BuyOkTest()
        {
            AddGoodsToRepository();
            int userId = 1;
            int goodId = _goodRepository.GetAll().First(g => g.Count > 0).Id;
            _userRepository.Add(new User { Id = userId, Wallet = FullWallet });
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = FullWallet });

            var contractOk = new Buy
            {
                CashBox = new Wallet { r10 = 1, r5 = 1, r2 = 2, r1 = 1 },
                GoodId = goodId,
                UserId = userId
            };

            Assert.AreEqual(_controller.Buy(contractOk).Code, ResponseCode.Ok);
        }

        [TestMethod]
        public void BuyUserSmallCashTest()
        {
            /// на клиенте на этот случай есть валидация
            AddGoodsToRepository();
            int userId = 1;
            int goodId = _goodRepository.GetAll().First(g => g.Count > 0).Id;
            _userRepository.Add(new User { Id = userId, Wallet = EmptyWallet });
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = FullWallet });

            var contractOk = new Buy
            {
                CashBox = new Wallet { r10 = 1, r5 = 1, r2 = 2, r1 = 1 },
                GoodId = goodId,
                UserId = userId
            };

            Assert.AreEqual(_controller.Buy(contractOk).Code, ResponseCode.UserSmallCash);
        }

        [TestMethod]
        public void BuyNoTrifleTest()
        {
            AddGoodsToRepository();
            int userId = 1;
            int goodId = _goodRepository.GetAll().First(g => g.Count > 0).Id;
            _userRepository.Add(new User { Id = userId, Wallet = FullWallet });
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = EmptyWallet });

            var contractNoTrifle = new Buy
            {
                CashBox = new Wallet { r10 = 2 },
                GoodId = goodId,
                UserId = userId
            };

            Assert.AreEqual(_controller.Buy(contractNoTrifle).Code, ResponseCode.NoTrifle);
        }


        //[ClassCleanup]
        //public void ClassCleanup()
        //{
        //    _container.Dispose();
        //}

        [TestCleanup]
        public void TestClean()
        {
            DataContext.Clear();
        }
    }
}
