using System.Collections.Generic;

namespace EfCoreExempel.Models
{
    public class Student
    {
        public int Id { get; set; } // PK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Student har en en-till-många relation med Address.
        // En student kan bara ha en adress, men en adress kan ha flera studenter.
        // Student har därför en FK till Address, medan Address har en samling med studenter som den kan tillhöra.
        public int AddressId { get; set; } // Reference property som blir FK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public Address Address { get; set; } // Navigation property.

        // Student har en många-till-många-relation med Course.
        // En student kan ha flera kurser, och en kurs kan ha flera studenter.
        // I detta fall skapats kopplingstabellen explicit och då pekar båda klassernas navigation collection på den.
        public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>(); // Navigation collection.

        // Student har en en-till-en-relation med Card.
        // En student kan bara ha ett kort, och varje kort kan bara ha en student.
        // Student är i detta fall "Principal" (parent). Den har då bara en navigation property till Card,
        // men Card som är "dependent" (child) har både FK och navigation property till Student.
        // En student kan existera utan ett kort, men ett kort måste ha en student.
        public Card Card { get; set; } // Navigation property.



        // Ej nödvändig, endast för demo.
        public override string ToString()
        {
            return $"Student: Id: {Id}, Name: {FirstName} {LastName}";
        }
    }
}
