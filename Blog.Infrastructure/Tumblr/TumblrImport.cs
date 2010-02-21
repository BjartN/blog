using System.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Blog.Core;

namespace Blog.Infrastructure.Tumblr
{
  public class Importer
  {
    private string _fileName;

    public Importer(string fileName)
    {
      _fileName = fileName;
    }

    public IEnumerable<Post> Import()
    {
      XDocument xmlFile = XDocument.Load(_fileName);
      var query = from c in xmlFile.Element("tumblr").Element("posts").Elements("post")
                  select new Post
                  {
                    Slug = c.Attribute("slug").Value,
                    Url = c.Attribute("url").Value,
                    Id = c.Attribute("id").Value,
                    GmtDate = DateTime.Parse(c.Attribute("date-gmt").Value),
                    Title = c.Element("regular-title") != null ? c.Element("regular-title").Value : null,
                    Body = c.Element("regular-body") != null ? c.Element("regular-body").Value : null,
                    Tags = c.Elements("tag").Select(e=>e.Value).ToList()
                  };
      
      return query.ToList().Where(x=>x.Title!=null && x.Body!=null);
    } 
  }
}
