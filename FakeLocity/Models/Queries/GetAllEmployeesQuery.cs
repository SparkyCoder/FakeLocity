namespace FakeLocity.Models.Queries
{
    using System;
    using System.Collections.Generic;
    using DAL;
    using Objects;

    public class GetAllEmployeesQuery : IQuery
    {
        public Func<IEnumerable<Employee>>  Execute;

        private readonly IDapperHub dapperHub;

        public GetAllEmployeesQuery(IDapperHub dapperHub)
        {
            this.dapperHub = dapperHub;
            Execute = ExecuteQuery;
        }

        private IEnumerable<Employee> ExecuteQuery()
        {
            dapperHub.BeginTransaction();

            var collectionOfEmployees = dapperHub.GetAll<Employee>(null);

            dapperHub.CommitTransaction();

            return collectionOfEmployees;
        } 
    }
}