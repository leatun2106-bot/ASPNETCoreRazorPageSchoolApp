using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Enrollments;

public class CreateModel : PageModel
{
    private readonly SchoolContext _context;
    public CreateModel(SchoolContext context) => _context = context;
    [BindProperty] public Enrollment Item { get; set; } = new();
    public SelectList CourseOptions { get; set; } = default!;
    public SelectList StudentOptions { get; set; } = default!;

    public async Task OnGetAsync()
    {
        CourseOptions = new SelectList(await _context.Courses.OrderBy(c => c.Title).ToListAsync(), "CourseID", "Title");
        StudentOptions = new SelectList(await _context.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(s => new { s.StudentID, FullName = s.FirstName + " " + s.LastName }).ToListAsync(), "StudentID", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
        CourseOptions = new SelectList(await _context.Courses.OrderBy(c => c.Title).ToListAsync(), "CourseID", "Title");
        StudentOptions = new SelectList(await _context.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(s => new { s.StudentID, FullName = s.FirstName + " " + s.LastName }).ToListAsync(), "StudentID", "FullName");
            return Page();
        }
        _context.Enrollments.Add(Item);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
