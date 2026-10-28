using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public class Enrollment
{
    [Key]
    public int EnrollmentID { get; set; }

    [Display(Name = "Course")]
    public int CourseID { get; set; }

    [Display(Name = "Student")]
    public int StudentID { get; set; }

    [Range(typeof(decimal), "0", "4")]
    public decimal? Grade { get; set; }

    public Course? Course { get; set; }
    public Student? Student { get; set; }
}
