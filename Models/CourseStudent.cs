namespace EfCoreExempel.Models
{
    public class CourseStudent
    {
        // PK skapas i Context.OnModelCreating som en kompositnyckel av de båda FK.

        public int CourseId { get; set; } // Reference property som blir FK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public Course Course { get; set; } // Navigation property.
        public int StudentId { get; set; } // Reference property som blir FK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public Student Student { get; set; } // Navigation property.



        // Ej nödvändig, endast för demo.
        public override string ToString()
        {
            return $"CourseStudent: CourseId: {CourseId}, StudentId: {StudentId}";
        }
    }
}
