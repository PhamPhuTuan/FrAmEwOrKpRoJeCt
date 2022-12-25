using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrameworkProject.Models;
using firstWeb.Models;

namespace FrameworkProject.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Plants()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(firstWeb.Models.StoreContext)) as StoreContext;

            return View(context.GetProducts());
        }

    }
}
