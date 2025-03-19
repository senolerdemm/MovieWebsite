using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Text.Json;


namespace WebApplication2.Controllers;

public class MovieController : Controller
{
    // GET
  
    public IActionResult MovieInfo(int? id)
    { 
        if (id == null)
        {
            ViewBag.Message = "Please Specify Movie Id!";
            return View();
        }
        string moviesJson = HttpContext.Session.GetString("Movies");
        if(moviesJson == null)
        {
            ViewBag.Message = "Please specify movie ID!";
            return View("Error");
        }

        List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(moviesJson);
        Movie selectedMovie = movies.FirstOrDefault(m => m.MovieId == id);

        if (selectedMovie == null)
        {
            ViewBag.Message = "Invalid Movie ID!";
            return View("Error");
        }

        return View(selectedMovie);
    }

    public IActionResult AddToCart(int id)
    {
        List<int> cart = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("Cart") ?? "[]");

        if (!cart.Contains(id))
        {
            cart.Add(id);
            TempData[$"Message_{id}"] = "✅ Movie successfully added to cart!";
        }else{
            TempData[$"Message_{id}"] = "⚠️ Movie already in cart!";
        }
            
        

        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));


        return RedirectToAction("Index", "HomePage"); 

        
    }
}