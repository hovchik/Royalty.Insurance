using System;
using System.Linq;
using System.Reflection;

namespace Royalty.Insurance.SessionScheduler
{
    public class FunctionsAssemblyResolver
    {
        public static void RedirectAssembly()
        {
            var list = AppDomain.CurrentDomain.GetAssemblies().OrderByDescending(a => a.FullName).Select(a => a.FullName).ToList();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var requestedAssembly = new AssemblyName(args.Name ?? throw new InvalidOperationException("Can not find resolve"));
            Assembly assembly = null;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            assembly = Assembly.Load(requestedAssembly.Name ?? throw new InvalidOperationException("Can not find Assembly"));

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return assembly;
        }
    }
}
