using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Text.Json;

namespace WebApplication2.Controllers;

public class HomePageController : Controller
{
    private readonly ILogger<HomePageController> _logger;

    public HomePageController(ILogger<HomePageController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
       
       if (HttpContext.Session.GetString("Movies") == null)
       {
           List<Movie> movies = new List<Movie>
           {
               new Movie { MovieId = 1, Title = "X-Men", Director = "Brett Ratner", Players = new string[] {"Patrick Stewart", "Hugh Jackman" ,"Halle Berry"}, ReleaseYear = 2006, ImageUrl = "/images/xmen11.jpeg" , Rating = 4.3},
               new Movie { MovieId = 2, Title = "Spider-Man 2", Director = "Sam Raimi", Players = new string[] {"Tobey Magurie", "Kirsten Dunst" ,"Alfred Molina"}, ReleaseYear = 2004, ImageUrl = "/images/spiderman2.jpeg" , Rating = 3.0},
               new Movie { MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", Players = new string[] {"Tobey Magurie", "Kirsten Dunst" ,"Topher Grace"}, ReleaseYear = 2007, ImageUrl = "/images/spiderman3.jpeg" , Rating = 4.1},          
               new Movie { MovieId = 5, Title = "Gladiator ", Director = "Ridley Scott", Players = new string[] {"Russel Crowe", "Joaquin Phoenix" , "Connie Nielsen"}, ReleaseYear = 2000, ImageUrl = "/images/gladiator.jpg", Rating = 3.4},
               new Movie { MovieId = 4, Title = "Valkryie ", Director = "Bryan Singer", Players = new string[] {"Tom Cruise", "Bill Nighy" , "Carice van Houten"}, ReleaseYear = 2008, ImageUrl = "/images/valkyrie.jpeg" , Rating = 4.5},  
            };
           HttpContext.Session.SetString("Movies", JsonSerializer.Serialize(movies));
       }
       List<Movie> movieList = JsonSerializer.Deserialize<List<Movie>>(HttpContext.Session.GetString("Movies"));

        
       return View(movieList);
        
    }
    [HttpPost]
    public IActionResult Logout(string returnUrl = null)
    {
        // Clear cookies
        Response.Cookies.Delete("UserFirstName");
        Response.Cookies.Delete("UserLastName");

        // Clear session
        HttpContext.Session.Clear();

        // Set cache headers
        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "-1";

        // Redirect back to the same page or home
        if (!string.IsNullOrEmpty(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        return RedirectToAction("Index", "HomePage");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    
}