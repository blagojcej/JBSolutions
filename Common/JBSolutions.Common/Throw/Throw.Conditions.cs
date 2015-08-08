using System.Diagnostics;

namespace JBSolutions.Common
{
    static partial class Throw
    {
        #region Methods
        /// <summary>
        /// Evaluates whether an exception can be thrown.
        /// </summary>
        /// <param name="result">The result condition.</param>
        /// <returns>An instance of <see cref="ThrowEvaluation"/> used to throw exceptions.</returns>
        [DebuggerStepThrough]
        public static ThrowEvaluation If(bool result)
        {
            return new ThrowEvaluation(result);
        }

        /// <summary>
        /// Evaluates whether an exception can be thrown.
        /// </summary>
        /// <param name="result">The result condition.</param>
        /// <returns>An instance of <see cref="ThrowEvaluation"/> used to throw exceptions.</returns>
        [DebuggerStepThrough]
        public static ThrowEvaluation IfNot(bool result)
        {
            return new ThrowEvaluation(!result);
        }
        #endregion
    }
}
