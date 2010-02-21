using System.Linq;
using Blog.Infrastructure.Tumblr;
using NUnit.Framework;
using System.IO;

namespace Blog.Tests
{
    [TestFixture]
    public class ImporterTests
    {
        private Importer _importer;

        [SetUp]
        public void setup_each_test()
        {
            _importer = new Importer(Path.Combine(Directory.GetCurrentDirectory(),"read.xml"));
        }

        [Test]
        public void should_import()
        {
            var res = _importer.Import();

            Assert.That(res.Count()>0);
        }

    }
}