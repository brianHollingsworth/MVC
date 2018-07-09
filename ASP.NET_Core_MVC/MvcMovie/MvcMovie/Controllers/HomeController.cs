using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

// Controllers: Classes that handle browser requests.
// They retrieve model data and call view templates that return a response.
// In an MVC app, the view only displays information; the controller handles and responds to user input and interaction.
// For example, the controller handles route data and query-string values, and passes these values to the model.
// The model might use these values to query the database.
// For example, http://localhost:1234/Home/About has route data of Home (the controller) and About (the action method to call on the home controller).
// http://localhost:1234/Movies/Edit/5 is a request to edit the movie with ID=5 using the movie controller.
// The default URL routing logic used by MVC uses the following format to determine what code to invoke: /[Controller]/[ActionName]/[Parameters]
namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        // When you run the app and don't supply any URL segments, it defaults to the "Home" controller and the "Index" method.
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
