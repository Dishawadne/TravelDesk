using iTextSharp.text.pdf;
using iTextSharp.text;
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
            if (existingTravelRequest.Status == TravelRequestStatus.Rejected ||
        existingTravelRequest.Status == TravelRequestStatus.ReturnedToEmployee)
            {
                existingTravelRequest.Status = TravelRequestStatus.Updated;
            }
            else
            { 
                existingTravelRequest.Status = travelRequestDto.Status;
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
            request.Comments = model.Comments;
            await _travelRequestRepository.UpdateRequestAsync(request);
            return NoContent();
        }
        //[HttpGet("DownloadTicket/{travelRequestId}")]
        //public IActionResult DownloadTicket(int travelRequestId)
        //{
        //    try
        //    {
        //        var travelRequest = _context.TravelRequests
        //            .Include(tr => tr.User)
        //            .FirstOrDefault(tr => tr.RequestId == travelRequestId);

        //        if (travelRequest == null)
        //        {
        //            return NotFound("Travel request not found.");
        //        }

        //        using (var stream = new MemoryStream())
        //        {
        //            var pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
        //            PdfWriter.GetInstance(pdfDoc, stream);
        //            pdfDoc.Open();

        //            var titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD);
        //            var bodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);

        //            pdfDoc.Add(new Paragraph("Travel Desk", titleFont));
        //            pdfDoc.Add(new Paragraph($"Request ID: {travelRequest.RequestId}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"User ID: {travelRequest.UserId}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"User Name: {travelRequest.User.FirstName} {travelRequest.User.LastName}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"Project Name: {travelRequest.ProjectName}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"Reason for Travelling: {travelRequest.ReasonForTravelling}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"From Date: {travelRequest.FromDate.ToShortDateString()}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"To Date: {travelRequest.ToDate.ToShortDateString()}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"From Location: {travelRequest.FromLocation}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"To Location: {travelRequest.ToLocation}", bodyFont));
        //            pdfDoc.Add(new Paragraph($"Comments: {travelRequest.Comments}", bodyFont));


        //            pdfDoc.Close();

        //            var pdfBytes = stream.ToArray();
        //            return File(pdfBytes, "application/pdf", "TravelRequestTicket.pdf");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpGet("DownloadTicket/{travelRequestId}")]
        public IActionResult DownloadTicket(int travelRequestId)
        {
            try
            {
                var travelRequest = _context.TravelRequests
                    .Include(tr => tr.User)
                    .FirstOrDefault(tr => tr.RequestId == travelRequestId);

                if (travelRequest == null)
                {
                    return NotFound("Travel request not found.");
                }

                using (var stream = new MemoryStream())
                {
                    var pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Set Background Color
                    PdfContentByte canvas = writer.DirectContentUnder;
                    canvas.SetColorFill(new BaseColor(240, 240, 240)); 
                    canvas.Rectangle(0, 0, pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                    canvas.Fill();

                    // Define Fonts
                    var titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 22, iTextSharp.text.Font.BOLD, BaseColor.RED); // Dark blue for Travel Desk
                    var headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                    var sectionFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    var bodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
                    var tableFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);
                    var footerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC, BaseColor.GRAY);

                    // Header Section with Travel Desk name and spacing
                    pdfDoc.Add(new Paragraph("Travel Desk ", titleFont));
                    pdfDoc.Add(new Paragraph("\n")); 

                    // Ticket Information Section (ID, Booking Date)
                    var ticketInfoTable = new PdfPTable(2);
                    ticketInfoTable.WidthPercentage = 100;
                    ticketInfoTable.SetWidths(new float[] { 3f, 1f });
                    ticketInfoTable.AddCell(GetStyledCell("Ticket ID: " + travelRequest.RequestId, sectionFont, true));
                    ticketInfoTable.AddCell(GetStyledCell("Booking Date: " + DateTime.Now.ToShortDateString(), sectionFont, true));
                    pdfDoc.Add(ticketInfoTable);
                    pdfDoc.Add(new Paragraph("\n"));  

                    // Passenger Information
                    pdfDoc.Add(new Paragraph("Passenger Information:", sectionFont));
                    pdfDoc.Add(new Paragraph("\n")); 
                    var passengerInfoTable = new PdfPTable(2);
                    passengerInfoTable.WidthPercentage = 100;
                    passengerInfoTable.SetWidths(new float[] { 1f, 2f });
                    passengerInfoTable.AddCell(GetStyledCell("Name:", tableFont, true));
                    passengerInfoTable.AddCell(GetStyledCell($"{travelRequest.User.FirstName} {travelRequest.User.LastName}", tableFont));
                    passengerInfoTable.AddCell(GetStyledCell("Email:", tableFont, true));
                    passengerInfoTable.AddCell(GetStyledCell(travelRequest.User.Email, tableFont));
                    passengerInfoTable.AddCell(GetStyledCell("Phone Number:", tableFont, true));
                    passengerInfoTable.AddCell(GetStyledCell(travelRequest.User.MobileNum, tableFont));
                    pdfDoc.Add(passengerInfoTable);
                    pdfDoc.Add(new Paragraph("\n")); // Adds space before next section

                    // Travel Details Table
                    pdfDoc.Add(new Paragraph("Travel Information:", sectionFont));
                    pdfDoc.Add(new Paragraph("\n")); // Add space
                    var travelInfoTable = new PdfPTable(2);
                    travelInfoTable.WidthPercentage = 100;
                    travelInfoTable.SetWidths(new float[] { 1f, 2f });
                    travelInfoTable.AddCell(GetStyledCell("From Date:", tableFont, true));
                    travelInfoTable.AddCell(GetStyledCell(travelRequest.FromDate.ToShortDateString(), tableFont));
                    travelInfoTable.AddCell(GetStyledCell("To Date:", tableFont, true));
                    travelInfoTable.AddCell(GetStyledCell(travelRequest.ToDate.ToShortDateString(), tableFont));
                    travelInfoTable.AddCell(GetStyledCell("From Location:", tableFont, true));
                    travelInfoTable.AddCell(GetStyledCell(travelRequest.FromLocation, tableFont));
                    travelInfoTable.AddCell(GetStyledCell("To Location:", tableFont, true));
                    travelInfoTable.AddCell(GetStyledCell(travelRequest.ToLocation, tableFont));
                    pdfDoc.Add(travelInfoTable);
                    pdfDoc.Add(new Paragraph("\n")); 

                   
                    pdfDoc.Add(new Paragraph("Booking Details:", sectionFont));
                    pdfDoc.Add(new Paragraph("\n")); 
                    var bookingInfoTable = new PdfPTable(2);
                    bookingInfoTable.WidthPercentage = 100;
                    bookingInfoTable.SetWidths(new float[] { 1f, 2f });
                    bookingInfoTable.AddCell(GetStyledCell("Reason for Travel:", tableFont, true));
                    bookingInfoTable.AddCell(GetStyledCell(travelRequest.ReasonForTravelling, tableFont));
                    bookingInfoTable.AddCell(GetStyledCell("Project Name:", tableFont, true));
                    bookingInfoTable.AddCell(GetStyledCell(travelRequest.ProjectName, tableFont));
                    bookingInfoTable.AddCell(GetStyledCell("Comments:", tableFont, true));
                    bookingInfoTable.AddCell(GetStyledCell(travelRequest.Comments, tableFont));
                    pdfDoc.Add(bookingInfoTable);
                    pdfDoc.Add(new Paragraph("\n")); 
                    pdfDoc.Add(new Paragraph("TravelDesk Support: +1 234 567 890 | Email: support@traveldesk.com", footerFont));
                    pdfDoc.Add(new Paragraph("Thank you for choosing TravelDesk! We hope you have a safe and pleasant journey.", footerFont));
                  //  pdfDoc.Add(new Paragraph("QR Code Placeholder", footerFont)); // Placeholder for QR code

                    pdfDoc.Close();

                    var pdfBytes = stream.ToArray();
                    return File(pdfBytes, "application/pdf", "TravelRequestTicket.pdf");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

      
        private PdfPCell GetStyledCell(string text, Font font, bool isHeader = false)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));

            if (isHeader)
            {
                cell.BackgroundColor = BaseColor.LIGHT_GRAY; 
            }

            cell.BorderWidth = 1f;
            cell.Padding = 5f;
            return cell;
        }


    }
}
