namespace FakeLocity.Interceptors
{
    using System;
    using Castle.Core.Logging;
    using Castle.DynamicProxy;

    public class ExceptionInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        private const string GenericErrorMessage = "Error: {0} Stacktrace: {1}";

        public ExceptionInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception unknownException)
            {
                logger.Error(string.Format(GenericErrorMessage, unknownException.Message, unknownException.StackTrace));
                throw;
            }
        }
    }
}