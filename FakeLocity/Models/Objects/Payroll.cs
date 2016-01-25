namespace FakeLocity.Models.Objects
{
    public class Payroll
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public decimal GrossMonthlySalary { get; set; }
        public int NumberOfChecks { get; set; }
    }
}