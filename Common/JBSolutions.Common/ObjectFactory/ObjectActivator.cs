
namespace JBSolutions.Common
{
    /// <summary>
    /// Defines an activator used to create an instance of a type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <param name="args">The constructor arguments.</param>
    /// <returns>A new instance of the specified type.</returns>
    public delegate T ObjectActivator<T>(params object[] args);

    /// <summary>
    /// Defines an activator used to create an instance of a type.
    /// </summary>
    /// <param name="args">The constructor arguments.</param>
    /// <returns>A new instance of the type.</returns>
    public delegate object ObjectActivator(params object[] args);
}