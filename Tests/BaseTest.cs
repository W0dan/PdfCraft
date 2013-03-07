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

        protected void DumpToFile(byte[] buffer, string filename)
        {
            using (var fs = File.OpenWrite(filename))
                fs.Write(buffer, 0, buffer.Length);
        }

        protected void DumpToRandomFile(byte[] buffer, string extension)
        {
            var filename = string.Format(@"C:\temp\{0}.{1}", Guid.NewGuid(), extension);

            DumpToFile(buffer, filename);
        }
    }
}