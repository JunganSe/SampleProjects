using EfCoreExempel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EfCoreExempel.Data
{
    internal partial class DataAccess
    {
        public void RecreateDatabaseWithoutMigrations()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void RecreateDatabaseWithMigrations()
        {
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
        }

        public void SeedDatabase()
        {
            var students = new List<Student>()
            {
                new Student() { FirstName = "Adam", LastName = "Adamsson" },
                new Student() { FirstName = "Bertil", LastName = "Bertilsson" },
                new Student() { FirstName = "Ceasar", LastName = "Ceasarsson" },
                new Student() { FirstName = "David", LastName = "Davidsson" }
            };
            _context.Students.AddRange(students);

            var courses = new List<Course>()
            {
                new Course() { Name = "Grundläggande fysik" },
                new Course() { Name = "Matematik för undulater" },
                new Course() { Name = "Att lata sig med stil" }
            };
            _context.Courses.AddRange(courses);

            var teachers = new List<Teacher>()
            {
                new Teacher() { FirstName = "Anna", LastName = "Lärarnsson" },
                new Teacher() { FirstName = "Bosse", LastName = "Betygsnål"}
            };
            _context.Teachers.AddRange(teachers);

            var addresses = new List<Address>()
            {
                new Address() { Street = "Mjölvägen", StreetNumber = 1, ZipCode = 11111, City = "Avesta", Country = "Sverige" },
                new Address() { Street = "Skalbaggen", StreetNumber = 2, ZipCode = 22222, City = "Börnliden", Country = "Sverige" }
            };
            _context.Addresses.AddRange(addresses);

            var cards = new List<Card>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(new Card());
            }
            _context.Cards.AddRange(cards);



            // Lägger till instanser direkt i kopplingstabellens properties.
            // Fungerar endast när kopplingstabellen skapats manuellt.
            var courseStudents = new List<CourseStudent>()
            {
                new CourseStudent() { Course = courses[0], Student = students[0] },
                new CourseStudent() { Course = courses[0], Student = students[1] },
                new CourseStudent() { Course = courses[1], Student = students[0] },
                new CourseStudent() { Course = courses[1], Student = students[1] },
                new CourseStudent() { Course = courses[1], Student = students[2] }
            };
            _context.CourseStudents.AddRange(courseStudents);

            // Lägger till en ny koppling i många-till-många relation.
            // En instans av kopplingen skapas. Den får ena halvan av kopplingen angiven, och den andra halvan automatiskt baserat på var den skapades.
            // Fungerar endast när kopplingstabellen skapats manuellt.
            courses[2].CourseStudents.Add(new CourseStudent() { Student = students[1] });
            students[3].CourseStudents.Add(new CourseStudent() { Course = courses[2] });
            students[2].CourseStudents.Add(new CourseStudent() { Course = courses[2] });



            // Två sätt att koppla många-till-många-relationen mellan Teacher och Course.
            // Fungerar endast när kopplingstabellen autogenererats.
            teachers[0].Courses.Add(courses[0]);
            teachers[0].Courses.Add(courses[2]);
            courses[1].Teachers.Add(teachers[1]);
            courses[2].Teachers.Add(teachers[1]);




            // Två sätt att koppla en-till-en-relationen mellan Student och Card.
            students[0].Card = cards[0];
            cards[1].Student = students[1];
            cards[2].Student = students[2];



            // Två sätt att koppla en-till-många-relationen mellan Student och Address.
            addresses[0].Students.Add(students[0]);
            addresses[0].Students.Add(students[1]);
            students[2].Address = addresses[1];
            students[3].Address = addresses[1];



            // Motsvarande referensproperties som CourseId, StudentId, AddressId, etc
            // kan användas istället för navigation properties som ovan, om objektet redan sparats och fått ett id tilldelat.

            _context.SaveChanges();
        }
    }
}
