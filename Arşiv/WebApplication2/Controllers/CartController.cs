using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Text.Json;

namespace WebApplication2.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {   
        List<int> cart = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("Cart") ?? "[]");
        List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(HttpContext.Session.GetString("Movies"));

        var cartMovies = movies.Where(m => cart.Contains(m.MovieId)).ToList();
            
        if (cartMovies.Count == 0)
        {
            ViewBag.Message = "you have not  movie in cart!";
        }

        return View(cartMovies);

        
    }
}