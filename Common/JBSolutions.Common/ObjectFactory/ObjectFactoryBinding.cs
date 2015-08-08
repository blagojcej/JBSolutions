using System;

namespace JBSolutions.Common
{
    /// <summary>
    /// Defines a binding between a type, the constructor used in its creation, and the delegate used to create it.
    /// </summary>
    internal class ObjectFactoryBinding
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ObjectFactoryBinding"/>
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="constructorArgumentTypes">The constructor argument types.</param>
        /// <param name="delegate">The delegate used to create it.</param>
        public ObjectFactoryBinding(Type objectType, Type[] constructorArgumentTypes, Delegate @delegate)
        {
            Throw.IfArgumentNull(objectType, "objectType");
            Throw.IfArgumentNull(constructorArgumentTypes, "constructorArgumentTypes");
            Throw.IfArgumentNull(@delegate, "delegate");

            ObjectType = objectType;
            ConstructorArgumentTypes = constructorArgumentTypes;
            Delegate = @delegate;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the object type.
        /// </summary>
        public Type ObjectType { get; private set; }

        /// <summary>
        /// Gets the constructor argument types.
        /// </summary>
        public Type[] ConstructorArgumentTypes { get; private set; }

        /// <summary>
        /// Gets the delegate used to create it.
        /// </summary>
        public Delegate Delegate { get; private set; }
        #endregion
    }
}
