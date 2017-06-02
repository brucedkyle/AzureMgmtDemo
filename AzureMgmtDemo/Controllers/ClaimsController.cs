// Example updated to support ASP.NET CORE
// Originally from https://docs.microsoft.com/en-us/azure/active-directory/develop/guidedsetups/active-directory-aspnetwebapp was for ASP.NET

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims; 

namespace AzureMgmtDemo.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        /// <summary>
        /// Add user's claims to viewbag
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            ViewBag.Name = User.Identity.Name;
            ViewBag.UserName = "Todo";
            ViewBag.Subject = "Todo";
            ViewBag.TenantId = User.Claims.FirstOrDefault<Claim>(c => c.Type.Equals("http://schemas.microsoft.com/identity/claims/tenantid")).Value;
            /*
             * How to do this with ASP.NET
            var claimsPrincipalCurrent = System.Security.Claims.ClaimsPrincipal.Current;

            //You get the user’s first and last name below:
            ViewBag.Name = claimsPrincipalCurrent.FindFirst("name").Value;

            // The 'preferred_username' claim can be used for showing the username
            ViewBag.Username = claimsPrincipalCurrent.FindFirst("preferred_username").Value;

            // The subject claim can be used to uniquely identify the user across the web
            ViewBag.Subject = claimsPrincipalCurrent.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            // TenantId is the unique Tenant Id - which represents an organization in Azure AD
            ViewBag.TenantId = claimsPrincipalCurrent.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            */

            return View();
        }

    }
}