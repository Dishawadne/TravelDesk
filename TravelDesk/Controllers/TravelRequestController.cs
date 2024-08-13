using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelDesk.IRepository;
using TravelDesk.Models;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRequestController : ControllerBase
    {
        private readonly ITravelRequestRepository _repository;

        public TravelRequestController(ITravelRequestRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{employeeId}/history")]
        public async Task<ActionResult<IEnumerable<TravelRequest>>> GetHistory(int employeeId)
        {
            var history = await _repository.GetRequestsByEmployeeIdAsync(employeeId);
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<TravelRequest>> CreateRequest([FromForm] TravelRequest request, [FromForm] List<IFormFile> documents)
        {
            request.DateOfRequest = DateTime.Now;
            request.IsSubmitted = false;
            request.Status = "New";

            foreach (var document in documents)
            {
                var docPath = Path.Combine("wwwroot", "Documents", document.FileName);
                using (var stream = new FileStream(docPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }

                var doc = new Document
                {
                    FileName = document.FileName,
                    FilePath = docPath,
                    DocumentType = "Uploaded", // Determine type dynamically based on form input
                    TravelRequest = request
                };

                await _repository.SaveDocumentAsync(doc);
            }

            var createdRequest = await _repository.CreateRequestAsync(request);
            return CreatedAtAction(nameof(GetHistory), new { employeeId = request.EmployeeId }, createdRequest);
        }

        [HttpPost("{requestId}/submit")]
        public async Task<IActionResult> SubmitRequest(int requestId)
        {
            var request = await _repository.GetRequestByIdAsync(requestId);
            if (request == null) return NotFound();

            request.IsSubmitted = true;
            request.Status = "Submitted";

            // Generate unique request number
            request.TravelRequestId = new Random().Next(100000, 999999);  // Or use a GUID or database sequence

            // Send email to manager
            var managerEmail = request.Employee.ManagerEmail;
            // Use a service to send the email here

            await _repository.UpdateRequestAsync(request);
            return Ok();
        }

        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(int requestId)
        {
            await _repository.DeleteRequestAsync(requestId);
            return NoContent();
        }
    }

}
