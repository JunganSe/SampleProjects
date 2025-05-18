namespace EfCoreExempel.Models
{
    public class Card
    {
        public int Id { get; set; } // PK. Den identifieras automatiskt av Entity Framework tack vare namnet.

        // Card har en en-till-en-relation med Student.
        // En student kan bara ha ett kort, och varje kort kan bara ha en student.
        // Student är i detta fall "Principal" (parent). Den har då bara en navigation property till Card,
        // men Card som är "dependent" (child) har både FK och navigation property till Student.
        // En student kan existera utan ett kort, men ett kort måste ha en student.
        public int StudentId { get; set; } // Reference property som blir FK. Den identifieras automatiskt av Entity Framework tack vare namnet.
        public Student Student { get; set; } // Navigation property.



        // Ej nödvändig, endast för demo.
        public override string ToString()
        {
            return $"Card: Id: {Id}, StudentId: {StudentId}";
        }
    }
}
