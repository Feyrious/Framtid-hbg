using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Framtid_hbg.Website.Models;
using Framtid_hbg.Website.Service;
using Framtid_hbg.Website.Service.Interface;
using Framtid_hbg.Website.Service.NotifyService;

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
    [ValidateAntiForgeryToken]
    [Route("Contact")]
    public IActionResult Contact(ContactViewModel model)
    {
        // Check model so it contains data
        if (model.Email == null || model.ContactType == null || model.Message == null)
            return View();

        // Prepare the contact form in to an email
        var notifyMessage = new NotifyMessage().PrepareContentFrom(model);
        var emailMessage = _notifyService.PrepareEmailFrom(notifyMessage);
        
        // Try and send the email
        var isSuccess = _notifyService.SendMessage(emailMessage);

        // Returns the result of sending the message to the user
        var contactEmail = Environment.GetEnvironmentVariable("");
        TempData["result"] = isSuccess.ToString().ToLower();
        TempData["message"] = isSuccess ? 
            "Vi har mottagit ditt meddelande, vi återkommer så snart vi kan!" : 
            "Vi hade problem att skicka meddelandet, vänligen prova på: " + contactEmail;

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