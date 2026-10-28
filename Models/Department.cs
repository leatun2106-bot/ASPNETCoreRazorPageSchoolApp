using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public class Department
{
    [Key]
    public int DepartmentID { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
