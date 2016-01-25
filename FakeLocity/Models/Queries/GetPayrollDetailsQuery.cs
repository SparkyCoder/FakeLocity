namespace FakeLocity.Models.Queries
{
    using System;
    using System.Collections.Generic;
    using Factories;
    using Helpers;
    using Objects;

    public class GetPayrollDetailsQuery : IQuery
    {
        public Func<int, string, PayrollDetails> Execute;

        private readonly IQueryFactory queryFactory;
        private readonly IPayrollDetailsHelper detailsHelper;

        public GetPayrollDetailsQuery(IQueryFactory queryFactory, IPayrollDetailsHelper detailsHelper)
        {
            this.queryFactory = queryFactory;
            this.detailsHelper = detailsHelper;
            Execute = ExecuteQuery;
        }

        private PayrollDetails ExecuteQuery(int employeeID, string name)
        {
            var employeePayroll = GetEmployeesPayroll(employeeID);

            var employeeDependents = GetEmployeesDependents(employeeID);

            return detailsHelper.CalculatePayrollDetails(employeePayroll, employeeDependents, name);
        }

        private IEnumerable<Dependents> GetEmployeesDependents(int employeeID)
        {
            var getEmployeeDependentsQuery = queryFactory.Create<GetDepedentsForEmployeeQuery>();

            return getEmployeeDependentsQuery.Execute(employeeID);
        }

        private Payroll GetEmployeesPayroll(int employeeID)
        {
            var getEmployeePayrollQuery = queryFactory.Create<GetPayrollForEmployeeQuery>();

            return getEmployeePayrollQuery.Execute(employeeID);
        }
    }
}