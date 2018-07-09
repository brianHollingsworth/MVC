using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

// 3.2.) "New Scaffolded Item..." creates:
//        - An Entity Framework Core database context class (Data/MvcMovieContext.cs)
//        - A movies controller(Controllers/MoviesController.cs)
//        - Razor view files for Create, Delete, Details, Edit, and Index pages(Views/Movies/*.cshtml)
//        - The automatic creation of the database context and CRUD (create, read, update, and delete) action methods and views is known as scaffolding.
namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        // 3.4.) The constructor uses Dependency Injection to inject the
        //       database context (MvcMovieContext) into the controller. The
        //       database context is used in each of the CRUD methods in the
        //       controller.
        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel();

            // 6.1.) The SelectList of genres is created by projecting the
            //       distinct genres (we don't want our select list to have
            //       duplicate genres).
            movieGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.movies = await movies.ToListAsync();

            return View(movieGenreVM);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Movie.ToListAsync());
        //}
        // 6.1.) By changing the parameter from 'searchString' to 'id', we can
        //       pass the search title as route data (a URL segment) instead of
        //       as a query string value: https://localhost:44322/Movies/Index/Brian
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    // 6.1.) The first line of the Index action method creates a LINQ
        //    //       query to select the movies. The query is ONLY defined at
        //    //       this point, it has not been run against the database.
        //    var movies = from m in _context.Movie
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        // 6.1.) The s => s.Title.Contains() is a Lambda Expression.
        //        //       Lambdas are used in method-based LINQ queries as
        //        //       arguments to standard query operator methods such as
        //        //       the Where method or Contains.
        //        // 6.1.) Navigate to /Movies/Index. Append a query string such
        //        //       as ?searchString=Brian to the URL. The filtered movies are
        //        //       displayed.
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //    }

        //    return View(await movies.ToListAsync());
        //}

        // 6.1.) 
        //[HttpPost]
        //public string Index(string searchString, bool notUsed)
        //{
        //    return "From [HttpPost]Index: filter on " + searchString;
        //}

        // GET: Movies/Details/5
        // 3.5.) The id parameter is generally passed as route data. For example http://localhost:5000/movies/details/1 sets:
        //       - The controller to the movies controller(the first URL segment).
        //       - The action to details(the second URL segment).
        //       - The id to 1 (the last URL segment).
        //       You can also pass in the id with a query string as follows: http://localhost:1234/movies/details?id=1
        // 5.1.) Modifying data in an HTTP GET method is a security risk.
        //       Modifying data in an HTTP GET method also violates HTTP best
        //       practices and the architectural REST pattern, which specifies
        //       that GET requests shouldn't change the state of your
        //       application.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 9.1.) EF makes it easy to search for data using the 
            //       SingleOrDefaultAsync method. An important security feature
            //       built into the method is that the code verifies that the
            //       search method has found a movie before it tries to do
            //       anything with it. 
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 5.1.) The [Bind] attribute is one way to protect against over-
        //       posting. You should only include properties in the [Bind]
        //       attribute that you want to change.
        // 5.1.) [ValidateAntiForgeryToken] Cross-site request forgery (also
        //       known as XSRF or CSRF, pronounced see-surf) is an attack
        //       against web-hosted apps whereby a malicious web app can
        //       influence the interaction between a client browser and a web
        //       app that trusts that browser.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            // 5.1.) The ModelState.IsValid method verifies that the data
            //       submitted in the form can be used to modify (edit or
            //       update) a Movie object.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        // 9.1.) The common language runtime (CLR) requires overloaded methods
        //       to have a unique parameter signature (same method name but
        //       different list of parameters). However, here you need two
        //       Delete methods -- one for GET and one for POST -- that both
        //       have the same parameter signature.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
