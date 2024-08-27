using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;
using TravelDesk.DTO;
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
        public async Task<IEnumerable<TravelRequestHistoryDto>> GetRequestsByManagerIdAsync(int managerId)
        {
          
            var travelRequests = await _context.TravelRequests
                .Where(tr => tr.ManagerId == managerId)
                .Include(tr => tr.Manager)
                .Include(tr => tr.User)
                .ToListAsync();

           
            var travelRequestHistory = travelRequests.Select(tr => new TravelRequestHistoryDto
            {
                RequestId = tr.RequestId, 
                ProjectName = tr.ProjectName,
                FromLocation = tr.FromLocation,
                ToLocation = tr.ToLocation,
                ReasonForTravelling = tr.ReasonForTravelling,
                Status = tr.Status.ToString(), 
                FromDate = tr.FromDate,
                ToDate = tr.ToDate,
                UserName = $"{tr.User.FirstName} {tr.User.LastName}"
                //CreatedOn = tr.CreatedOn
            }).ToList();

            // Step 4: Return the DTO list
            return travelRequestHistory;
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



