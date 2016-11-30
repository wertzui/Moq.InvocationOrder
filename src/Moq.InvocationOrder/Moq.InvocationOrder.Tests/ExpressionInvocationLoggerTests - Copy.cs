using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.InvocationOrder;

namespace Moq.InvocationOrder.Tests
{
    [TestClass]
    public class ExpressionInvocationLoggerTests
    {
        ExpressionInvocationLogger logger1;
        ExpressionInvocationLogger logger2;

        [TestInitialize]
        public void TestInitialize()
        {
            logger1 = new ExpressionInvocationLogger();
            logger2 = new ExpressionInvocationLogger();
        }

        [TestMethod]
        public void First_and_last_call_are_correctly_incremented()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.AreEqual(1, logger1.FirstCall);
            Assert.AreEqual(2, logger1.LastCall);
        }

        [TestMethod]
        public void logger1_WasCalledBefore_returns_false_if_only_logger1_was_logged()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);

            // Assert
            Assert.IsFalse(logger1.WasCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledBefore_returns_false_if_only_logger2_was_logged()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);

            // Assert
            Assert.IsFalse(logger1.WasCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledBefore_returns_true_if_logger1_was_logged_before_logger2()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger2.LogInvocation(2);

            // Assert
            Assert.IsTrue(logger1.WasCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledBefore_returns_false_if_logger2_was_logged_before_logger1()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.IsFalse(logger1.WasCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledAfter_returns_false_if_only_logger1_was_logged()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);

            // Assert
            Assert.IsFalse(logger1.WasCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledAfter_returns_false_if_only_logger2_was_logged()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);

            // Assert
            Assert.IsFalse(logger1.WasCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledAfter_returns_true_if_logger1_was_logged_after_logger2()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.IsTrue(logger1.WasCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasCalledAfter_returns_false_if_logger2_was_logged_after_logger1()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger2.LogInvocation(2);

            // Assert
            Assert.IsFalse(logger1.WasCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledBefore_returns_true_if_only_logger2_was_logged()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledBefore_returns_true_if_only_logger1_was_logged()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledBefore_returns_true_if_logger2_was_logged_before_logger1()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledBefore_returns_false_if_logger1_was_logged_before_logger2()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger2.LogInvocation(2);

            // Assert
            Assert.IsFalse(logger1.WasNotCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledAfter_returns_true_if__only_logger1_was_logged()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledAfter_returns_true_if__only_logger2_was_logged()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledAfter_returns_true_if_logger1_was_logged_before_logger2()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger2.LogInvocation(2);

            // Assert
            Assert.IsTrue(logger1.WasNotCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasNotCalledAfter_returns_false_if_logger2_was_logged_before_logger1()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.IsFalse(logger1.WasNotCalledAfter(logger2));
        }

        [TestMethod]
        public void logger1_WasOnlyCalledBefore_returns_false_if_only_logger1_was_logged()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);

            // Assert
            Assert.IsFalse(logger1.WasOnlyCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasOnlyCalledBefore_returns_true_if_only_logger2_was_logged()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);

            // Assert
            Assert.IsTrue(logger1.WasOnlyCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasOnlyCalledBefore_returns_true_if_logger1_was_logged_before_logger2()
        {
            // Arrange

            // Act
            logger1.LogInvocation(1);
            logger2.LogInvocation(2);

            // Assert
            Assert.IsTrue(logger1.WasOnlyCalledBefore(logger2));
        }

        [TestMethod]
        public void logger1_WasOnlyCalledBefore_returns_false_if_logger2_was_logged_before_logger1()
        {
            // Arrange

            // Act
            logger2.LogInvocation(1);
            logger1.LogInvocation(2);

            // Assert
            Assert.IsFalse(logger1.WasOnlyCalledBefore(logger2));
        }
    }
}
