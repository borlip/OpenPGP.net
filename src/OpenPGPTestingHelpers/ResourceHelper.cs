using System;
using System.IO;
using System.Reflection;

namespace OpenPGPTestingHelpers
{
    public static class ResourceHelper
    {
        public static string GetResourceAsString(Assembly assembly, string name)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null or empty", "name");
            }

            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null) return null;

                var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
        }

        public static byte[] GetResourceAsByteArray(Assembly assembly, string name)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null or empty", "name");
            }

            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null) return null;

                var length = (int)stream.Length;
                var result = new byte[length];
                stream.Read(result, 0, length);

                return result;
            }
        }

        public static Stream GetResourceAsStream(Assembly assembly, string name)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null or empty", "name");
            }

            return assembly.GetManifestResourceStream(name);
        }
    }
}
