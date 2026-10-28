using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Courses;

public class CreateModel : PageModel
{
    private readonly SchoolContext _context;
    public CreateModel(SchoolContext context) => _context = context;
    [BindProperty] public Course Item { get; set; } = new();
    public SelectList DepartmentOptions { get; set; } = default!;

    public async Task OnGetAsync()
    {
        DepartmentOptions = new SelectList(await _context.Departments.OrderBy(d => d.Name).ToListAsync(), "DepartmentID", "Name");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
        DepartmentOptions = new SelectList(await _context.Departments.OrderBy(d => d.Name).ToListAsync(), "DepartmentID", "Name");
            return Page();
        }
        _context.Courses.Add(Item);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
