using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using VM.Business.Entities;
using VM.Business.Services;
using System.Web.Http;
using VM.Business.Dto;


namespace VM.Api.Controllers
{
    public class VendingMachineController : ApiController
    {

        VendingMachineService _vmService;
        public VendingMachineController(VendingMachineService vmService)
        {
            _vmService = vmService;
        }

        
            
        

        [HttpGet]
        public object Init([FromUri]int? userId = null)
        {
            User u;
            if (userId == null)
            {
                u = _vmService.CreateNewUser();
            }
            else
            {
                u = _vmService.GetUserById(userId.Value);
                u = u ?? _vmService.CreateNewUser();
            }

            var vm = _vmService.GetVendingMachine() ?? _vmService.CreateVendingMachine();
            
            return new { user = u, vendingMachine = vm };
        }

        [HttpPost]
        public BuyResponse Buy([FromBody]Buy model)
        {
            return _vmService.Buy(model);
        }

    }
}
