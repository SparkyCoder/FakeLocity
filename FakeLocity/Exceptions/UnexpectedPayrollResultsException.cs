namespace FakeLocity.Exceptions
{
    using System;

    public class UnexpectedPayrollResultsException : Exception
    {
        public override string Message
        {
            get { return "Expected One Payroll For Employee, But Found More."; }
        }
    }
}