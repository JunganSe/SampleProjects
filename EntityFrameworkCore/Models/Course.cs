using System.Collections.Generic;

namespace EfCoreExempel.Models
{
    public class Course
    {
        public int Id { get; set; } // PK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public string Name { get; set; }

        // Course har en många-till-många-relation med Student.
        // En kurs kan ha flera studenter, och en student kan ha flera kurser.
        // I detta fall har kopplingstabellen skapats manuellt, så båda klassernas navigation collection pekar på den.
        public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>(); // Navigation collection.

        // Course har en många-till-många-relation med Teacher.
        // En kurs kan ha flera lärare, och en lärare kan ha flera kurser.
        // I detta fall har både Course och Teacher har en navigation collection som pekar på varandra, så kopplingstabellen kommer att autogenereras.
        // Kopplingstabellen ska inte skapas manuellt i detta läge.
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();



        // Ej nödvändig, endast för demo.
        public override string ToString()
        {
            return $"Course: Id: {Id}, name: {Name}";
        }
    }
}
