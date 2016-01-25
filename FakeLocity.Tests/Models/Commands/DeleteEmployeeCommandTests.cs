namespace FakeLocity.Tests.Models.Commands
{
    using FakeItEasy;
    using FakeLocity.Models.Commands;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Objects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class DeleteEmployeeCommandTests
    {
        private IDapperHub fakeDapperHub;

        [TestMethod]
        public void Execute_ShouldCallDapperBeginGetAllAndCommit()
        {
            var employeeCommand = GetDeleteEmployeeCommand();

            employeeCommand.Execute(A<Employee>._);

            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.Delete(A<Employee>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.CommitTransaction()).MustHaveHappened(Repeated.Exactly.Once);
        }

        private DeleteEmployeeCommand GetDeleteEmployeeCommand()
        {
            fakeDapperHub = A.Fake<IDapperHub>();

            return new DeleteEmployeeCommand(fakeDapperHub);
        }
    }
}
