using System;
using System.IO;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        [TestFixtureSetUp]
        public abstract void Arrange();
        public abstract void Act();

        [TestFixtureTearDown]
        public virtual void CleanUp(){}

        protected void DumpToFile(byte[] buffer, string filename)
        {
            using (var fs = File.OpenWrite(filename))
                fs.Write(buffer, 0, buffer.Length);
        }

        protected string DumpToRandomFile(byte[] buffer, string extension)
        {
            var filename = string.Format(@"C:\temp\{0}.{1}", Guid.NewGuid(), extension);

            DumpToFile(buffer, filename);

            return filename;
        }

        protected string DumpToRandomFile(Stream stream, string extension)
        {
            var length = (int)stream.Length;
            var buffer = new byte[length];
            stream.Read(buffer, 0, length);

            return DumpToRandomFile(buffer, extension);
        }
    }
}