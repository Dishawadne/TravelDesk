using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class TravelAdminController : ControllerBase
        {
            private readonly DbContexts _context;

            public TravelAdminController(DbContexts context)
            {
                _context = context;
            }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var travelRequests = await _context.TravelRequests
                    .Include(tr => tr.User)
                    .Select(tr => new
                    {
                        tr.RequestId,
                        tr.UserId,
                        UserName = tr.User.FirstName + " " + (tr.User.LastName ?? ""),
                        tr.ProjectName,
                        tr.ReasonForTravelling,
                        tr.FromDate,
                        tr.ToDate,
                        tr.FromLocation,
                        tr.ToLocation,
                        tr.Comments,
                        tr.TicketUrl,
                        Status = tr.Status.ToString() 
                    })
                    .ToListAsync();

                if (travelRequests == null || travelRequests.Count == 0)
                {
                    return NotFound("No travel requests found.");
                }

                return Ok(travelRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("BookTicket/{travelRequestId}")]
            public async Task<IActionResult> BookTicket(int travelRequestId, [FromBody] BookingDetails bookingDetails)
            {
                try
                {
                    var travelRequest = await _context.TravelRequests
                        .Include(tr => tr.User) 
                        .FirstOrDefaultAsync(tr => tr.RequestId == travelRequestId);

                    if (travelRequest == null)
                    {
                        return NotFound("Travel request not found.");
                    }

                    travelRequest.Status = TravelRequestStatus.Booked;
                    travelRequest.Comments = bookingDetails.Comments; // Update comments

                    // Save the booking confirmation URL if available
                    if (!string.IsNullOrEmpty(bookingDetails.TicketUrl))
                    {
                        travelRequest.TicketUrl = bookingDetails.TicketUrl;
                    }

                    _context.TravelRequests.Update(travelRequest);
                    await _context.SaveChangesAsync();

                    // Notify the employee via the history update
                    return Ok("Booking confirmed successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPost("ReturnToManager/{travelRequestId}")]
            public async Task<IActionResult> ReturnToManager(int travelRequestId, [FromBody] BookingDetails bookingDetails)
            {
                try
                {
                    var travelRequest = await _context.TravelRequests
                        .FirstOrDefaultAsync(tr => tr.RequestId == travelRequestId);

                    if (travelRequest == null)
                    {
                        return NotFound("Travel request not found.");
                    }

                    // Reassign request to manager
                    travelRequest.Status = TravelRequestStatus.ReturnedToManager;
                    travelRequest.Comments = bookingDetails.Comments;


                    _context.TravelRequests.Update(travelRequest);
                    await _context.SaveChangesAsync();

                    return Ok("Request returned to manager successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPost("ReturnToEmployee/{travelRequestId}")]
            public async Task<IActionResult> ReturnToEmployee(int travelRequestId, [FromBody] BookingDetails bookingDetails)
            {
                try
                {
                    var travelRequest = await _context.TravelRequests
                        .FirstOrDefaultAsync(tr => tr.RequestId == travelRequestId);

                    if (travelRequest == null)
                    {
                        return NotFound("Travel request not found.");
                    }

                    // Reassign request to employee
                    travelRequest.Status = TravelRequestStatus.ReturnedToEmployee;
                    travelRequest.Comments = bookingDetails.Comments;


                    _context.TravelRequests.Update(travelRequest);
                    await _context.SaveChangesAsync();

                    return Ok("Request returned to employee successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPost("CloseRequest/{travelRequestId}")]
            public async Task<IActionResult> CloseRequest(int travelRequestId, [FromBody] BookingDetails bookingDetails)
            {
                // Ensure that CommentsRequest class matches the expected JSON payload
                try
                {
                    var travelRequest = await _context.TravelRequests
                        .FirstOrDefaultAsync(tr => tr.RequestId == travelRequestId);

                    if (travelRequest == null)
                    {
                        return NotFound("Travel request not found.");
                    }

                    // Close the request with complete status
                    travelRequest.Status = TravelRequestStatus.Completed;
                    //travelRequest.Comments = bookingDetails.Comments;


                    _context.TravelRequests.Update(travelRequest);
                    await _context.SaveChangesAsync();

                    return Ok("Request closed successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }

