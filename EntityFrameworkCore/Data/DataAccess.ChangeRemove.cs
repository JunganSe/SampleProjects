using EfCoreExempel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExempel.Data
{
    internal partial class DataAccess
    {
        private readonly Context _context = new();

        public void RemoveStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id); // Hämta studenten med rätt id.
            _context.Students.Remove(student); // Markera studenten för borttagning.
            _context.SaveChanges(); // Uppdatera databas och context. Här sker själva borttagningen.
        }

        public void ChangeStudentName(int id, string firstName, string lastName)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id); // Hämta studenten med rätt id.
            student.FirstName = firstName; // Gör önskade ändringar.
            student.LastName = lastName;   //
            _context.Update(student); // Markera studenten för uppdatering. Detta sker automatiskt när något ändras om den finns i context, så denna rad är i regel överflödig.
            _context.SaveChanges(); // Uppdatera databas och context. Här sker själva ändringen.
        }
    }
}
