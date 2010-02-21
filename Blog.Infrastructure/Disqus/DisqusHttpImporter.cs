using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Blog.Core;
using Blog.Infrastructure.Db4o;

namespace Blog.Infrastructure.Disqus
{
    public class DisqusHttpImporter
    {
        private static string _forumApiKey;
        private const string _updateThreadUrl = @"http://disqus.com/api/update_thread/";
        private const string _updateThreadParams = @"forum_api_key={0}&thread_id={1}&url={2}";
        private const string _getThreadListUrl = @"http://disqus.com/api/get_thread_list?forum_api_key={0}";

        public DisqusHttpImporter(string forumApiKey)
        {
            _forumApiKey = forumApiKey;
        }
      
        public DisqusThreads BuildNewUrls()
        {
            var ser = new JavaScriptSerializer();
            var threads = ser.Deserialize<DisqusThreads>(File.ReadAllText(@"C:\Users\BjartN\Documents\Visual Studio 2008\Projects\Blog\Blog.Infrastructure\Disqus\threads.txt"));
            using (var repository = new Repository(@"C:\Users\BjartN\Documents\Visual Studio 2008\Projects\Blog\Blog\App_Data\Db2.yap"))
            {
                foreach (var t in threads.message)
                {
                    var id = t.url.Split('/').ToList().Last();
                    var post = Post.GetPost(id, repository);

                    if (post == null)
                        t.newurl = t.url;
                    else
                        t.newurl = "http://bjarte.com/post/" + post.Slug;
                }
            }
            return threads;
        }

        public string ImportThreadList()
        {
            var url = string.Format(_getThreadListUrl, _forumApiKey);
            return getUrl(url, "GET");
        }

        public bool UpdateThread(string thread_id, string newUrl)
        {
            //temp
            //thread_id = "33095598";
            //newUrl = "http://bjarte.com/post/178580883/even-a-bad-process-is-better-than-not-having-one";

            var parameters = string.Format(_updateThreadParams, _forumApiKey, thread_id, newUrl);
            try
            {
                postForm(_updateThreadUrl, parameters);
                return true;
            }
            catch (HttpException)
            {
                return false;
            }
        }

        private string postForm(string url, string parameters)
        {
            byte[] dataArray = Encoding.UTF8.GetBytes(parameters);

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            webRequest.ContentLength = dataArray.Length;
            var requestStream = webRequest.GetRequestStream();
            requestStream.Write(dataArray, 0, dataArray.Length);
            requestStream.Flush();
            requestStream.Close();

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            var responseReader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
            return responseReader.ReadToEnd();
        }

        private string getUrl(string url, string method)
        {
            var sb = new StringBuilder();
            var buf = new byte[8192];

            // prepare the web page we will be asking for
            var request = (HttpWebRequest)
                          WebRequest.Create(string.Format(url));
            request.Method = method;

            // execute the request
            var response = (HttpWebResponse)
                           request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpException("Error doing the wild monkey dance");

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    string tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // print out page source
            return sb.ToString();
        }

    }
}