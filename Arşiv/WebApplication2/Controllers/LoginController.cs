using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    { 
        
        if (Request.Cookies.ContainsKey("UserFirstName"))
        {
            return RedirectToAction("Index", "HomePage");
        }
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(string firstName, string lastName)
    {
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            Response.Cookies.Append("UserFirstName", firstName);
            Response.Cookies.Append("UserLastName", lastName);
            return RedirectToAction("Index", "HomePage");
        }

        ViewBag.Message = "Please enter both first and last name";
        return View("Index");

        
    }
}