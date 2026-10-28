using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Enrollments;

public class DetailsModel : PageModel
{
    private readonly SchoolContext _context;
    public DetailsModel(SchoolContext context) => _context = context;
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
}
