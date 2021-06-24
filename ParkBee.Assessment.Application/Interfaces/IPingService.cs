using System;
using System.Net;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Interfaces
{
    public interface IPingService
    {
        Task<bool> HasPing(IPAddress ip, int retryCount, TimeSpan interval);
        Task<bool> HasPing(IPAddress ip);
    }
}