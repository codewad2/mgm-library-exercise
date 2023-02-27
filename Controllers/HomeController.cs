using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Data;
using project.Models;
using project.Services;
using project.ViewModels;

namespace project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILibraryService _libraryService;

    public HomeController(ILogger<HomeController> logger, ILibraryService libraryService)
    {
        _logger = logger;
        _libraryService = libraryService;
    }

    public IActionResult Index()
    {
        HomeViewModel model = new HomeViewModel();
        model.Members = _libraryService.GetAllMembers();
        model.Books = _libraryService.GetBooks(null);

        return View(model);
    }

    [HttpPost]
    public IActionResult CheckoutBookForMember(int memberId, int bookId)
    {
        return _libraryService.CheckoutBookForMember(memberId, bookId) ?
            new OkResult() :
            new BadRequestResult();
    }

    [HttpPost]
    public IActionResult CheckinBook(int bookId)
    {
        return _libraryService.CheckinBook(bookId) ?
            new OkResult() :
            new BadRequestResult();
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
