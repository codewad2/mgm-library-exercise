using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Data;
using project.Models;
using project.Services;
using project.ViewModels;

namespace project.Controllers;

public class BooksController : Controller
{
    private readonly ILogger<BooksController> _logger;
    private readonly ILibraryService _libraryService;

    public BooksController(ILogger<BooksController> logger, ILibraryService libraryService)
    {
        _logger = logger;
        _libraryService = libraryService;
    }

    public IActionResult Index()
    {
        BooksViewModel model = new BooksViewModel();
        model.Books = _libraryService.GetAllBooks();

        return View(model);
    }

    [HttpGet]
    public IActionResult Search(string? searchTerms)
    {
        IEnumerable<Book> model = String.IsNullOrEmpty(searchTerms) ? _libraryService.GetAllBooks() : _libraryService.GetBooks(searchTerms);
        return PartialView("/Views/Partials/_Books.cshtml", model);
    }
}