using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfCoreExempel.Models
{
    public class Teacher
    {
        public int Id { get; set; } // PK. Den identifieras automatiskt av Entity Framework tack vare namnet.

        [Required] // Följande property får inte vara null.
        [StringLength(20)] // Följande property får vara max 20 tecken.
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        // Teacher har en många-till-många-relation med Course.
        // En kurs kan ha flera lärare, och en lärare kan ha flera kurser.
        // I detta fall har både Course och Teacher har en navigation collection som pekar på varandra, så kopplingstabellen kommer att autogenereras.
        public ICollection<Course> Courses { get; set; } = new List<Course>();



        // Ej nödvändig, endast för demo.
        public override string ToString()
        {
            return $"Student: Id: {Id}, Name: {FirstName} {LastName}";
        }
    }
}
