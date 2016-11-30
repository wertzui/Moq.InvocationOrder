using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Moq.InvocationOrder
{
    /// <summary>
    /// Stores information about the invocation order of all mocks that used this instance in their setup.
    /// </summary>
    public class MockInvocationOrder
    {
        /// <summary>
        /// Gets the counter which represent the total invocations + 1.
        /// </summary>
        /// <value>
        /// The counter.
        /// </value>
        /// <remarks>
        /// The counter starts at 1.
        /// </remarks>
        public int Counter { get; private set; } = 1;

        /// <summary>
        /// Contains information about the invocation of each expression.
        /// </summary>
        /// <value>
        /// The invocations.
        /// </value>
        private IDictionary<MockExpressionKey, ExpressionInvocationLogger> Invocations { get; } = new Dictionary<MockExpressionKey, ExpressionInvocationLogger>();

        /// <summary>
        /// Logs an invocation of an expression.
        /// </summary>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        public void LogInvocation<TMock, TResult>(Mock<TMock> mock, Expression<Func<TMock, TResult>> expression) where TMock : class
        {
            var key = new MockExpressionKey(mock, expression);
            var logger = Invocations[key];
            logger.LogInvocation(Counter++);
        }

        /// <summary>
        /// Setups order of invocation logging for the mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        public void Setup<TMock, TResult>(Mock<TMock> mock, Expression<Func<TMock, TResult>> expression) where TMock : class
        {
            var key = new MockExpressionKey(mock, expression);
            Invocations[key] = new ExpressionInvocationLogger();
        }

        /// <summary>
        /// Gets the logger for the specified mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="MockInvocationOrderSetupException">There was no setup for the mock.</exception>
        public ExpressionInvocationLogger GetLogger<TMock, TResult>(Mock<TMock> mock, Expression<Func<TMock, TResult>> expression) where TMock : class
        {
            var key = new MockExpressionKey(mock, expression);
            ExpressionInvocationLogger logger;
            if (!Invocations.TryGetValue(key, out logger))
                throw new MockInvocationOrderSetupException("There was no setup for the mock.");
            return logger;
        }
    }
}