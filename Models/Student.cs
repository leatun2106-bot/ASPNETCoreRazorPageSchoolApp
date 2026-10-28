using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public class Student
{
    [Key]
    public int StudentID { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
