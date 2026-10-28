using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Courses;

public class EditModel : PageModel
{
    private readonly SchoolContext _context;
    public EditModel(SchoolContext context) => _context = context;
    [BindProperty] public Course Item { get; set; } = new();
    public SelectList DepartmentOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var record = await _context.Courses.FindAsync(id);
        if (record == null) return NotFound();
        Item = record;
        DepartmentOptions = new SelectList(await _context.Departments.OrderBy(d => d.Name).ToListAsync(), "DepartmentID", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) {
        DepartmentOptions = new SelectList(await _context.Departments.OrderBy(d => d.Name).ToListAsync(), "DepartmentID", "Name");
            return Page();
        }
        _context.Attach(Item).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) {
            if (!await _context.Courses.AnyAsync(x => x.CourseID == Item.CourseID)) return NotFound();
            throw;
        }
        return RedirectToPage("Index");
    }
}
