using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Departments;

public class CreateModel : PageModel
{
    private readonly SchoolContext _context;
    public CreateModel(SchoolContext context) => _context = context;
    [BindProperty] public Department Item { get; set; } = new();

    public async Task OnGetAsync()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {

            return Page();
        }
        _context.Departments.Add(Item);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
