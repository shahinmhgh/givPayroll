using System.Reflection;

public static class AppVersion
{
    //public static string Current =>
    //    Assembly.GetEntryAssembly()?
    //        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
    //        .InformationalVersion ?? "Unknown";

    public static string Current
    {
        get
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                return "Unknown";
            }

            AssemblyInformationalVersionAttribute attribute =
             assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            if (attribute == null)
            {
                return "Unknown";
            }

            string version = attribute.InformationalVersion;

            if (string.IsNullOrEmpty(version))
            {
                return "Unknown";
            }

            int index = version.IndexOf('+');

            if (index >= 0)
            {
                version = version.Substring(0, index);
            }

            return version;
        }
    }

}