using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkProject.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Package()
        {

            return View();
        }

        public IActionResult Succeed()
        {
            return View();
        }
    }
}
