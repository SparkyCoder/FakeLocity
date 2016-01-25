namespace FakeLocity.Models.Commands
{
    using System;
    using System.Collections.Generic;
    using DAL;
    using Factories;
    using Objects;

    public class AddEmployeeCommand : ICommand
    {
        public Action<Employee, IEnumerable<Dependents>> Execute;

        private readonly IDapperHub dapperHub;
        private readonly ICommandFactory commandFactory;

        public AddEmployeeCommand(IDapperHub dapperHub, ICommandFactory commandFactory)
        {
            this.dapperHub = dapperHub;
            this.commandFactory = commandFactory;
            Execute = ExecuteQuery;
        }

        public void ExecuteQuery(Employee newEmployee, IEnumerable<Dependents> dependents)
        {
            dapperHub.BeginTransaction();

            var employeeID = dapperHub.Insert(newEmployee);

            AddDependents(dependents, employeeID);

            dapperHub.CommitTransaction();
        }

        private void AddDependents(IEnumerable<Dependents> dependents, int employeeID)
        {
            if (dependents != null)
            {
                foreach (var dependent in dependents)
                {
                    dependent.EmployeeID = employeeID;
                }
            }

            var addDependentCommand = commandFactory.Create<AddDependentsCommand>();

            addDependentCommand.Execute(dependents);
        }
    }
}