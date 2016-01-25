namespace FakeLocity.Tests.Models.Queries
{
    using FakeItEasy;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Objects;
    using FakeLocity.Models.Queries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetDependentsForEmployeeQueryTests
    {
        private IDapperHub fakeDapperHub;

        [TestMethod]
        public void Execute_ShouldCallDapperBeginGetAllAndCommit()
        {
            var getDependentsForEmployeeQuery = GetDependentsForEmployeeQueryObject();

            getDependentsForEmployeeQuery.Execute(A<int>._);

            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.GetAll<Dependents>(A<object>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
        }

        private GetDepedentsForEmployeeQuery GetDependentsForEmployeeQueryObject()
        {
            fakeDapperHub = A.Fake<IDapperHub>();

            return new GetDepedentsForEmployeeQuery(fakeDapperHub);
        }
    }
}
