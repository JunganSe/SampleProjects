using System.Collections.Generic;

namespace EfCoreExempel.Models
{
    public class Address
    {
        public int Id { get; set; } // PK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Address har en en-till-många relation med Student.
        // En student kan bara ha en adress, men en adress kan ha flera studenter.
        // Address har en samling med studenter som den kan tillhöra, och Student har en FK till Address.
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
