using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Pages.Students;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;
    public IndexModel(SchoolContext context) => _context = context;
    public IList<Student> Items { get; set; } = new List<Student>();

    public async Task OnGetAsync()
    {
        var query = _context.Students.AsQueryable();
        Items = await query.AsNoTracking().ToListAsync();
    }
}
