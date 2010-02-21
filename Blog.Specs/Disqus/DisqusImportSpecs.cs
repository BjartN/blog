using System.Configuration;
using System.Web.Script.Serialization;
using Blog.Infrastructure.Disqus;
using NUnit.Framework;
using System.IO;

namespace Blog.Specs.DisqusImport
{
  [TestFixture]
  public class when_importing_from_disqus
  {
    [Test]
    public void should_import()
    {
      var importer = new DisqusImporter(Directory.GetCurrentDirectory() + "/comments.xml");
      var res = importer.Import();
    }
  }

  [TestFixture]
  public class when_importing_remotely_from_disqus
  {
      private DisqusHttpImporter _importer;

      [SetUp]
      public void setup()
      {
           _importer = new DisqusHttpImporter(ConfigurationManager.AppSettings["disqus-forum-api-key"]);
      }

      [Test]
      public void should_import_threads()
      {
          var res = _importer.ImportThreadList();
          var ser = new JavaScriptSerializer();
          var threads = ser.Deserialize<DisqusThreads>(res);
      }

      [Ignore][Test]
      public void should_update_thread_with_new_url()
      {
          var res = _importer.UpdateThread("", "");
      }

      [Test]
      public void should_build_new_urls()
      {
          var res = _importer.BuildNewUrls();
      }
  }

}