using TravelDesk.Models;

namespace TravelDesk.IRepository
{
    public interface ITravelRequestRepository
    {
        
        Task<TravelRequest> GetTravelRequestByIdAsync(int id);
        Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync();
        Task CreateTravelRequestAsync(TravelRequest travelRequest);
        Task UpdateTravelRequestAsync(TravelRequest travelRequest);
        Task DeleteTravelRequestAsync(int id);


        Task<IEnumerable<TravelRequest>> GetRequestsByManagerIdAsync(int managerId);
        Task<TravelRequest> GetRequestByIdAsync(int requestId);
        Task UpdateRequestAsync(TravelRequest request);
    }
}
