using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Caching;
namespace SignalRChat
{
    public class UrlHelpers
    {
        public static string Version(string rootRelativePath)
        {
            if (HttpRuntime.Cache[rootRelativePath] == null)
            {
                var absolutePath = HostingEnvironment.MapPath(rootRelativePath);
                var lastChangedDateTime = File.GetLastWriteTime(absolutePath);
                if (rootRelativePath.StartsWith("~"))
                {
                    rootRelativePath = rootRelativePath.Substring(1);
                }
                var versionedUrl = rootRelativePath + "?v=" + lastChangedDateTime.Ticks;
                HttpRuntime.Cache.Insert(rootRelativePath, versionedUrl, new CacheDependency(absolutePath));
            }
            return HttpRuntime.Cache[rootRelativePath] as string;
        }
    }
}