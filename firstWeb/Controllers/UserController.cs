using firstWeb.Models;
using FrameworkProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkProject.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string mess)
        {
            Users USERGLOBAL = firstWeb.Program.USERGLOBAL;
            if (USERGLOBAL.USERNAME != null)
            {
                ViewData["u"] = firstWeb.Program.USERGLOBAL.USERNAME ?? "A";
            }
            if (mess != null)
            {
                ViewData["Message"] = mess;
            }
            else
            {
                ViewData["Message"] = "";

            }
            return View();
        }
        public ActionResult Regis(Users u)
        {
            firstWeb.Program.USERGLOBAL = u;
            @ViewData["user"] = firstWeb.Program.USERGLOBAL.USERNAME;
            @ViewData["pass"] = firstWeb.Program.USERGLOBAL.PASSWORD;
            @ViewData["role"] = firstWeb.Program.USERGLOBAL.ROLEID;
            return View();
        }

        public ActionResult Profile()
        {
            Users u = firstWeb.Program.USERGLOBAL;
            @ViewData["user"] = u.USERNAME;
            @ViewData["pass"] = u.PASSWORD;
            @ViewData["role"] = u.ROLEID;
            return View();
        }

        public ActionResult Load(Users u)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(firstWeb.Models.StoreContext)) as StoreContext;
            if (u != null)
            {
                Users a = context.LogIn(u);
                if (a.EMAIL == null)
                {
                    string mess = "Account does not exist or wrong password.";
                    return RedirectToAction("Login", new { mess});
                }
                else
                {
                    firstWeb.Program.USERGLOBAL = a;
                    return RedirectToAction("Home", "Products");
                }
            }
            else
            {
                string mess = "";
                return RedirectToAction("Login", new { mess});
            }
            ;
        }
    }
}
