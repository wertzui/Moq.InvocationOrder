using Moq.InvocationOrder;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;

namespace Moq
{
    /// <summary>
    /// Extension Methods for easy setup of invocation order checking.
    /// </summary>
    public static class MoqExtensions
    {
        /// <summary>
        /// Specifies a setup with a verifyable order on the mocked type for a call to to a value returning method.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">Type of the return value. Typically omitted as it can be inferred from the expression.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">Lambda expression that specifies the method invocation.</param>
        /// <param name="order">The order.</param>
        /// <remarks>
        /// If more than one setup is specified for the same method or property, the latest one wins and is the one that will be executed.
        /// </remarks>
        /// <returns></returns>
        public static ISetup<TMock, TResult> SetupWithOrder<TMock, TResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, MockInvocationOrder order)
            where TMock : class
        {
            var setup = mock.Setup(expression);
            order.Setup(mock, expression);
            setup.Callback(() => order.LogInvocation(mock, expression));
            return setup;
        }

        /// <summary>
        /// Verifies that the given expression on this mock was called before the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasCalledBefore<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
            where TMock : class
            where TOtherMock : class
            => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasCalledBefore(o), "Expected {1} of mock {0} to be called before {3} of mock {2}.");

        /// <summary>
        /// Verifies that the given expression on this mock was called after the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasCalledAfter<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
            where TMock : class
            where TOtherMock : class
            => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasCalledAfter(o), "Expected {1} of mock {0} to be called after {3} of mock {2}.");

        /// <summary>
        /// Verifies that the given expression on this mock was not called before the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasNotCalledBefore<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
            where TMock : class
            where TOtherMock : class
            => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasNotCalledBefore(o), "Expected {1} of mock {0} to not be called before {3} of mock {2}.");

        /// <summary>
        /// Verifies that the given expression on this mock was not called after the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasNotCalledAfter<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
            where TMock : class
            where TOtherMock : class
            => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasNotCalledAfter(o), "Expected {1} of mock {0} to not be called after {3} of mock {2}.");

        /// <summary>
        /// Verifies that the given expression on this mock was only called before the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasOnlyCalledBefore<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
                    where TMock : class
                    where TOtherMock : class
                    => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasOnlyCalledBefore(o), "Expected {1} of mock {0} to be only called before {3} of mock {2}.");

        /// <summary>
        /// Verifies that the given expression on this mock was only called after the other expression on the other mock.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TOtherMock">The type of the other mock.</typeparam>
        /// <typeparam name="TOtherResult">The type of the other result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="other">The other mock.</param>
        /// <param name="otherexpression">The expression of the other mock.</param>
        /// <param name="order">The order that was used in the setup.</param>
        public static void VerifyWasOnlyCalledAfter<TMock, TResult, TOtherMock, TOtherResult>(this Mock<TMock> mock, Expression<Func<TMock, TResult>> expression, Mock<TOtherMock> other, Expression<Func<TOtherMock, TOtherResult>> otherexpression, MockInvocationOrder order)
                    where TMock : class
                    where TOtherMock : class
                    => Verify(mock, expression, other, otherexpression, order, (m, o) => m.WasOnlyCalledAfter(o), "Expected {1} of mock {0} to be only called after {3} of mock {2}.");

        private static void Verify<TMock, TResult, TOtherMock, TOtherResult>(
            Mock<TMock> mock,
            Expression<Func<TMock, TResult>> expression,
            Mock<TOtherMock> other,
            Expression<Func<TOtherMock, TOtherResult>> otherExpression,
            MockInvocationOrder order,
            Func<ExpressionInvocationLogger, ExpressionInvocationLogger, bool> verifyFunction,
            string exceptionMessage)
            where TMock : class
            where TOtherMock : class
        {
            if (verifyFunction == null)
                throw new ArgumentNullException(nameof(verifyFunction));

            var mockCounter = order.GetLogger(mock, expression);
            var otherCounter = order.GetLogger(other, otherExpression);

            if (!verifyFunction(mockCounter, otherCounter))
                throw new MockInvocationOrderException(string.Format(exceptionMessage, mock, expression, other, otherExpression));
        }
    }
}