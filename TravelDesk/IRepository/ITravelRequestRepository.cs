using TravelDesk.Models;

namespace TravelDesk.IRepository
{
    public interface ITravelRequestRepository
    {
        //Task<IEnumerable<TravelRequest>> GetAllRequests();
        //Task<TravelRequest> GetRequestById(int id);
        //Task<TravelRequest> AddRequest(TravelRequest travelRequest);
        //Task UpdateRequest(TravelRequest travelRequest);
        //Task DeleteRequest(int id);
        Task<TravelRequest> GetTravelRequestByIdAsync(int id);
        Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync();
        Task CreateTravelRequestAsync(TravelRequest travelRequest);
        Task UpdateTravelRequestAsync(TravelRequest travelRequest);
        Task DeleteTravelRequestAsync(int id);

    }
}
