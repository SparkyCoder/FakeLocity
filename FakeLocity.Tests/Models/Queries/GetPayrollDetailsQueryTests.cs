namespace FakeLocity.Tests.Models.Queries
{
    using System.Collections.Generic;
    using FakeItEasy;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Factories;
    using FakeLocity.Models.Helpers;
    using FakeLocity.Models.Objects;
    using FakeLocity.Models.Queries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetPayrollDetailsQueryTests
    {
        private IQueryFactory fakeQueryFactory;
        private IPayrollDetailsHelper fakePayrollHelper;

        [TestMethod]
        public void Execute_ShouldCreateDependentsAndPayrollQueries()
        {
            var getPayrollQuery = GetPayrollDetailsQueryObject();

            A.CallTo(() => fakeQueryFactory.Create<GetDepedentsForEmployeeQuery>()).Returns(new GetDepedentsForEmployeeQuery(A.Fake<IDapperHub>()) {Execute = i => null});
            A.CallTo(() => fakeQueryFactory.Create<GetPayrollForEmployeeQuery>()).Returns(new GetPayrollForEmployeeQuery(A.Fake<IDapperHub>()) { Execute =i => null });

            getPayrollQuery.Execute(A<int>._, A<string>._);

            A.CallTo(() => fakeQueryFactory.Create<GetDepedentsForEmployeeQuery>()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeQueryFactory.Create<GetPayrollForEmployeeQuery>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void Execute_ShouldCallPayrollHelper()
        {
            var getPayrollQuery = GetPayrollDetailsQueryObject();

            A.CallTo(() => fakeQueryFactory.Create<GetDepedentsForEmployeeQuery>()).Returns(new GetDepedentsForEmployeeQuery(A.Fake<IDapperHub>()) { Execute = i => null });
            A.CallTo(() => fakeQueryFactory.Create<GetPayrollForEmployeeQuery>()).Returns(new GetPayrollForEmployeeQuery(A.Fake<IDapperHub>()) { Execute = i => null });

            getPayrollQuery.Execute(A<int>._, A<string>._);

            A.CallTo(() => fakePayrollHelper.CalculatePayrollDetails(A<Payroll>._, A<IEnumerable<Dependents>>._, A<string>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        private GetPayrollDetailsQuery GetPayrollDetailsQueryObject()
        {
            fakeQueryFactory = A.Fake<IQueryFactory>();
            fakePayrollHelper = A.Fake<IPayrollDetailsHelper>();

            return new GetPayrollDetailsQuery(fakeQueryFactory, fakePayrollHelper);
        }
    }
}
