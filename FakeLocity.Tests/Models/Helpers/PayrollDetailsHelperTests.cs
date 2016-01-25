namespace FakeLocity.Tests.Models.Helpers
{
    using System.Collections.Generic;
    using FakeLocity.Models.Helpers;
    using FakeLocity.Models.Objects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PayrollDetailsHelperTests
    {
        private const string EmployeeName = "Test Employee";

        [TestMethod]
        public void CalculatePayrollDetails_ShouldCorrectlyCalculateSavings()
        {
            var payrollHelper = GetPayrollDetailsHelper();

            var testPayroll = GetTestPayroll();

            var payrolDetails = payrollHelper.CalculatePayrollDetails(testPayroll, GetTestDependentsWithSavings(), EmployeeName);

            Assert.IsTrue(payrolDetails.Savings == (payrolDetails.BenefitDeductions + payrolDetails.DependentDeductions)*10/100);
        }

        [TestMethod]
        public void CalculatePayrollDetails_ShouldCorrectlyCalculateNoSavings()
        {
            var payrollHelper = GetPayrollDetailsHelper();

            var testPayroll = GetTestPayroll();

            var payrolDetails = payrollHelper.CalculatePayrollDetails(testPayroll, GetTestDependentsWithoutSavings(), EmployeeName);

            Assert.IsTrue(payrolDetails.Savings == 0);
        }

        [TestMethod]
        public void CalculatePayrollDetails_ShouldCorrectlyCalculateGrossPay()
        {
            var payrollHelper = GetPayrollDetailsHelper();

            var testPayroll = GetTestPayroll();

            var payrolDetails = payrollHelper.CalculatePayrollDetails(testPayroll, GetTestDependentsWithSavings(), EmployeeName);

            Assert.IsTrue(payrolDetails.GrossYearlyPay == (testPayroll.GrossMonthlySalary * testPayroll.NumberOfChecks));
        }

        [TestMethod]
        public void CalculatePayrollDetails_ShouldCorrectlyCalculateDependentDeductions()
        {
            var payrollHelper = GetPayrollDetailsHelper();

            var payrolDetails = payrollHelper.CalculatePayrollDetails(GetTestPayroll(), GetTestDependentsWithSavings(), EmployeeName);

            Assert.IsTrue(payrolDetails.DependentDeductions == (500 * 3));
        }

        [TestMethod]
        public void CalculatePayrollDetails_ShouldCorrectlyBenefitDeductions()
        {
            var payrollHelper = GetPayrollDetailsHelper();

            var testPayroll = GetTestPayroll();

            var payrolDetails = payrollHelper.CalculatePayrollDetails(testPayroll, GetTestDependentsWithSavings(), EmployeeName);

            Assert.IsTrue(payrolDetails.BenefitDeductions == 1000);
        }

        private IEnumerable<Dependents> GetTestDependentsWithSavings()
        {
            return new List<Dependents>()
            {
                new Dependents(){Name = "Anakin Skywalker"},
                 new Dependents(){Name = "Luke Skywalker"},
                  new Dependents(){Name = "R2-D2"}
            };
        }

        private IEnumerable<Dependents> GetTestDependentsWithoutSavings()
        {
            return new List<Dependents>()
            {
                new Dependents(){Name = "Darth Vador"}
            };
        } 

        private Payroll GetTestPayroll()
        {
            return new Payroll
            {
                EmployeeID = 1,
                GrossMonthlySalary = 2000,
                NumberOfChecks = 26
            };
        }

        private PayrollDetailsHelper GetPayrollDetailsHelper()
        {
            return new PayrollDetailsHelper(1000, 500, 10);
        }
    }
}
