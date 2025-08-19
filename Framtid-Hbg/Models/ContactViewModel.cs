using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Framtid_hbg.Website.Models;

public class ContactViewModel
{
    [BindProperty]
    public string? ContactType { get; set; }
    
    [BindProperty]
    public string? Name { get; set; }
    
    [BindProperty]
    public string? Email { get; set; }
    
    [BindProperty]
    public int PhoneNumber { get; set; }
    
    [BindProperty]
    public string? Adress { get; set; }
    
    [BindProperty]
    public string? Message { get; set; }
}