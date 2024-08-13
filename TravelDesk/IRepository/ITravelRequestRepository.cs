using TravelDesk.Models;

namespace TravelDesk.IRepository
{
    public interface ITravelRequestRepository
    {
        Task<TravelRequest> CreateRequestAsync(TravelRequest request);
        Task<TravelRequest> GetRequestByIdAsync(int requestId);
        Task<IEnumerable<TravelRequest>> GetRequestsByEmployeeIdAsync(int employeeId);
        Task UpdateRequestAsync(TravelRequest request);
        Task DeleteRequestAsync(int requestId);
        Task SaveDocumentAsync(Document document);
    }
}
