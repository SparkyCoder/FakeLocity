namespace FakeLocity.Models.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Objects;

    public class PayrollDetailsHelper : IPayrollDetailsHelper
    {
        private readonly int benefitsDeduction;
        private readonly int dependentDeductions;
        private readonly int savingsPercentage;
        private const char LetterForSavings = 'A';

        public PayrollDetailsHelper(int benefitsDeduction, int dependentDeductions, int savingsPercentage)
        {
            this.benefitsDeduction = benefitsDeduction;
            this.dependentDeductions = dependentDeductions;
            this.savingsPercentage = savingsPercentage;
        }

        public PayrollDetails CalculatePayrollDetails(Payroll employeePayroll, IEnumerable<Dependents> employeeDependents, string name)
        {
            var grossAnnualPay = CalculateAnnualGrossPay(employeePayroll);

            var dependentsDeductions = CalculateAnnualDependentDeductions(employeeDependents.Count());

            var annualDeductions = CalculateAnnualDeductions(dependentsDeductions);

            var annualSavings = CalculateAnnualSavings(annualDeductions, employeeDependents, name);

            return CreatePayrollDetail(grossAnnualPay, dependentsDeductions, annualSavings);
        }

        private decimal CalculateAnnualGrossPay(Payroll employeePayroll)
        {
            if (employeePayroll == null)
                return 0;

            return employeePayroll.GrossMonthlySalary*employeePayroll.NumberOfChecks;
        }

        private decimal CalculateAnnualDependentDeductions(int numberOfEmployeeDependents)
        {
            return numberOfEmployeeDependents*dependentDeductions;
        }

        private decimal CalculateAnnualDeductions(decimal dependentDeductions)
        {
            return dependentDeductions + benefitsDeduction;
        }

        private decimal CalculateAnnualSavings(decimal totalAnnualDeductions, IEnumerable<Dependents> employeeDependents, string name)
        {
            var dependentsWithLetterSavings = employeeDependents.Where(dependent => dependent.Name.ToUpper()[0] == LetterForSavings);

            return (dependentsWithLetterSavings.Any() || name.ToUpper()[0] == LetterForSavings) ? (totalAnnualDeductions * savingsPercentage) / 100 : 0;
        }

        private PayrollDetails CreatePayrollDetail(decimal grossPay, decimal dependentsDeductions, decimal savings)
        {
            return new PayrollDetails()
            {
                BenefitDeductions = benefitsDeduction,
                DependentDeductions = dependentsDeductions,
                GrossYearlyPay = grossPay,
                Savings = savings
            };
        }
    }
}