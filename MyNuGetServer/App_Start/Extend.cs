using System.Web;
using NuGet.Server;

namespace MyNuGetServer
{
    public static class Extend
    {
        public static T Get<T>(this IServiceResolver serviceResolver) where T : class
        {
            return serviceResolver.Resolve(typeof(T)) as T;
        }

        public static bool IsLogin(this HttpContext httpContext)
        {
            return httpContext.Session["IsLogin"] != null;
        }

        public static void Login(this HttpContext httpContext)
        {
            httpContext.Session["IsLogin"] = "Yes";
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.Session["IsLogin"] = null;
        }
    }
}