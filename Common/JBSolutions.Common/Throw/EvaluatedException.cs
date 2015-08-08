using System;

namespace JBSolutions.Common
{
    /// <summary>
    /// Defines an exception to be thrown when an intended exception could not be created.
    /// </summary>
    public sealed class EvaluatedException : Exception
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="EvaluatedException"/>.
        /// </summary>
        /// <param name="exception">The exception that was intended to be thrown.</param>
        /// <param name="message">The original message for the exception.</param>
        internal EvaluatedException(Type exception, string message)
            : base(StringResources.EvaluatedException.With(exception.FullName, message))
        {
            ExceptionType = exception;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the intended exception type to be thrown.
        /// </summary>
        public Type ExceptionType { get; private set; }
        #endregion
    }
}
