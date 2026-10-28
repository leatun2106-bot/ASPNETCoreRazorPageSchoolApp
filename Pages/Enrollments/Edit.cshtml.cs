using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Enrollments;

public class EditModel : PageModel
{
    private readonly SchoolContext _context;
    public EditModel(SchoolContext context) => _context = context;
    [BindProperty] public Enrollment Item { get; set; } = new();
    public SelectList CourseOptions { get; set; } = default!;
    public SelectList StudentOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var record = await _context.Enrollments.FindAsync(id);
        if (record == null) return NotFound();
        Item = record;
        CourseOptions = new SelectList(await _context.Courses.OrderBy(c => c.Title).ToListAsync(), "CourseID", "Title");
        StudentOptions = new SelectList(await _context.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(s => new { s.StudentID, FullName = s.FirstName + " " + s.LastName }).ToListAsync(), "StudentID", "FullName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) {
        CourseOptions = new SelectList(await _context.Courses.OrderBy(c => c.Title).ToListAsync(), "CourseID", "Title");
        StudentOptions = new SelectList(await _context.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .Select(s => new { s.StudentID, FullName = s.FirstName + " " + s.LastName }).ToListAsync(), "StudentID", "FullName");
            return Page();
        }
        _context.Attach(Item).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) {
            if (!await _context.Enrollments.AnyAsync(x => x.EnrollmentID == Item.EnrollmentID)) return NotFound();
            throw;
        }
        return RedirectToPage("Index");
    }
}
