using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Garages.Commands
{
    public class RefreshDoorStatusCommandHandler : IRequestHandler<RefreshDoorStatusCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDoorStatusService _doorStatusService;
        private readonly ILoggedInUserContext _loggedInUserContext;

        public RefreshDoorStatusCommandHandler(IApplicationDbContext dbContext, IDoorStatusService doorStatusService, ILoggedInUserContext loggedInUserContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _doorStatusService = doorStatusService ?? throw new ArgumentNullException(nameof(doorStatusService));
            _loggedInUserContext = loggedInUserContext ?? throw new ArgumentNullException(nameof(loggedInUserContext));
        }

        public async Task<bool> Handle(RefreshDoorStatusCommand request, CancellationToken cancellationToken)
        {
            var door = await _dbContext.DoorRepository.GetDoorWithLatestStatus(request.DoorId, _loggedInUserContext.GarageId);
            if (door == null)
                throw new NotFoundException($"Door Id={request.DoorId} not found");

            var isOnline = await _doorStatusService.CheckDoorStatus(door);
            await _dbContext.DoorRepository.ChangeDoorStatus(door, isOnline);
            return isOnline;
        }
    }
}
