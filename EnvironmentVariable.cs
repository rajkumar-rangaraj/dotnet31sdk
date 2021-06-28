namespace Shared
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class EnvironmentVariable
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public static IEnumerable<EnvironmentVariable> GetEnvironmentVariables()
        {
            List<EnvironmentVariable> envVariables = new List<EnvironmentVariable>();
            foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                string key = (string)env.Key;
                string value = (string)env.Value;
                EnvironmentVariable envVariable = new EnvironmentVariable { Name = key, Value = value };
                envVariables.Add(envVariable);
            }
            envVariables.Sort((x, y) => x.Name.CompareTo(y.Name));

            return envVariables.ToArray();
        }
    }
}
