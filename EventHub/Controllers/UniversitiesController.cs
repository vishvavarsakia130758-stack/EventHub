using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventHub.Data;

namespace EventHub.Controllers
{
    public class UniversitiesController : Controller
    {
        public IActionResult Index(string state, string type, string q)
        {
            var list = DataStore.Universities.AsEnumerable();
            if (!string.IsNullOrEmpty(state)) list = list.Where(u => u.State == state);
            if (!string.IsNullOrEmpty(type)) list = list.Where(u => u.Type == type);
            if (!string.IsNullOrWhiteSpace(q))
                list = list.Where(u => u.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 || u.City.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);

            ViewBag.States = DataStore.Universities.Select(u => u.State).Distinct().OrderBy(x => x).ToList();
            ViewBag.Types = DataStore.Universities.Select(u => u.Type).Distinct().OrderBy(x => x).ToList();
            ViewBag.State = state; ViewBag.Type = type; ViewBag.Q = q;
            return View(list.OrderBy(u => u.Name).ToList());
        }

        public IActionResult Details(int id)
        {
            var u = DataStore.GetUniversity(id);
            if (u == null) return NotFound();
            ViewBag.Events = DataStore.Events.Where(e => e.UniversityId == id).OrderBy(e => e.StartDate).ToList();
            return View(u);
        }
    }
}
