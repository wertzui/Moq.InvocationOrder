using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace Moq.InvocationOrder.Tests
{
    [TestClass]
    public class MockExtensionsTests
    {
        private MockInvocationOrder order;
        private Mock<object> mock1;
        private Mock<object> mock2;
        Expression<Func<object, string>> expression1;
        Expression<Func<object, int>> expression2;

        [TestInitialize]
        public void TestInitialize()
        {
            order = new MockInvocationOrder();
            mock1 = new Mock<object>();
            mock2 = new Mock<object>();
            expression1 = o => o.ToString();
            expression2 = o => o.GetHashCode();

            mock1.SetupWithOrder(expression1, order);
            mock2.SetupWithOrder(expression2, order);
        }

        [TestMethod]
        public void SetupWithOrder_registers_mock_correctly()
        {
            // Arrange

            // Act
            mock1.Object.ToString();

            // Assert
            var logger1 = order.GetLogger(mock1, expression1);
            Assert.AreEqual(1, logger1.LastCall);
        }

        [TestMethod]
        public void VerifyWasCalledBefore_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock1.Object.ToString();
            mock2.Object.GetHashCode();

            // Assert
            mock1.VerifyWasCalledBefore(expression1, mock2, expression2, order);
        }

        [TestMethod]
        public void VerifyWasCalledAfter_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock2.Object.GetHashCode();
            mock1.Object.ToString();

            // Assert
            mock1.VerifyWasCalledAfter(expression1, mock2, expression2, order);
        }

        [TestMethod]
        public void VerifyWasNotCalledBefore_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock2.Object.GetHashCode();
            mock1.Object.ToString();

            // Assert
            mock1.VerifyWasNotCalledBefore(expression1, mock2, expression2, order);
        }

        [TestMethod]
        public void VerifyWasNotCalledAfter_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock1.Object.ToString();
            mock2.Object.GetHashCode();

            // Assert
            mock1.VerifyWasNotCalledAfter(expression1, mock2, expression2, order);
        }

        [TestMethod]
        public void VerifyWasOnlyCalledBefore_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock1.Object.ToString();
            mock2.Object.GetHashCode();

            // Assert
            mock1.VerifyWasOnlyCalledBefore(expression1, mock2, expression2, order);
        }

        [TestMethod]
        public void VerifyWasOnlyCalledAfter_returns_true_if_expression1_was_called_before_expression2()
        {
            // Arrange

            // Act
            mock2.Object.GetHashCode();
            mock1.Object.ToString();

            // Assert
            mock1.VerifyWasOnlyCalledAfter(expression1, mock2, expression2, order);
        }
    }
}