using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Data;
using project.Models;
using project.Services;
using project.ViewModels;

namespace project.Controllers;

public class CheckoutsController : Controller
{
    private readonly ILogger<CheckoutsController> _logger;
    private readonly ILibraryService _libraryService;

    public CheckoutsController(ILogger<CheckoutsController> logger, ILibraryService libraryService)
    {
        _logger = logger;
        _libraryService = libraryService;
    }

    public IActionResult Index()
    {
        CheckoutsViewModel model = new CheckoutsViewModel();
        model.Checkouts = _libraryService.GetAllCheckouts();

        return View(model);
    }

    [HttpPost]
    public IActionResult New(int memberId, int bookId)
    {
        _libraryService.CheckoutBookForMember(memberId, bookId);
        return Redirect(Request.Headers.Referer.FirstOrDefault());
    }

    [HttpPost]
    public IActionResult Checkin(int bookId)
    {
        _libraryService.CheckinBook(bookId);
        return Redirect(Request.Headers.Referer.FirstOrDefault());
    }
}