using System.Configuration;
using System.Web.Mvc;

namespace MyNuGetServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!System.Web.HttpContext.Current.IsLogin()) return Redirect("/home/login");
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string password)
        {
            if (password == ConfigurationManager.AppSettings["adminPwd"])
            {
                System.Web.HttpContext.Current.Login();
                return Redirect("/home/index");
            }

            ViewBag.Message = "密码错误";
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Logout();
            return Redirect("/home/login");
        }

    }
}