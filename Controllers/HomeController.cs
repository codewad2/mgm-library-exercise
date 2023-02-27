using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models;
using project.ViewModels;

namespace project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LibraryContext _context;

    public HomeController(ILogger<HomeController> logger, LibraryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        HomeViewModel model = new HomeViewModel();
        model.Members = _context.Members.ToList();
        model.Books = _context.Books.ToList();

        return View(model);
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
