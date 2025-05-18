Nugets som behövs:
Grund:                                Microsoft.EntityFrameworkCore
För att migrera och bygga databasen:  Microsoft.EntityFrameworkCore.Design
För att kommunicera med SQL Server:   Microsoft.EntityFrameworkCore.SqlServer

Skapa en klass för varje tabell i databasen.
Properties motsvarar kolumner.

Skapa en context-klass.
Ange connection string i dess OnConfiguring-metod.
Gör en property av typ DbSet<TEntity> för varje tabell, där TEntity är klasserna som skapades tidigare.

Skapa en klass som hanterar dataåtkomsten.
Den instansierar context och använder den för att kommunicera med databasen.
Skapa metoder för att hämta eller manipulera data.








context.Courses.FirstOrDefault(c => c.Id == id) // Sparar bara den specifika kursen i context, eller alla?
context.Courses.ToList()     // Sparar alla kurser i context. (implicit discard.)
_ = context.Courses.ToList() // Sparar alla kurser i context. (explicit discard.)

// Laddar adresser till context. (Alla eller bara den/de som refereras av course?)
context.Entry(course).Reference(c => c.Address).Load(); // Byt .Reference till .Collection om det är en samling.