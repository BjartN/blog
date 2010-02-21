using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Blog.Core.Comments;

namespace Blog.Infrastructure.Disqus
{
  public class DisqusImporter
  {
    private string _fileName;

    public DisqusImporter(string fileName)
    {
      _fileName = fileName;
    }

    public IEnumerable<Thread> Import()
    {
      XDocument xmlFile = XDocument.Load(_fileName);
      var query = xmlFile
        .Element("articles")
        .Elements("article")
        .Select(a =>
                new Thread
                {
                  Url = a.Element("url").Value,
                  Comments = a
                    .Element("comments")
                    .Elements("comment").
                    Select(
                    c => new Comment
                    {
                      Name = c.Element("name").Value,
                      Email = c.Element("email").Value,
                      IpAddress = c.Element("ip_address").Value,
                      Message = c.Element("message").Value,
                      Date = DateTime.Parse(c.Element("date").Value)
                    }
                    ).ToList()
                });

      return query.ToList();
    }
  }
}