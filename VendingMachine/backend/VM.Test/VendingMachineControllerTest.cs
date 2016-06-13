using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VM.Business.Services;
using VM.Business.Entities;
using VM.Test.Mock;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using VM.Api.Controllers;
using VM.Business.Dto;
using VM.Business.Dal;
using VM.Test.Dal;
using VM.Test.Ioc;
using Container = StructureMap.Container;
using IContainer = StructureMap.IContainer;

namespace VM.Test
{
    [TestClass]
    public class VendingMachineControllerTest
    {


        private readonly IContainer _container;
        private readonly VendingMachineController _controller;
        private readonly IRepository<VendingMachine> _vmRepository;
        private readonly IRepository<User> _userRepository;

        public VendingMachineControllerTest()
        {
            _container = new Container(new TestRegistry());
            _controller = _container.GetInstance<VendingMachineController>();
            _vmRepository = _container.GetInstance<IRepository<VendingMachine>>();
            _userRepository = _container.GetInstance<IRepository<User>>();
        }

        dynamic ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }


        [TestMethod]
        public void InitForNewUserTest()
        {            
            var result = ToDynamic(_controller.Init());
            var newVm = _vmRepository.GetById(0);
            Assert.IsNotNull(newVm);
        }

        /*
        [TestMethod]
        public void InitForExistUserTest()
        {
            _vmRepository.Add(new VendingMachine { Id = 1, Wallet = new Wallet() });
            _userRepository.Add(new User { Id = 1, Wallet = new Wallet() });
            var result = _controller.Init(1);
            
            
        }
        */

        [ClassCleanup]
        public void ClassCleanup()
        {
            _container.Dispose();
        }

        [TestCleanup]
        public void TestClean()
        {
            DataContext.Clear();
        }
    }
}
