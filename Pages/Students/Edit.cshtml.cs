using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Students;

public class EditModel : PageModel
{
    private readonly SchoolContext _context;
    public EditModel(SchoolContext context) => _context = context;
    [BindProperty] public Student Item { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var record = await _context.Students.FindAsync(id);
        if (record == null) return NotFound();
        Item = record;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) {

            return Page();
        }
        _context.Attach(Item).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) {
            if (!await _context.Students.AnyAsync(x => x.StudentID == Item.StudentID)) return NotFound();
            throw;
        }
        return RedirectToPage("Index");
    }
}
