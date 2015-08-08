using System;
using System.Diagnostics;

namespace JBSolutions.Common
{
    /// <summary>
    /// Throws evaluated conditions.
    /// </summary>
    public class ThrowEvaluation
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ThrowEvaluation"/>.
        /// </summary>
        /// <param name="throwException">Should an exception be thrown?</param>
        [DebuggerStepThrough]
        internal ThrowEvaluation(bool throwException)
        {
            ThrowException = throwException;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether an exception should be thrown.
        /// </summary>
        public bool ThrowException { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Throws an exception of the specified type.
        /// </summary>
        /// <typeparam name="TException">The type of exception to be thrown.</typeparam>
        /// <param name="messageFactory">A delegate used to get the message to be thrown.</param>
        /// <param name="modifier">[Optional] A delegate used to modifier the exception before being thrown.</param>
        [DebuggerStepThrough]
        public void As<TException>(Func<string> messageFactory, Func<Exception, Exception> modifier = null) where TException : Exception
        {
            if (ThrowException)
            {
                string message = messageFactory();
                var activator = ObjectFactory.GetActivator<TException>(message);

                Exception exception = (activator == null)
                                ? (Exception)new EvaluatedException(typeof(TException), message)
                                : activator(message);

                Throw.ThrowInternal(exception, modifier);
            }
        }
        #endregion
    }
}
