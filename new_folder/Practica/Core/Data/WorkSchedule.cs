namespace EmployeeManagementApp
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double HoursWorked { get; set; }
        public string Shift { get; set; }
        public object ArrivalTime { get; set; }
        public object DepartureTime { get; set; }
        public object DaysOff { get; set; }
    }
}