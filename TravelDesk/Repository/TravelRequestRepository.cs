//using Microsoft.EntityFrameworkCore;
//using TravelDesk.Context;
//using TravelDesk.IRepository;
//using TravelDesk.Models;

//namespace TravelDesk.Repository
//{
//    public class TravelRequestRepository : ITravelRequestRepository
//    {
//        private readonly DbContexts _context;

//        public TravelRequestRepository(DbContexts context)
//        {
//            _context = context;
//        }

//        public async Task<TravelRequest> CreateRequestAsync(TravelRequest request)
//        {
//            _context.TravelRequests.Add(request);
//            await _context.SaveChangesAsync();
//            return request;
//        }

//        public async Task<TravelRequest> GetRequestByIdAsync(int requestId)
//        {
//            return await _context.TravelRequests
//                .Include(r => r.Documents)
//                .Include(r => r.Employee)
//                .FirstOrDefaultAsync(r => r.TravelRequestId == requestId);
//        }

//        public async Task<IEnumerable<TravelRequest>> GetRequestsByEmployeeIdAsync(int employeeId)
//        {
//            return await _context.TravelRequests
//                .Where(r => r.EmployeeId == employeeId)
//                .Include(r => r.Documents)
//                .ToListAsync();
//        }

//        public async Task UpdateRequestAsync(TravelRequest request)
//        {
//            _context.TravelRequests.Update(request);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteRequestAsync(int requestId)
//        {
//            var request = await _context.TravelRequests.FindAsync(requestId);
//            _context.TravelRequests.Remove(request);
//            await _context.SaveChangesAsync();
//        }

//        public async Task SaveDocumentAsync(Document document)
//        {
//            _context.Documents.Add(document);
//            await _context.SaveChangesAsync();
//        }
//    }
//}

