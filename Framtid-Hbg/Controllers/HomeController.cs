using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service;
using Framtid_hbg.Website.Service.Interface;

namespace Framtid_hbg.Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INotifyService _notifyService;

    public HomeController(ILogger<HomeController> logger,  INotifyService notifyService)
    {
        _logger = logger;
        _notifyService = notifyService;
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
        if (model.Email == null || model.ContactType == null || model.Message == null)
            return View();

        var emailMessage = new EmailMessage();
        emailMessage.PrepareMessage(model);
        
        var isSuccess = _notifyService.SendMessage(emailMessage);

        TempData["result"] = isSuccess.ToString().ToLower();
        TempData["message"] = isSuccess ? 
            "Vi har mottagit ditt meddelande, vi återkommer så snart vi kan!" : 
            "Vi hade problem att skicka meddelandet, vänligen prova på: noreply@test.se";

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