using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exceptional.Core;

namespace Exceptional.Web.Mvc
{
    public static class Mapper
    {
        public static HttpRequestSummary CreateHttpRequestSummary(ExceptionContext context)
        {
            var summary = new HttpRequestSummary();

            var session = context.HttpContext.Session;
            var headers = context.HttpContext.Request.Headers;

            summary.Action = context.RouteData.Values["action"] as string;
            summary.Controller = context.RouteData.Values["controller"] as string;
            summary.Headers = headers.AllKeys.ToDictionary(k => k, k => headers[k]);
            summary.Parameters = context.RouteData.Values.Union(context.RouteData.DataTokens).ToDictionary(k => k.Key, v => Serializer.SanitizeItem(v.Value));
            summary.RemoteIp = context.HttpContext.Request.UserHostAddress;
            summary.RequestMethod = context.HttpContext.Request.HttpMethod;

            if (session != null)
                summary.Session = session.Keys.Cast<string>().ToDictionary(k => k, k => Serializer.SanitizeItem(session[k]));

            summary.Url = context.HttpContext.Request.RawUrl;

            return summary;
        }

        public static EnvironmentSummary CreateEnvironmentSummary(ExceptionContext context)
        {
            var appPath = context.HttpContext.Request.PhysicalApplicationPath;
            var username = context.HttpContext.User.Identity.Name;

            return EnvironmentSummary.CreateFromEnvironment(appPath, username);
        }
    }
}
