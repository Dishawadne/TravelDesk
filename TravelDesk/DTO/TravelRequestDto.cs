﻿using TravelDesk.Models;

namespace TravelDesk.DTO
{
    public class TravelRequestDto
    {
        public int UserId { get; set; }
        public string ReasonForTravelling { get; set; }
        public int ManagerId { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TravelRequestStatus Status { get; set; } = TravelRequestStatus.Pending;

        public IFormFile? AddharFile { get; set; }
        public string? Comments { get; set; }
    }
}
