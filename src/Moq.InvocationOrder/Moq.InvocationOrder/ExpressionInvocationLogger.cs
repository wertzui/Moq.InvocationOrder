namespace Moq.InvocationOrder
{
    /// <summary>
    /// Stores the first and last invocation of an expression
    /// </summary>
    public class ExpressionInvocationLogger
    {
        /// <summary>
        /// Gets or sets the Counter at the time of the first invocation.
        /// </summary>
        /// <value>
        /// The first call.
        /// </value>
        public int FirstCall { get; private set; }

        /// <summary>
        /// Gets or sets the Counter at the time of the last invocation.
        /// </summary>
        /// <value>
        /// The first call.
        /// </value>
        public int LastCall { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this logger was at least called once.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this logger was at least called once; otherwise, <c>false</c>.
        /// </value>
        public bool WasEverCalled => !WasNeverCalled;

        /// <summary>
        /// Gets a value indicating whether this logger was never called.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this logger was never called; otherwise, <c>false</c>.
        /// </value>
        public bool WasNeverCalled => FirstCall == 0;

        /// <summary>
        /// Determines if this expression was called at least one time before the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if this expression was called at least one time before the other expression; otherwise false.</returns>
        public bool WasCalledBefore(ExpressionInvocationLogger other)
            => WasEverCalled && FirstCall < other.LastCall;

        /// <summary>
        /// Determines if this expression was called at least one time after the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if this expression was called at least one time after the other expression; otherwise false.</returns>
        public bool WasCalledAfter(ExpressionInvocationLogger other)
            => other.WasEverCalled && LastCall > other.FirstCall;

        /// <summary>
        /// Determines if this expression was never called before the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if this expression was never called before the other expression; otherwise false.</returns>
        public bool WasNotCalledBefore(ExpressionInvocationLogger other)
            => FirstCall > other.FirstCall || WasNeverCalled || other.WasNeverCalled;

        /// <summary>
        /// Determines if this expression was never called after the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if this expression was never called after the other expression; otherwise false.</returns>
        public bool WasNotCalledAfter(ExpressionInvocationLogger other)
            => LastCall < other.LastCall || WasNeverCalled || other.WasNeverCalled;

        /// <summary>
        /// Determines if all invocations of this expression happened before the first call to the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if all invocations of this expression happened before the first call to the other expression; otherwise false.</returns>
        public bool WasOnlyCalledBefore(ExpressionInvocationLogger other)
            => WasEverCalled && LastCall < other.FirstCall || WasNeverCalled;

        /// <summary>
        /// Determines if all invocations of this expression happened after the last call to the other expression.
        /// </summary>
        /// <param name="other">The other expression.</param>
        /// <returns>true if all invocations of this expression happened after the last call to the other expression; otherwise false.</returns>
        public bool WasOnlyCalledAfter(ExpressionInvocationLogger other)
            => FirstCall > other.LastCall || WasNeverCalled || other.WasNeverCalled;

        /// <summary>
        /// Logs an invocation to this expression.
        /// </summary>
        /// <param name="counter">The current counter.</param>
        public void LogInvocation(int counter)
        {
            LastCall = counter;
            if (FirstCall == 0)
                FirstCall = counter;
        }
    }
}