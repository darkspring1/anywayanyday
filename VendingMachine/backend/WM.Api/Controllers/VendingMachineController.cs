using VM.Business.Entities;
using VM.Business.Services;
using System.Web.Http;
using VM.Business.Contracts;


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
        public object Buy([FromBody]Buy c)
        {
            return _vmService.Buy(c);
        }

    }
}
