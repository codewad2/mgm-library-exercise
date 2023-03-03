using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Data;
using project.Models;
using project.Services;
using project.ViewModels;

namespace project.Controllers;

public class MembersController : Controller
{
    private readonly ILogger<MembersController> _logger;
    private readonly ILibraryService _libraryService;

    public MembersController(ILogger<MembersController> logger, ILibraryService libraryService)
    {
        _logger = logger;
        _libraryService = libraryService;
    }

    //public IActionResult Index()
    //{
    //    MembersViewModel model = new MembersViewModel();
    //    model.Members = _libraryService.GetAllMembers();

    //    return View(model);
    //}

    [HttpGet]
    public IActionResult Details(int id)
    {
        MemberDetailsViewModel model = new MemberDetailsViewModel();
        model.Member = _libraryService.GetMember(id);

        return View(model);
    }

    //[HttpGet]
    //public IActionResult Search(string? searchTerms)
    //{
    //    IEnumerable<Member> model = String.IsNullOrEmpty(searchTerms) ? _libraryService.GetAllMembers() : _libraryService.GetMembers(searchTerms);
    //    return PartialView("/Views/Partials/_Members.cshtml", model);
    //}
}