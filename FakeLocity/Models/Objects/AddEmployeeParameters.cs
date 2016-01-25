namespace FakeLocity.Models.Objects
{
    using System.Collections.Generic;

    public class AddEmployeeParameters
    {
        public Employee NewEmployee { get; set; }
        public IEnumerable<Dependents> Dependents { get; set; } 
    }
}