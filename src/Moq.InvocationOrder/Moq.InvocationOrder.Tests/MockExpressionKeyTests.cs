using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Moq.InvocationOrder.Tests
{
    [TestClass]
    public class MockExpressionKeyTests
    {
        MockExpressionKey key1;
        MockExpressionKey key2;

        [TestInitialize]
        public void TestInitialize()
        {
            var mock1 = new Mock<object>();
            var mock2 = new Mock<object>();
            Expression<Func<object, string>> expression1 = o => o.ToString();
            Expression<Func<object, string>> expression2 = o => o.ToString();
            key1 = new MockExpressionKey(mock1, expression1);
            key2 = new MockExpressionKey(mock2, expression2);
        }

        [TestMethod]
        public void Two_keys_with_same_Mock_type_and_equal_expressions_are_equal()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(key1, key2);
        }

        [TestMethod]
        public void Two_keys_with_same_Mock_type_and_equal_expressions_are_found_in_dictionary()
        {
            // Arrange
            var dict = new Dictionary<MockExpressionKey, int> { { key1, 1 } };

            // Act
            var containsKey = dict.ContainsKey(key2);

            // Assert
            Assert.IsTrue(containsKey);
        }

        [TestMethod]
        public void Key_with_It_equals_itself()
        {
            // Arrange
            Expression<Func<object, bool>> expr = o => o.Equals(It.IsAny<object>());
            var key = new MockExpressionKey(new Mock<object>(), expr);

            // Act
            var equalsSelf = key.Equals(key);

            // Assert
            Assert.IsTrue(equalsSelf);
        }
    }
}