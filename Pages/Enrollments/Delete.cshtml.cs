using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Enrollments;

public class DeleteModel : PageModel
{
    private readonly SchoolContext _context;
    public DeleteModel(SchoolContext context) => _context = context;
    public Enrollment Item { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var record = await _context.Enrollments.Include(x => x.Course).Include(x => x.Student).AsNoTracking()
            .FirstOrDefaultAsync(x => x.EnrollmentID == id);
        if (record == null) return NotFound();
        Item = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var record = await _context.Enrollments.FindAsync(id);
        if (record == null) return RedirectToPage("Index");
        _context.Enrollments.Remove(record);
        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException) {
            ModelState.AddModelError(string.Empty, "This record is referenced by another record and cannot be deleted. Delete or reassign dependent records first.");
            Item = record;
            return Page();
        }
        return RedirectToPage("Index");
    }
}
