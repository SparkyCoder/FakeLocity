namespace FakeLocity.Models.Queries
{
    using System;
    using System.Collections.Generic;
    using DAL;
    using DapperExtensions;
    using Objects;

    public class GetDepedentsForEmployeeQuery : IQuery
    {
        public Func<int, IEnumerable<Dependents>> Execute;

        private readonly IDapperHub dapperHub;

        public GetDepedentsForEmployeeQuery(IDapperHub dapperHub)
        {
            this.dapperHub = dapperHub;
            Execute = ExecuteQuery;
        }

        private IEnumerable<Dependents> ExecuteQuery(int employeeID)
        {
            dapperHub.BeginTransaction();

            var predicate = Predicates.Field<Dependents>(d => d.EmployeeID, Operator.Eq, employeeID);

            var employeeDependents = dapperHub.GetAll<Dependents>(predicate);

            dapperHub.CommitTransaction();

            return employeeDependents;
        }
    }
}