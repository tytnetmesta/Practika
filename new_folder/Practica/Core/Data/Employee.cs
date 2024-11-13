namespace EmployeeManagementApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string WorkMode { get; set; }
    }
}