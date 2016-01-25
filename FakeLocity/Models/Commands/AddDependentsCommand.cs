namespace FakeLocity.Models.Commands
{
    using System;
    using System.Collections.Generic;
    using DAL;
    using Objects;

    public class AddDependentsCommand : ICommand
    {
        public Action<IEnumerable<Dependents>> Execute;

        private readonly IDapperHub dapperHub;
 
        public AddDependentsCommand(IDapperHub dapperHub)
        {
            this.dapperHub = dapperHub;
            Execute = ExecuteCommand;
        }

        public void ExecuteCommand(IEnumerable<Dependents> dependents)
        {
            if(dependents == null)
                return;

            foreach (var dependent in dependents)
            {
                dapperHub.Insert(dependent);   
            }
        }
    }
}