namespace FakeLocity.Models.Commands
{
    using System;
    using DAL;
    using Objects;

    public class DeleteEmployeeCommand : ICommand
    {
        public Action<Employee> Execute; 

        private readonly IDapperHub dapperHub;

        public DeleteEmployeeCommand(IDapperHub dapperHub)
        {
            this.dapperHub = dapperHub;
            Execute = ExecuteCommand;
        }

        public void ExecuteCommand(Employee employeeToDelete)
        {
            dapperHub.BeginTransaction();

            dapperHub.Delete(employeeToDelete);

            dapperHub.CommitTransaction();
        }
    }
}