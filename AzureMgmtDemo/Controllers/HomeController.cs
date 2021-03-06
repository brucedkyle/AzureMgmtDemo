﻿// Example based on https://github.com/Azure-Samples/active-directory-dotnet-webapp-openidconnect-aspnetcore-v2
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AzureMgmtDemo.Controllers
{
    // TO DO Secure app pages, See https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data
    // [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
