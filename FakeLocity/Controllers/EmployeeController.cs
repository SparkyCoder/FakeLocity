namespace FakeLocity.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Exceptions;
    using Models.Commands;
    using Models.Factories;
    using Models.Objects;
    using Models.Queries;

    [RoutePrefix("api")]
    public class EmployeeController : ApiController
    {
        private readonly ICommandFactory commandFactory;
        private readonly IQueryFactory queryFactory;

        public EmployeeController(ICommandFactory commandFactory, IQueryFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.queryFactory = queryFactory;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public void AddEmployee(AddEmployeeParameters parameters)
        {
            if(parameters.NewEmployee == null)
                throw new InvalidParameterException();

            var addEmployeeCommand = commandFactory.Create<AddEmployeeCommand>();

            addEmployeeCommand.Execute(parameters.NewEmployee, parameters.Dependents);
        }

        [HttpPost]
        [Route("DeleteEmployee")]
        public void DeleteEmployee(Employee employeeToDelete)
        {
            if (employeeToDelete == null)
                throw new InvalidParameterException();

            var deleteEmployeeCommand = commandFactory.Create<DeleteEmployeeCommand>();

            deleteEmployeeCommand.Execute(employeeToDelete);
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            var getAllEmployeesQuery = queryFactory.Create<GetAllEmployeesQuery>();

            return getAllEmployeesQuery.Execute();
        }

        [HttpGet]
        [HttpPost]
        [Route("GetPayrollDetails")]
        public PayrollDetails GetPayrollDetails(GetPayrollDetailsParameters parameters)
        {
            if (parameters.EmployeeID == 0)
                throw new InvalidParameterException();

            var getPayrollDetailsQuery = queryFactory.Create<GetPayrollDetailsQuery>();

            return getPayrollDetailsQuery.Execute(parameters.EmployeeID, parameters.Name);
        }
    }
}
