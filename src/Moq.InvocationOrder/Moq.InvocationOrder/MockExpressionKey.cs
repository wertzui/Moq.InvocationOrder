using System;
using System.Linq.Expressions;
using yesmarket.Linq.Expressions;

namespace Moq.InvocationOrder
{
    /// <summary>
    /// Key with a mock and an expression to be used in dictionaries.
    /// </summary>
    public class MockExpressionKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockExpressionKey"/> class.
        /// </summary>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <exception cref="System.ArgumentNullException">
        /// mock
        /// or
        /// expression
        /// </exception>
        public MockExpressionKey(Mock mock, Expression expression)
        {
            if (mock == null)
                throw new ArgumentNullException(nameof(mock));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            Mock = mock;
            Expression = expression;
        }

        private static ExpressionEqualityComparer equalityComparer = new ExpressionEqualityComparer();

        /// <summary>
        /// Gets the mock.
        /// </summary>
        /// <value>
        /// The mock.
        /// </value>
        public Mock Mock { get; }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public Expression Expression { get; }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as MockExpressionKey);

        /// <summary>
        /// Determines whether the specified <see cref="MockExpressionKey" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="MockExpressionKey" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="MockExpressionKey" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(MockExpressionKey other)
        {
            if (other == null)
                return false;

            return Mock.Object.GetType().Equals(other.Mock.Object.GetType()) && equalityComparer.Equals(Expression, other.Expression);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => Mock.Object.GetType().GetHashCode();
    }
}