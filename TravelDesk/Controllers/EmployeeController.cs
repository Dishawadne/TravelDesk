using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TravelDesk.DTO;
using TeavelDesk.Models;
using TravelDesk.Models;
using TravelDesk.Context;

namespace TravelDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DbContexts _context;

        public EmployeeController(DbContexts context)
        {
            _context = context;
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadEmployeeInformation([FromForm] EmployeeUploadDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Invalid Employee Information");
            }

            // Create the employee entity and map basic information
            var employee = new Employee
            {
                EmployeeName = employeeDto.EmployeeName,
                ProjectName = employeeDto.ProjectName,
                DepartmentName = employeeDto.DepartmentName,
                ReasonForTraveling = employeeDto.ReasonForTraveling,
                AirBookings = new List<AirBooking>(),
                HotelBookings = new List<HotelBooking>()
            };

            // Define the folder path for storing files
            var folderName = Path.Combine("Resources", "EmployeeFiles", employee.EmployeeName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            // Handle AirBooking
            if (employeeDto.AirBooking != null)
            {
                var airBookingDto = employeeDto.AirBooking;
                var airBooking = new AirBooking
                {
                    BookingDate = airBookingDto.BookingDate,
                    FlightType = airBookingDto.FlightType,
                    AadharCardNumber = airBookingDto.AadharCardNumber,
                    PassportNumber = airBookingDto.PassportNumber
                };

                // Handle Passport File upload
                // Handle Passport File upload
                if (airBookingDto.PassportFile != null)
                {
                    var passportFileName = Path.GetFileName(airBookingDto.PassportFile.FileName);
                    var passportFullPath = Path.Combine(pathToSave, "Passport_" + passportFileName);

                    using (var stream = new FileStream(passportFullPath, FileMode.Create))
                    {
                        await airBookingDto.PassportFile.CopyToAsync(stream);
                    }

                    airBooking.PassportFileName = passportFileName; // Save file name
                }

                // Handle Visa File upload
                if (airBookingDto.VisaFile != null)
                {
                    var visaFileName = Path.GetFileName(airBookingDto.VisaFile.FileName);
                    var visaFullPath = Path.Combine(pathToSave, "Visa_" + visaFileName);

                    using (var stream = new FileStream(visaFullPath, FileMode.Create))
                    {
                        await airBookingDto.VisaFile.CopyToAsync(stream);
                    }

                    airBooking.VisaFileName = visaFileName; // Save file name
                }


                employee.AirBookings.Add(airBooking);
            }

            // Handle HotelBooking
            if (employeeDto.HotelBooking != null)
            {
                var hotelBookingDto = employeeDto.HotelBooking;
                var hotelBooking = new HotelBooking
                {
                    BookingDate = hotelBookingDto.BookingDate,
                    DaysOfStay = hotelBookingDto.DaysOfStay,
                    mealRequired = hotelBookingDto.MealRequired,
                    mealPreference = hotelBookingDto.MealPreference
                };

                employee.HotelBookings.Add(hotelBooking);
            }

            // Save the employee entity to the database
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Employee information and files uploaded successfully." });
        }
        // Additional actions to get employee details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.AirBookings)
                .Include(e => e.HotelBookings)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            return Ok(employee);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            List<Employee> empList = await _context.Employees
                                    .Include(e => e.AirBookings)
                                    .Include(e => e.HotelBookings)
                                    .ToListAsync();
            return Ok(empList);
        }
    }
    }
