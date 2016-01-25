using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeLocity.Tests.Models.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using FakeLocity.Models.Commands;
    using FakeLocity.Models.DAL;
    using FakeLocity.Models.Objects;

    [TestClass]
    public class AddDependentsCommandTests
    {
        private IDapperHub fakeDapperHub;

        [TestMethod]
        public void Execute_ShouldInsertDependents()
        {
            var addDependentsComand = GetAddDependentsCommand();

            var testDependents = GetTestDependents();

            addDependentsComand.Execute(testDependents);

            A.CallTo(() => fakeDapperHub.Insert(A<Dependents>._)).MustHaveHappened(Repeated.Exactly.Times(testDependents.Count()));
        }

        private IEnumerable<Dependents> GetTestDependents()
        {

            return new List<Dependents>()
            {
                new Dependents(),
                new Dependents(),
                new Dependents(),
                new Dependents(),
                new Dependents()
            };
        } 

        private AddDependentsCommand GetAddDependentsCommand()
        {
            fakeDapperHub = A.Fake<IDapperHub>();

            return new AddDependentsCommand(fakeDapperHub);
        }
    }
}
