﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Net.WebUI.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowDesigns(string no)
        {

            return View();
        }

    }
}
