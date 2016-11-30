using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moq.InvocationOrder
{
    public class MockInvocationOrderException : Exception
    {
        public MockInvocationOrderException(string message)
            : base(message)
        {
        }
    }
}
