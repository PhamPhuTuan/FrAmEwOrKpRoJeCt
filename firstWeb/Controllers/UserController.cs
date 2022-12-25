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

        public ActionResult Login()
        {
            Users uSERGLOBAL = firstWeb.Program.USERGLOBAL;
            if (uSERGLOBAL.USERNAME != null)
            {
                ViewData["u"] = firstWeb.Program.USERGLOBAL.USERNAME ?? "A";
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

        public ActionResult profile()
        {
            Users u = firstWeb.Program.USERGLOBAL;
            @ViewData["user"] = u.USERNAME;
            @ViewData["pass"] = u.PASSWORD;
            @ViewData["role"] = u.ROLEID;
            return View();
        }
    }
}
