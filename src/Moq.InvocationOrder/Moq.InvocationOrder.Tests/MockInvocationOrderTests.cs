using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moq.InvocationOrder.Tests
{
    [TestClass]
    public class MockInvocationOrderTests
    {
        [TestMethod]
        public void LogInvocation_logs_to_correct_logger()
        {
            // Arrange
            var order = new MockInvocationOrder();
            var mock1 = new Mock<object>();
            var mock2 = new Mock<object>();
            Expression<Func<object, string>> expression1 = o => o.ToString();
            Expression<Func<object, int>> expression2 = o => o.GetHashCode();
            order.Setup(mock1, expression1);
            order.Setup(mock2, expression2);

            // Act
            order.LogInvocation(mock1, expression1);
            order.LogInvocation(mock2, expression2);

            // Assert
            var logger1 = order.GetLogger(mock1, expression1);
            var logger2 = order.GetLogger(mock2, expression2);

            Assert.AreEqual(1, logger1.LastCall);
            Assert.AreEqual(2, logger2.LastCall);
        }
    }
}
