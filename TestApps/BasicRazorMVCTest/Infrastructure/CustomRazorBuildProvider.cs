using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.WebPages.Razor;

namespace BasicRazorMVCTest
{
    public class CustomRazorBuildProvider : RazorBuildProvider
    {
        public static IEnumerable<Assembly> _assemblies;

        static CustomRazorBuildProvider()
        {
            string extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

            _assemblies = Directory.GetFiles(extensionsPath, "*.dll")
                .Select(Assembly.Load);
        }

        public override void GenerateCode(System.Web.Compilation.AssemblyBuilder assemblyBuilder)
        {
            foreach (var assembly in _assemblies)
                assemblyBuilder.AddAssemblyReference(assembly);

            base.GenerateCode(assemblyBuilder);
        }

    }
}