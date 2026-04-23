namespace TrainingCenter.Entities;
public class StudentProfile
{
  public int ProfileId { get; set; }
  public string? Address { get; set; } 
  public string? City { get; set; } 
  public string? Country { get; set; } 
  public string? Bio { get; set; } 
  public string? LinkedInUrl { get; set; } 
  public virtual Student Student { get; set; } = null!;
}
