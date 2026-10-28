using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public class Course
{
    [Key]
    public int CourseID { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Credits { get; set; }

    [Display(Name = "Department")]
    public int DepartmentID { get; set; }

    public Department? Department { get; set; }
}
