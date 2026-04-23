namespace TrainingCenter.Entities;
public class Instructor
{
  public int InstructorId { get; set; }
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public DateOnly HireDate { get; set; }
  public decimal Salary { get; set; }
  public int? ManagerId { get; set; }
  public bool IsActive { get; set; }
  public virtual ICollection<Course> Courses { get; set; }
    = new List<Course>();
  // group that this instructor manages
  public virtual ICollection<Instructor> ManagedInstructors { get; set; }
    = new List<Instructor>();
  // this instructor's manager
  public virtual Instructor? Manager { get; set; }
}
