using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventHub.Data;

namespace EventHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var upcoming = DataStore.Events.Where(e => e.StartDate >= DateTime.Today).OrderBy(e => e.StartDate).ToList();
            ViewBag.UniversityCount = DataStore.Universities.Count;
            ViewBag.StateCount = DataStore.Universities.Select(u => u.State).Distinct().Count();
            ViewBag.EventCount = DataStore.Events.Count;
            ViewBag.Universities = DataStore.Universities.Take(8).ToList();
            return View(upcoming.Take(6).ToList());
        }
    }
}
