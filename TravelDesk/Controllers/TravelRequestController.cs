using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelDesk.Context;
using TravelDesk.DTO;
using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRequestController : ControllerBase
    {
        private readonly ITravelRequestRepository _travelRequestRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly DbContexts _context;

        public TravelRequestController(ITravelRequestRepository travelRequestRepository, IWebHostEnvironment environment, DbContexts context)
        {
            _travelRequestRepository = travelRequestRepository;
            _environment = environment;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelRequest>>> GetAllTravelRequests()
        {
            var travelRequests = await _travelRequestRepository.GetAllTravelRequestsAsync();
            return Ok(travelRequests);
        }

        // GET: api/TravelRequest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelRequest>> GetTravelRequest(int id)
        {
            var travelRequest = await _travelRequestRepository.GetTravelRequestByIdAsync(id);

            if (travelRequest == null)
            {
                return NotFound();
            }

            return Ok(travelRequest);
        }

        [HttpPost]
        public async Task<ActionResult<TravelRequest>> CreateTravelRequest([FromForm] TravelRequestDto travelRequestDto)
        {
            try
            {
                if (travelRequestDto == null)
                {
                    return BadRequest("Invalid data.");
                }

                string fileName = null;

                if (travelRequestDto.AddharCard != null)
                {
                    // Save Aadhaar card file to a folder
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "aadhaarUploads");
                    fileName = Guid.NewGuid().ToString() + "_" + travelRequestDto.AddharCard.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await travelRequestDto.AddharCard.CopyToAsync(fileStream);
                    }
                }

                var travelRequest = new TravelRequest
                {
                    UserId = travelRequestDto.UserId,
                    ReasonForTravelling = travelRequestDto.ReasonForTravelling,
                    ManagerId = travelRequestDto.ManagerId,
                    ProjectName = travelRequestDto.ProjectName,
                    FromLocation = travelRequestDto.FromLocation,
                    ToLocation = travelRequestDto.ToLocation,
                    FromDate = travelRequestDto.FromDate,
                    ToDate = travelRequestDto.ToDate,
                    Status = travelRequestDto.Status,
                    AddharCard = fileName
                };

                await _travelRequestRepository.CreateTravelRequestAsync(travelRequest);

                return CreatedAtAction(nameof(GetTravelRequest), new { id = travelRequest.RequestId }, travelRequest);
            }
            catch (Exception ex)
            {
                // Log the exception
                //  _logger.LogError(ex, "Error creating travel request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TravelRequestHistoryDto>>> GetTravelRequestsByUserId(int userId)
        {
            var travelRequests = await _context.TravelRequests
                .Where(tr => tr.UserId == userId)
                .Include(tr => tr.User) // Include User data if needed
                .ToListAsync();

            if (travelRequests == null || !travelRequests.Any())
            {
                return NotFound();
            }

            // Map to DTO
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
                Comments = tr.Comments,
                CreatedOn = tr.CreatedOn
            }).ToList();

            return Ok(travelRequestHistory);
        }


        // PUT: api/TravelRequest/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelRequest(int id, [FromForm] TravelRequestDto travelRequestDto)
        {
            var existingTravelRequest = await _travelRequestRepository.GetTravelRequestByIdAsync(id);
            if (existingTravelRequest == null)
            {
                return NotFound();
            }

            string fileName = existingTravelRequest.AddharCard;

            if (travelRequestDto.AddharCard != null)
            {
               
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "aadhaarUploads");
                fileName = Guid.NewGuid().ToString() + "_" + travelRequestDto.AddharCard.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await travelRequestDto.AddharCard.CopyToAsync(fileStream);
                }

                // Optionally: delete the old Aadhaar file
                var oldFilePath = Path.Combine(uploadsFolder, existingTravelRequest.AddharCard);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            existingTravelRequest.ReasonForTravelling = travelRequestDto.ReasonForTravelling;
            existingTravelRequest.ProjectName = travelRequestDto.ProjectName;
            existingTravelRequest.FromLocation = travelRequestDto.FromLocation;
            existingTravelRequest.ToLocation = travelRequestDto.ToLocation;
            existingTravelRequest.FromDate = travelRequestDto.FromDate;
            existingTravelRequest.ToDate = travelRequestDto.ToDate;
            existingTravelRequest.Status = travelRequestDto.Status;
            existingTravelRequest.AddharCard = fileName;

            await _travelRequestRepository.UpdateTravelRequestAsync(existingTravelRequest);

            return NoContent();
        }

        // DELETE: api/TravelRequest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelRequest(int id)
        {
            var existingTravelRequest = await _travelRequestRepository.GetTravelRequestByIdAsync(id);
            if (existingTravelRequest == null)
            {
                return NotFound();
            }

            // Optionally: delete Aadhaar file
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "aadhaarUploads");
            var filePath = Path.Combine(uploadsFolder, existingTravelRequest.AddharCard);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await _travelRequestRepository.DeleteTravelRequestAsync(id);

            return NoContent();
        }


        // Get all requests for a specific manager
        [HttpGet("manager/{managerId}")]
        public async Task<IActionResult> GetRequestsByManagerIdAsync(int managerId)
        {
            var requests = await _travelRequestRepository.GetRequestsByManagerIdAsync(managerId);
            if (requests == null)
            {
                return NotFound();
            }
            return Ok(requests);
        }

        // Get a specific travel request by ID
        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequestByIdAsync(int requestId)
        {
            var request = await _travelRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }


        [HttpPost("{requestId}/approve")]
        public async Task<IActionResult> ApproveRequestAsync(int requestId, [FromBody] RequestActionModel model)
        {
            var request = await _travelRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            request.Status = TravelRequestStatus.Approved;
            request.Comments = model.Comments;
            await _travelRequestRepository.UpdateRequestAsync(request);

            return NoContent();
        }

        [HttpPost("{requestId}/reject")]
        public async Task<IActionResult> RejectRequestAsync(int requestId, [FromBody] RequestActionModel model)
        {
            var request = await _travelRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            request.Status = TravelRequestStatus.Rejected;
            request.Comments = model.Comments;
            await _travelRequestRepository.UpdateRequestAsync(request);

            return NoContent();
        }

        [HttpPost("{requestId}/return")]
        public async Task<IActionResult> ReturnRequestAsync(int requestId, [FromBody] RequestActionModel model)
        {
            var request = await _travelRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            //  request.Status = TravelRequestStatus.Returned; // Assuming 'Returned' status exists
            request.Comments = model.Comments;
            await _travelRequestRepository.UpdateRequestAsync(request);

            return NoContent();
        }
    }
}
