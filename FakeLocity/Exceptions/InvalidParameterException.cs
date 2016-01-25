namespace FakeLocity.Exceptions
{
    using System;

    public class InvalidParameterException : Exception
    {
        public override string Message
        {
            get { return "Parameter Was Empty"; }
        }
    }
}