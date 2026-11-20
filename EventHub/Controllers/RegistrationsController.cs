using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventHub.Data;

namespace EventHub.Controllers
{
    public class RegistrationsController : Controller
    {
        // "My registrations": look up by the email used while registering.
        public IActionResult Index(string email)
        {
            if (string.IsNullOrWhiteSpace(email) && User.Identity != null && User.Identity.IsAuthenticated)
                email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            ViewBag.Email = email;
            if (string.IsNullOrWhiteSpace(email)) return View();
            var list = DataStore.Registrations
                .Where(r => r.Email.ToLower() == email.Trim().ToLower())
                .OrderByDescending(r => r.RegisteredOn).ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(string code, string email)
        {
            bool ok = DataStore.Cancel(code, email);
            TempData["Msg"] = ok ? "Registration " + code + " cancelled." : "Could not cancel that registration.";
            return RedirectToAction("Index", new { email = email });
        }
    }
}
