using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tests.Resources.Fonts
{
    internal class ResourceReader
    {
        private static ResourceReader instance;

        private ResourceReader()
        { }

        public static ResourceReader GetInstance => instance ?? (instance = new ResourceReader());

        public Stream ReadStream(string resourceName, Assembly assembly)
        {
            return Read(resourceName, assembly, stream => stream);
        }

        public string ReadString(string resourceName, Assembly assembly)
        {
            return Read(resourceName, assembly, stream =>
            {
                const int winAnsiEncoding = 1252;
                using (var reader = new StreamReader(stream, Encoding.GetEncoding(winAnsiEncoding)))
                {
                    return reader.ReadToEnd();
                }
            });
        }

        public byte[] ReadBytes(string resourceName, Assembly assembly)
        {
            return Read(resourceName, assembly, stream =>
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                return buffer;
            });
        }

        private TResult Read<TResult>(string resourceName, Assembly assembly, Func<Stream, TResult> process)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null) return process(stream);

                var manifestResourceNames = assembly.GetManifestResourceNames();
                var manifestResourceNamesList = string.Join(Environment.NewLine, manifestResourceNames);
                throw new FileNotFoundException($"Embedded resource {resourceName} not found in assembly {assembly.FullName}, " +
                                                $"these are valid resources:{Environment.NewLine}{manifestResourceNamesList}");
            }
        }
    }
}