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
        public InitResponse Init([FromUri]int? userId = null)
        {
            return _vmService.Init(userId);
        }

        [HttpPost]
        public BuyResponse Buy([FromBody]Buy model)
        {
            return _vmService.Buy(model);
        }

    }
}
