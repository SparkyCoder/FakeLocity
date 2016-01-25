namespace FakeLocity.Models.Helpers
{
    using System.Collections.Generic;
    using Objects;

    public interface IPayrollDetailsHelper
    {
        PayrollDetails CalculatePayrollDetails(Payroll employeePayroll, IEnumerable<Dependents> employeeDependents, string name);
    }
}