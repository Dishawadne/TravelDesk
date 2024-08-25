using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;
using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Repository
{
    public class TravelRequestRepository : ITravelRequestRepository
    {
        private readonly DbContexts _context;

        public TravelRequestRepository(DbContexts context)
        {
            _context = context;
        }
        public async Task<TravelRequest> GetTravelRequestByIdAsync(int id)
        {
            return await _context.TravelRequests
                .Include(tr => tr.User)
                .Include(tr => tr.Manager)
                .FirstOrDefaultAsync(tr => tr.RequestId == id);
        }

        public async Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync()
        {
            return await _context.TravelRequests
                .Include(tr => tr.User)
                .Include(tr => tr.Manager)
                .ToListAsync();
        }

        public async Task CreateTravelRequestAsync(TravelRequest travelRequest)
        {
            _context.TravelRequests.Add(travelRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTravelRequestAsync(TravelRequest travelRequest)
        {
            _context.TravelRequests.Update(travelRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTravelRequestAsync(int id)
        {
            var travelRequest = await GetTravelRequestByIdAsync(id);
            if (travelRequest != null)
            {
                _context.TravelRequests.Remove(travelRequest);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<TravelRequest>> GetRequestsByManagerIdAsync(int managerId)
        {
            return await _context.TravelRequests
        .Where(tr => tr.ManagerId == managerId)
        .Include(tr => tr.Manager) // Ensure Manager is included
        .Include(tr => tr.User)    // Ensure User is included if needed
        .ToListAsync();
        }

        public async Task<TravelRequest> GetRequestByIdAsync(int requestId)
        {
            return await _context.TravelRequests
                                 .FindAsync(requestId);
        }

        public async Task UpdateRequestAsync(TravelRequest request)
        {
            _context.TravelRequests.Update(request);
            await _context.SaveChangesAsync();
        }


    }
}


//public async Task<IEnumerable<TravelRequest>> GetRequestsByEmployeeIdAsync(int employeeId)
//{
//    return await _context.TravelRequests
//        .Where(tr => tr.UserId == employeeId)
//        .Include(tr => tr.User)
//        .ToListAsync();
//}

//// Create a new travel request
//public async Task<TravelRequest> CreateRequestAsync(TravelRequest request)
//{
//    _context.TravelRequests.Add(request);
//    await _context.SaveChangesAsync();
//    return request;
//}

//// Update an existing travel request
//public async Task UpdateRequestAsync(TravelRequest request)
//{
//    var existingRequest = await _context.TravelRequests.FindAsync(request.RequestId);
//    if (existingRequest != null)
//    {
//        existingRequest.ProjectName = request.ProjectName;
//        existingRequest.Location = request.Location;
//        existingRequest.FromDate = request.FromDate;
//        existingRequest.ToDate = request.ToDate;
//        existingRequest.Status = request.Status;
//        existingRequest.ModifiedOn = request.ModifiedOn;
//        existingRequest.ModifiedBy = request.ModifiedBy;

//        _context.TravelRequests.Update(existingRequest);
//        await _context.SaveChangesAsync();
//    }
//}

//// Approve a travel request
//public async Task ApproveRequestAsync(int requestId, int managerId)
//{
//    var request = await _context.TravelRequests.FindAsync(requestId);
//    if (request != null)
//    {
//        request.Status = TravelRequestStatus.Approved;

//        request.ModifiedBy = managerId.ToString();
//        request.ModifiedOn = DateTime.Now;
//        _context.TravelRequests.Update(request);
//        await _context.SaveChangesAsync();
//    }
//}

//// Reject a travel request
//public async Task RejectRequestAsync(int requestId, int managerId)
//{
//    var request = await _context.TravelRequests.FindAsync(requestId);
//    if (request != null)
//    {
//        request.Status = TravelRequestStatus.Rejected;
//        request.ModifiedBy = managerId.ToString();
//        request.ModifiedOn = DateTime.Now;
//        _context.TravelRequests.Update(request);
//        await _context.SaveChangesAsync();
//    }




//}


