using System;
using System.Diagnostics;

namespace JBSolutions.Common
{
    /// <summary>
    /// Provides static methods for conditional exceptions.
    /// </summary>
    public static partial class Throw
    {
        #region Fields
        private static Func<Exception, Exception> GlobalModifier;
        #endregion

        #region Methods
        /// <summary>
        /// Applies the given modifier.  If the modifier returns null, the original exception is returned.
        /// </summary>
        /// <param name="exception">The exception to modify.</param>
        /// <param name="modifier">The modifier to apply.</param>
        /// <returns>The modified exception, or the original exception if the modifier returns null.</returns>
        [DebuggerStepThrough]
        private static Exception ApplyModifier(Exception exception, Func<Exception, Exception> modifier)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            if (modifier == null)
                throw new ArgumentNullException("modifier");

            var ex = modifier(exception);
            return ex ?? exception;
        }

        /// <summary>
        /// Throws an exception immediately.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="messageFactory">The message factory used to create the message.</param>
        /// <param name="modifier">[Optional] A modifier used to modify the exception before throwing.</param>
        [DebuggerStepThrough]
        public static void Now<TException>(Func<string> messageFactory, Func<Exception, Exception> modifier = null)
            where TException : Exception
        {
            var eval = new ThrowEvaluation(true);
            eval.As<TException>(messageFactory, modifier);
        }

        /// <summary>
        /// Sets the global modifier used for all exceptions that are thrown. The global modifier is applied after any direct modifiers.
        /// </summary>
        /// <param name="modifier">The modifier to use.</param>
        [DebuggerStepThrough]
        public static void SetGlobalModifier(Func<Exception, Exception> modifier)
        {
            if (modifier == null)
                throw new ArgumentNullException("modifier");

            GlobalModifier = modifier;
        }

        /// <summary>
        /// Throws the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be thrown.</param>
        /// <param name="modifier">[Optional] A modifier used to modify the exception before throwing.</param>
        [DebuggerStepThrough]
        internal static void ThrowInternal(Exception exception, Func<Exception, Exception> modifier = null)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            if (modifier != null)
                exception = ApplyModifier(exception, modifier);

            if (GlobalModifier != null)
                exception = ApplyModifier(exception, GlobalModifier);

            throw exception;
        }

        /// <summary>
        /// Throws a <see cref="InvalidOperationException" /> with the specified message.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        public static void InvalidOperation(string message, Func<Exception, Exception> modifier = null)
        {
            ThrowInternal(new InvalidOperationException(message), modifier);
        }

        /// <summary>
        /// Throws a <see cref="NotSupportedException" /> with the specified message.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        public static void NotSupported(string message, Func<Exception, Exception> modifier = null)
        {
            ThrowInternal(new NotSupportedException(message), modifier);
        }
        #endregion
    }
}
