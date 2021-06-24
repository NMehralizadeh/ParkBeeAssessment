using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkBee.Assessment.Application.Garages.Commands;
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

        [HttpPost("RefreshStatus/{doorId:int}")]
        public async Task<bool> RefreshDoorStatus(int doorId)
        {
            return await Mediator.Send(new RefreshDoorStatusCommand { DoorId = doorId }).ConfigureAwait(false);
        }
    }
}