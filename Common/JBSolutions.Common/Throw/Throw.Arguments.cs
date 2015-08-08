using System;
using System.Diagnostics;

namespace JBSolutions.Common
{
    static partial class Throw
    {
        #region Methods
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
        /// </summary>
        /// <param name="argument">The argument to check</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">[Optional] A modifier used to modify the exception before throwing.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNull(object argument, string argumentName, Func<Exception, Exception> modifier = null)
        {
            if (argument == null)
            {
                var ex = new ArgumentNullException(argumentName);
                ThrowInternal(ex, modifier);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the specified argument is null or empty.
        /// </summary>
        /// <param name="argument">The argument to check</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">[Optional] A modifier used to modify the exception before throwing.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNullOrEmpty(string argument, string argumentName, Func<Exception, Exception> modifier = null)
        {
            if (string.IsNullOrEmpty(argument))
            {
                var ex = new ArgumentException(StringResources.ArgumentNullOrEmpty.With(argumentName), argumentName);
                ThrowInternal(ex, modifier);
            }
        }
        #endregion
    }
}
