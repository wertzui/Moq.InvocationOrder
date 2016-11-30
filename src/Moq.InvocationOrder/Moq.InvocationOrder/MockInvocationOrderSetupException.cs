using System;

namespace Moq.InvocationOrder
{
    class MockInvocationOrderSetupException : Exception
    {
        public MockInvocationOrderSetupException(string mesage)
            : base(mesage)
        {
        }
    }
}