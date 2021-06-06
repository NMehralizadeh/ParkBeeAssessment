
namespace ParkBee.Assessment.Application.Interfaces
{
    public interface ILoggedInUserContext
    {
        string Name { get; }
        int GarageId { get; }
        bool IsAuthenticated { get; }
    }
}
