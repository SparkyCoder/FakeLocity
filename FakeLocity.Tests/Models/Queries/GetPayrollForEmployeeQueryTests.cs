using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeLocity.Tests.Models.Queries
{
    using System.Collections.Generic;
    using Exceptions;
    using FakeItEasy;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Objects;
    using FakeLocity.Models.Queries;

    [TestClass]
    public class GetPayrollForEmployeeQueryTests
    {
        private IDapperHub fakeDapperHub;

        [TestMethod]
        public void Execute_ShouldCallDapperBeginGetAllAndCommit()
        {
            var getPayrollForEmployeeQuery = GetPayrollForEmployeeQueryObject();

            A.CallTo(() => fakeDapperHub.GetAll<Payroll>(A<object>._))
                .Returns(new List<Payroll>()
                {
                    new Payroll()
                });
        
        getPayrollForEmployeeQuery.Execute(A<int>._);

            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.GetAll<Payroll>(A<object>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeDapperHub.BeginTransaction()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedPayrollResultsException))]
        public void Execute_ShouldThrowExceptionWhenMoreThanOnePayrollIsReturned()
        {
            var getPayrollForEmployeeQuery = GetPayrollForEmployeeQueryObject();

            A.CallTo(() => fakeDapperHub.GetAll<Payroll>(A<object>._))
                .Returns(new List<Payroll>()
                {
                    new Payroll(),
                    new Payroll()
                });

            getPayrollForEmployeeQuery.Execute(A<int>._);
        }

        private GetPayrollForEmployeeQuery GetPayrollForEmployeeQueryObject()
        {
            fakeDapperHub = A.Fake<IDapperHub>();

            return new GetPayrollForEmployeeQuery(fakeDapperHub);
        }
    }
}
