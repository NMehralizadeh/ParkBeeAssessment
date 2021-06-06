using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkBee.Assessment.Application.Garages.Queries.GetGarageDetails;

namespace ParkBee.Assessment.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class GarageController : BaseController
    {
        [HttpGet]
        public async Task<GarageDto> GetGarageDetails()
        {
            return await Mediator.Send(new GetGarageDetailsQuery()).ConfigureAwait(false);
        }
    }
}