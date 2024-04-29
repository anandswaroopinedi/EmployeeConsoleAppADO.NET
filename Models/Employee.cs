using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public int JobTitleId { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public string ManagerId { get; set; }
        public int ProjectId { get; set; }
        public static string[] Headers = { "Exit", "FirstName", "LastName", "Date Of Birth", "Email", "MobileNumber", "JoinDate", "JobTitle", "Location", "Department", "ManagerId", "Project" };

    }
}
