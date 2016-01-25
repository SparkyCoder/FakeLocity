namespace FakeLocity.Models.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DAL;
    using DapperExtensions;
    using Exceptions;
    using Objects;

    public class GetPayrollForEmployeeQuery : IQuery
    {
        public Func<int, Payroll> Execute;

        private readonly IDapperHub dapperHub;

        public GetPayrollForEmployeeQuery(IDapperHub dapperHub)
        {
            this.dapperHub = dapperHub;
            Execute = ExecuteCommand;
        }

        private Payroll ExecuteCommand(int employeeID)
        {
            var employeesPayroll = GetEmployeesPayroll(employeeID);

            if(employeesPayroll.Count() > 1)
                throw new UnexpectedPayrollResultsException();

            return employeesPayroll.FirstOrDefault();
        }

        private IEnumerable<Payroll> GetEmployeesPayroll(int employeeID)
        {
            dapperHub.BeginTransaction();

            var predicate = Predicates.Field<Payroll>(p => p.EmployeeID, Operator.Eq, employeeID);

            var employeesPayroll = dapperHub.GetAll<Payroll>(predicate);

            dapperHub.CommitTransaction();

            return employeesPayroll;
        } 
    }
}