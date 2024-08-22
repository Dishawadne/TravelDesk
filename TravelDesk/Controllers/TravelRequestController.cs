//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TravelDesk.IRepository;
//using TravelDesk.Models;

//namespace TravelDesk.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TravelRequestController : ControllerBase
//    {
//        private readonly ITravelRequestRepository _repository;

//        public TravelRequestController(ITravelRequestRepository repository)
//        {
//            _repository = repository;
//        }

//        [HttpGet("{employeeId}/history")]
//        public async Task<ActionResult<IEnumerable<TravelRequest>>> GetHistory(int employeeId)
//        {
//            var history = await _repository.GetRequestsByEmployeeIdAsync(employeeId);
//            return Ok(history);
//        }

//        [HttpPost]
//        public async Task<ActionResult<TravelRequest>> CreateRequest([FromForm] TravelRequest request, [FromForm] List<IFormFile> documents)
//        {
//            request.DateOfRequest = DateTime.Now;
//            request.IsSubmitted = false;
//            request.Status = "New";

//            foreach (var document in documents)
//            {
//                var docPath = Path.Combine("wwwroot", "Documents", document.FileName);
//                using (var stream = new FileStream(docPath, FileMode.Create))
//                {
//                    await document.CopyToAsync(stream);
//                }

//                var doc = new Document
//                {
//                    FileName = document.FileName,
//                    FilePath = docPath,
//                    DocumentType = "Uploaded", // Determine type dynamically based on form input
//                    TravelRequest = request
//                };

//                await _repository.SaveDocumentAsync(doc);
//            }

//            var createdRequest = await _repository.CreateRequestAsync(request);
//            return CreatedAtAction(nameof(GetHistory), new { employeeId = request.EmployeeId }, createdRequest);
//        }

//        [HttpPost("{requestId}/submit")]
//        public async Task<IActionResult> SubmitRequest(int requestId)
//        {
//            var request = await _repository.GetRequestByIdAsync(requestId);
//            if (request == null) return NotFound();

//            request.IsSubmitted = true;
//            request.Status = "Submitted";

//            // Generate unique request number
//            request.TravelRequestId = new Random().Next(100000, 999999);  // Or use a GUID or database sequence


//            await _repository.UpdateRequestAsync(request);
//            return Ok();
//        }

//        [HttpDelete("{requestId}")]
//        public async Task<IActionResult> DeleteRequest(int requestId)
//        {
//            await _repository.DeleteRequestAsync(requestId);
//            return NoContent();
//        }
//    }

//}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public TravelRequestController(ITravelRequestRepository travelRequestRepository, IWebHostEnvironment environment)
        {
            _travelRequestRepository = travelRequestRepository;
            _environment = environment;
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

        // POST: api/TravelRequest
        [HttpPost]
        public async Task<ActionResult<TravelRequest>> CreateTravelRequest([FromForm] TravelRequestDto travelRequestDto)
        {
            if (travelRequestDto == null)
            {
                return BadRequest("Invalid data.");
            }

            string fileName = null;

            if (travelRequestDto.AddharFile != null)
            {
                // Save Aadhaar card file to a folder
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "aadhaarUploads");
                fileName = Guid.NewGuid().ToString() + "_" + travelRequestDto.AddharFile.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await travelRequestDto.AddharFile.CopyToAsync(fileStream);
                }
            }

            // Create a new TravelRequest object
            var travelRequest = new TravelRequest
            {
                UserId = travelRequestDto.UserId,
                ReasonForTravelling = travelRequestDto.ReasonForTravelling,
                ManagerId = travelRequestDto.ManagerId,
                ProjectName = travelRequestDto.ProjectName,
                Location = travelRequestDto.Location,
                FromDate = travelRequestDto.FromDate,
                ToDate = travelRequestDto.ToDate,
                Status = travelRequestDto.Status,
                AddharCard = fileName // Save the file path
            };

            await _travelRequestRepository.CreateTravelRequestAsync(travelRequest);

            return CreatedAtAction(nameof(GetTravelRequest), new { id = travelRequest.RequestId }, travelRequest);
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

            if (travelRequestDto.AddharFile != null)
            {
                // Save the new Aadhaar card file if uploaded
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "aadhaarUploads");
                fileName = Guid.NewGuid().ToString() + "_" + travelRequestDto.AddharFile.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await travelRequestDto.AddharFile.CopyToAsync(fileStream);
                }

                // Optionally: delete the old Aadhaar file
                var oldFilePath = Path.Combine(uploadsFolder, existingTravelRequest.AddharCard);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Update the travel request details
            existingTravelRequest.UserId = travelRequestDto.UserId;
            existingTravelRequest.ReasonForTravelling = travelRequestDto.ReasonForTravelling;
            existingTravelRequest.ManagerId = travelRequestDto.ManagerId;
            existingTravelRequest.ProjectName = travelRequestDto.ProjectName;
            existingTravelRequest.Location = travelRequestDto.Location;
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
    }
}
