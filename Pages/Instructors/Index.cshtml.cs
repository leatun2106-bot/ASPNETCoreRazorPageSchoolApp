using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;
    public IndexModel(SchoolContext context) => _context = context;
    public IList<Instructor> Items { get; set; } = new List<Instructor>();

    public async Task OnGetAsync()
    {
        var query = _context.Instructors.AsQueryable();
        Items = await query.AsNoTracking().ToListAsync();
    }
}
