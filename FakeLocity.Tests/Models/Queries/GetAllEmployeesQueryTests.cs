namespace FakeLocity.Tests.Models.Queries
{
    using FakeItEasy;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Objects;
    using FakeLocity.Models.Queries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetAllEmployeesQueryTests
    {
        private IDapperHub fakeDapperHub;

        [TestMethod]
        public void Execute_ShouldCallDapperBeginGetAllAndCommit()
        {
            var getAllEmployeesQuery = GetAllEmployeesQueryObject();

            getAllEmployeesQuery.Execute();

            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.GetAll<Employee>(null)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
        }

        private GetAllEmployeesQuery GetAllEmployeesQueryObject()
        {
            fakeDapperHub = A.Fake<IDapperHub>();

            return new GetAllEmployeesQuery(fakeDapperHub);
        }
    }
}
