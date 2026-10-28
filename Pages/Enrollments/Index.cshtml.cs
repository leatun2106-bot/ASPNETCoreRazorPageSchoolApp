using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Enrollments;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;
    public IndexModel(SchoolContext context) => _context = context;
    public IList<Enrollment> Items { get; set; } = new List<Enrollment>();

    public async Task OnGetAsync()
    {
        var query = _context.Enrollments.AsQueryable();
        query = query.Include(x => x.Course).Include(x => x.Student);
        Items = await query.AsNoTracking().ToListAsync();
    }
}
