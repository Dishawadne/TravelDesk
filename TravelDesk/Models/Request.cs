using System.ComponentModel.DataAnnotations.Schema;

namespace TravelDesk.Models
{
    public class Request
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int Userld { get; set; }
        public Project? Project { get; set; }
        public int Projectid { get; set; }
        public string ReasonForTraveling { get; set; }
        public virtual User? Manager { get; set; }
        public int Managerld { get; set; } //public Document? Documents {get; set; } 
        //public int? Documentsld { get; set; }
        public string AadharNumber { get; set; } 

        [NotMapped] 
        public IFormFile? Aadhar { get; set; } 
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
        public string? ModifiedBy { get; set;} public DateTime? ModifiedDate { get; set; } 
        public Boolean IsActive { get; set; } = true;}
    }
