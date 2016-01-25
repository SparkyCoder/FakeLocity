namespace FakeLocity.Tests.Models.Commands
{
    using System.Collections.Generic;
    using FakeItEasy;
    using FakeLocity.Models.Commands;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Factories;
    using FakeLocity.Models.Objects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddEmployeeCommandTests
    {
        private IDapperHub fakeDapperHub;
        private ICommandFactory fakeCommandFactory;

        [TestMethod]
        public void Execute_ShouldCallDapperBeginGetAllAndCommit()
        {
            var employeeCommand = GetAddEmployeeCommand();

            A.CallTo(() => fakeCommandFactory.Create<AddDependentsCommand>()).Returns(new AddDependentsCommand(A.Fake<IDapperHub>()) { Execute = employee => { } });

            employeeCommand.Execute(A<Employee>._, A<IEnumerable<Dependents>>._);

            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.Insert(A<Employee>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.CommitTransaction()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void Execute_ShouldCallCreateAddDependentsCommand()
        {
            var employeeCommand = GetAddEmployeeCommand();

            A.CallTo(() => fakeCommandFactory.Create<AddDependentsCommand>()).Returns(new AddDependentsCommand(A.Fake<IDapperHub>()) { Execute = employee => { } });

            employeeCommand.Execute(A<Employee>._, A<IEnumerable<Dependents>>._);

            A.CallTo(() => fakeCommandFactory.Create<AddDependentsCommand>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        private AddEmployeeCommand GetAddEmployeeCommand()
        {
            fakeDapperHub = A.Fake<IDapperHub>();
            fakeCommandFactory = A.Fake<ICommandFactory>();

            return new AddEmployeeCommand(fakeDapperHub, fakeCommandFactory);
        }
    }
}
