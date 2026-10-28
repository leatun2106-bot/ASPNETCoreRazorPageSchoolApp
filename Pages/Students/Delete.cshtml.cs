using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Students;

public class DeleteModel : PageModel
{
    private readonly SchoolContext _context;
    public DeleteModel(SchoolContext context) => _context = context;
    public Student Item { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var record = await _context.Students.AsNoTracking()
            .FirstOrDefaultAsync(x => x.StudentID == id);
        if (record == null) return NotFound();
        Item = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var record = await _context.Students.FindAsync(id);
        if (record == null) return RedirectToPage("Index");
        _context.Students.Remove(record);
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
