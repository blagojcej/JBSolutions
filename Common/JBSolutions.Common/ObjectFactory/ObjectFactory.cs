using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JBSolutions.Common
{
    /// <summary>
    /// Provides methods for creating instances of objects.
    /// </summary>
    public static class ObjectFactory
    {
        #region Fields
        private static readonly IList<ObjectFactoryBinding> Bindings = new List<ObjectFactoryBinding>();
        #endregion

        #region Methods
        /// <summary>
        /// Creates an object activator for the specified type and arguments.
        /// </summary>
        /// <param name="type">The type to be created by the activator.</param>
        /// <param name="arguments">The argument types to pass to the constructor.</param>
        /// <returns>An instance of the an activator</returns>
        [DebuggerStepThrough]
        private static ObjectActivator CreateActivator(Type type, Type[] arguments)
        {
            var constructor = type.GetConstructor(arguments);

            Throw.If(constructor == null)
                .As<InvalidOperationException>(() =>
                                               StringResources.ConstructorMismatch
                                                   .With(type.FullName,
                                                         string.Join(", ", arguments.Select(a => a.FullName))));

            var expression = CreateExpression(constructor, arguments);
            ObjectActivator activator = (ObjectActivator)expression.Compile();

            return activator;
        }

        /// <summary>
        /// Creates an object activator for the specified type and arguments.
        /// </summary>
        /// <typeparam name="T">The type to be created by the activator.</typeparam>
        /// <param name="arguments">The argument types to pass to the constructor.</param>
        /// <returns>An instance of the an activator</returns>
        [DebuggerStepThrough]
        private static ObjectActivator<T> CreateActivator<T>(Type[] arguments)
        {
            Type type = typeof(T);

            var constructor = type.GetConstructor(arguments);

            Throw.If(constructor == null)
                .As<InvalidOperationException>(() =>
                                               StringResources.ConstructorMismatch
                                                   .With(type.FullName,
                                                         string.Join(", ", arguments.Select(a => a.FullName))));

            var expression = CreateExpression<T>(constructor, arguments);
            ObjectActivator<T> activator = (ObjectActivator<T>)expression.Compile();

            return activator;
        }

        /// <summary>
        /// Creates a lambda expression to create the activator instance.
        /// </summary>
        /// <param name="constructor">The constructor used to create the instance.</param>
        /// <param name="argTypes">The argument types.</param>
        /// <returns>A lambda expression to create the activator instance.</returns>
        [DebuggerStepThrough]
        private static LambdaExpression CreateExpression(ConstructorInfo constructor, Type[] argTypes)
        {
            var param = Expression.Parameter(typeof(object[]), "args");
            var args = new Expression[argTypes.Length];

            for (int i = 0; i < argTypes.Length; i++)
            {
                var index = Expression.Constant(i);
                Type paramType = argTypes[i];

                var accessor = Expression.ArrayIndex(param, index);
                var cast = Expression.Convert(accessor, paramType);

                args[i] = cast;
            }

            var @new = Expression.New(constructor, args);
            var lambda = Expression.Lambda(typeof(ObjectActivator), @new, param);

            return lambda;
        }

        /// <summary>
        /// Creates a lambda expression to create the activator instance.
        /// </summary>
        /// <typeparam name="T">The type to be created by the activator.</typeparam>
        /// <param name="constructor">The constructor used to create the instance.</param>
        /// <param name="argTypes">The argument types.</param>
        /// <returns>A lambda expression to create the activator instance.</returns>
        [DebuggerStepThrough]
        private static LambdaExpression CreateExpression<T>(ConstructorInfo constructor, Type[] argTypes)
        {
            var param = Expression.Parameter(typeof(object[]), "args");
            var args = new Expression[argTypes.Length];

            for (int i = 0; i < argTypes.Length; i++)
            {
                var index = Expression.Constant(i);
                Type paramType = argTypes[i];

                var accessor = Expression.ArrayIndex(param, index);
                var cast = Expression.Convert(accessor, paramType);

                args[i] = cast;
            }

            var @new = Expression.New(constructor, args);
            var lambda = Expression.Lambda(typeof(ObjectActivator<T>), @new, param);

            return lambda;
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type to be created by the activator.</param>
        /// <param name="args">The arguments to pass to the constructor.</param>
        /// <returns>A new instance of the specified type.</returns>
        [DebuggerStepThrough]
        public static object CreateInstance(Type type, params object[] args)
        {
            var activator = GetActivator(type, args);
            return activator(args);
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of instance to create.</typeparam>
        /// <param name="args">The arguments to pass to the constructor.</param>
        /// <returns>A new instance of the specified type.</returns>
        [DebuggerStepThrough]
        public static T CreateInstance<T>(params object[] args)
        {
            var activator = GetActivator<T>(args);
            return activator(args);
        }

        /// <summary>
        /// Gets an object activator for the specified type and arguments.
        /// </summary>
        /// <param name="type">The type to be created by the activator.</param>
        /// <param name="args">The arguments to pass to the constructor.</param>
        /// <returns>An instance of the an activator</returns>
        [DebuggerStepThrough]
        public static ObjectActivator GetActivator(Type type, params object[] args)
        {
            var argumentTypes = GetArgumentTypes(args);
            var binding = Bindings
                .Where(b => b.ObjectType == type && b.ConstructorArgumentTypes.SequenceEqual(argumentTypes))
                .SingleOrDefault();

            if (binding != null)
                return (ObjectActivator)binding.Delegate;

            var activator = CreateActivator(type, argumentTypes);
            Bindings.Add(new ObjectFactoryBinding(type, argumentTypes, activator));

            return activator;
        }

        /// <summary>
        /// Gets an object activator for the specified type and arguments.
        /// </summary>
        /// <typeparam name="T">The type to be created by the activator.</typeparam>
        /// <param name="args">The arguments to pass to the constructor.</param>
        /// <returns>An instance of the an activator</returns>
        [DebuggerStepThrough]
        public static ObjectActivator<T> GetActivator<T>(params object[] args)
        {
            Type type = typeof(T);
            var argumentTypes = GetArgumentTypes(args);
            var binding = Bindings
                .Where(b => b.ObjectType == type && b.ConstructorArgumentTypes.SequenceEqual(argumentTypes))
                .SingleOrDefault();

            if (binding != null)
                return (ObjectActivator<T>)binding.Delegate;

            var activator = CreateActivator<T>(argumentTypes);
            Bindings.Add(new ObjectFactoryBinding(type, argumentTypes, activator));

            return activator;
        }

        /// <summary>
        /// Gets an array of types for the specified arguments.
        /// </summary>
        /// <param name="args">The arguments to get types for.</param>
        /// <returns>An array of types.</returns>
        [DebuggerStepThrough]
        private static Type[] GetArgumentTypes(params object[] args)
        {
            if (args == null)
                return new Type[0];

            return args
                .Select(a => a.GetType())
                .ToArray();
        }
        #endregion
    }
}
