using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index(string q, string category, string state, string mode, string sort)
        {
            var list = DataStore.Events.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(q))
                list = list.Where(e => e.Title.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                    || e.University.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                    || e.Description.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrEmpty(category)) list = list.Where(e => e.Category == category);
            if (!string.IsNullOrEmpty(state)) list = list.Where(e => e.University.State == state);
            if (!string.IsNullOrEmpty(mode)) list = list.Where(e => e.Mode == mode);

            switch (sort)
            {
                case "fee": list = list.OrderBy(e => e.Fee); break;
                case "seats": list = list.OrderByDescending(e => e.SeatsLeft); break;
                default: list = list.OrderBy(e => e.StartDate); break;
            }

            ViewBag.Categories = DataStore.Events.Select(e => e.Category).Distinct().OrderBy(x => x).ToList();
            ViewBag.States = DataStore.Universities.Select(u => u.State).Distinct().OrderBy(x => x).ToList();
            ViewBag.Q = q; ViewBag.Category = category; ViewBag.State = state; ViewBag.Mode = mode; ViewBag.Sort = sort;
            return View(list.ToList());
        }

        public IActionResult Details(int id)
        {
            var ev = DataStore.GetEvent(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpGet]
        public IActionResult Register(int id)
        {
            var ev = DataStore.GetEvent(id);
            if (ev == null) return NotFound();
            if (ev.IsClosed) return RedirectToAction("Details", new { id = id });
            ViewBag.Event = ev;
            var prefill = new Registration { EventId = id, Year = 1 };
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                prefill.FullName = User.Identity.Name;
                prefill.Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }
            return View(prefill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Registration model)
        {
            var ev = DataStore.GetEvent(model.EventId);
            if (ev == null) return NotFound();
            ViewBag.Event = ev;

            if (!ModelState.IsValid) return View(model);

            string error = DataStore.TryRegister(model);
            if (error != null)
            {
                ModelState.AddModelError("", error);
                return View(model);
            }
            return RedirectToAction("Success", new { code = model.Code });
        }

        public IActionResult Success(string code)
        {
            var reg = DataStore.Registrations.FirstOrDefault(r => r.Code == code);
            if (reg == null) return NotFound();
            ViewBag.Event = DataStore.GetEvent(reg.EventId);
            return View(reg);
        }
    }
}
