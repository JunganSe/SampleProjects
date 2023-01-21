using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreRelations.OneToMany.UsingAttributes;

public class Parent
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Child> Children { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }
    [ForeignKey(nameof(Parent))]
    public int ParentFK { get; set; }

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}