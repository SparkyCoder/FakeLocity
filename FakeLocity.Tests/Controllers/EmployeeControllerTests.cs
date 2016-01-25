namespace FakeLocity.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Dynamic;
    using Exceptions;
    using FakeItEasy;
    using FakeLocity.Controllers;
    using FakeLocity.Models.Commands;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Factories;
    using FakeLocity.Models.Helpers;
    using FakeLocity.Models.Objects;
    using FakeLocity.Models.Queries;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmployeeControllerTests
    {
        private ICommandFactory fakeCommandFactory;
        private IQueryFactory fakeQueryFactory;
        private IPayrollDetailsHelper fakePayrolleHelper;

        [TestMethod]
        public void AddEmployee_ShouldCreateAddEmployeeCommand()
        {
            var employeeController = GetEmployeeController();

            A.CallTo(() => fakeCommandFactory.Create<AddEmployeeCommand>()).Returns(new AddEmployeeCommand(A.Fake<IDapperHub>(), A.Fake<ICommandFactory>()) { Execute = (employee, dependents) => {}});

            employeeController.AddEmployee(new Employee(), new List<Dependents>());

            A.CallTo(() => fakeCommandFactory.Create<AddEmployeeCommand>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DeleteEmployee_ShouldCreateDeleteEmployeeCommand()
        {
            var employeeController = GetEmployeeController();

            A.CallTo(() => fakeCommandFactory.Create<DeleteEmployeeCommand>()).Returns(new DeleteEmployeeCommand(A.Fake<IDapperHub>()) { Execute = employee => { } });

            employeeController.DeleteEmployee(new Employee());

            A.CallTo(() => fakeCommandFactory.Create<DeleteEmployeeCommand>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void GetAllEmployees_ShouldCreateGetAllEmployeesQuery()
        {
            var employeeController = GetEmployeeController();

            A.CallTo(() => fakeQueryFactory.Create<GetAllEmployeesQuery>()).Returns(new GetAllEmployeesQuery(A.Fake<IDapperHub>()) { Execute = () => null});

            employeeController.GetAllEmployees();

            A.CallTo(() => fakeQueryFactory.Create<GetAllEmployeesQuery>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void GetPayrollDetails_ShouldCreateGetPayrollDetailsQuery()
        {
            var employeeController = GetEmployeeController();

            A.CallTo(() => fakeQueryFactory.Create<GetPayrollDetailsQuery>()).Returns(new GetPayrollDetailsQuery(fakeQueryFactory, fakePayrolleHelper) {Execute = (i,n) => null});

            employeeController.GetPayrollDetails(1, "zion");

            A.CallTo(() => fakeQueryFactory.Create<GetPayrollDetailsQuery>()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void AddEmployee_ShouldThrowExceptionForNullParameter()
        {
            var employeeController = GetEmployeeController();

            employeeController.AddEmployee(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void DeleteEmployee_ShouldThrowExceptionForNullParameter()
        {
            var employeeController = GetEmployeeController();

            employeeController.DeleteEmployee(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void GetPayrollDetails_ShouldThrowExceptionForNullParameter()
        {
            var employeeController = GetEmployeeController();

            employeeController.GetPayrollDetails(new int(), "");
        }

        private EmployeeController GetEmployeeController()
        {
            fakeCommandFactory = A.Fake<ICommandFactory>();
            fakeQueryFactory = A.Fake<IQueryFactory>();
            fakePayrolleHelper = A.Fake<IPayrollDetailsHelper>();

            return new EmployeeController(fakeCommandFactory, fakeQueryFactory);
        }
    }
}
