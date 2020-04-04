using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using NuGet;
using NuGet.Server;
using NuGet.Server.Core.Infrastructure;

namespace MyNuGetServer.Controllers
{
    public class PackageController : ApiController
    {
        private readonly IServerPackageRepository _repository;

        public PackageController()
        {
            _repository = ServiceResolver.Current.Get<IServerPackageRepository>();
        }

        [HttpPost]
        public string Delete(string id, string version)
        {
            if (!HttpContext.Current.IsLogin()) return "尚未登录或者登录超时";
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                if (_repository.RemovePackageAsync(id, new SemanticVersion(version), tokenSource.Token).Wait(3000))
                {
                    return "";
                }
                tokenSource.Cancel();
                return "删除超时";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}