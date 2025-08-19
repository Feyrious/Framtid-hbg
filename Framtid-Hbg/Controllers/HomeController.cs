using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Framtid_hbg.Website.Models;

namespace Framtid_hbg.Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("Service")]
    public IActionResult Service()
    {
        return View();
    }
    
    [Route("Location")]
    public IActionResult Location()
    {
        return View();
    }

    [Route("Contact")]
    public IActionResult Contact()
    {
        var model = new ContactViewModel();
        return View(model);
    }
    
    [HttpPost]
    [Route("Contact")]
    public IActionResult Contact(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            var message = model.Message;
        }

        return View();
    }
    
    [Route("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult PageBanner()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}