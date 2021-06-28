namespace Shared
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    public class LoadedDLL
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public static IEnumerable<LoadedDLL> GetLoadedDLLs()
        {
            List<LoadedDLL> loadedDLLs = new List<LoadedDLL>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemblyName = assembly.GetName().ToString();
                string assemblyLocation = assembly.IsDynamic ? "" : assembly.Location;
                LoadedDLL dll = new LoadedDLL { Name = assemblyName, Location = assemblyLocation };
                loadedDLLs.Add(dll);
            }
            loadedDLLs.Sort((x, y) => x.Name.CompareTo(y.Name));

            return loadedDLLs.ToArray();
        }
    }
}
