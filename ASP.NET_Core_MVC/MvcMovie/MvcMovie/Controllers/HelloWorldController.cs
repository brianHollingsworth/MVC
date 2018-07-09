using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// 1.) Add a controller to an ASP.NET Core MVC app
//
// 2.3.)  A best practice: View templates should not perform business logic or
//        interact with a database directly. Rather, a view template should
//        work only with the data that's provided to it by the controller.
//        Maintaining this "separation of concerns" helps keep your code clean,
//        testable, and maintainable.
namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index()
        {
            // 2.1.) Replace the hard-coded string with a View object.
            //       It uses a view template to generate an HTML response to
            //       the browser.
            return View();
            //return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        // 1.1.) Modify the code to pass some parameter information from the URL
        //       to the controller. For example, /HelloWorld/Welcome?name=Brian&numtimes=42
        //       This URL uses the C# optional-parameter feature to indicate
        //       that the numTimes parameter defaults to 42 if no value is
        //       passed for that parameter.

        //public string Welcome(string name, int numTimes = 42)
        //{
        //    //return "This is the Welcome action method...";
        //    // The $ special character identifies a string literal as an interpolated string.
        //    return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        //}

        // 1.2.) Replace the Welcome method with the following code:
        //       This time the third URL segment matched the route parameter
        //       id.
        //       The Welcome method contains a parameter id that matched the
        //       URL template in the MapRoute method.
        //       The trailing ? (in id?) indicates the id parameter is optional.
        //public string Welcome(string name, int ID = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        //}

        // 2.3.) Rather than have the controller render this response as a
        //       string, change the controller to use a view template instead.
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
