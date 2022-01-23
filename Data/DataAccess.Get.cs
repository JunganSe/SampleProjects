using EfCoreExempel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCoreExempel.Data
{
    internal partial class DataAccess
    {
        #region Get basic entities
        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public List<CourseStudent> GetCourseStudents()
        {
            // Kopplingarna kan hämtas direkt eftersom kopplingstabellen skapats manuellt.
            return _context.CourseStudents.ToList();
        }

        public List<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }

        public List<Card> GetCards()
        {
            return _context.Cards.ToList();
        }

        public List<Address> GetAddresses()
        {
            return _context.Addresses.ToList();
        }
        #endregion



        public Student GetStudent(int studentId)
        {
            return _context.Students
                .FirstOrDefault(s => s.Id == studentId); // Returnerar den första träffen eller null om inget hittas.
        }



        public List<Student> GetStudentsByAddress1(int addressId)
        {
            return _context.Students
                .Where(s => s.AddressId == addressId)
                .ToList();
        }

        public List<Student> GetStudentsByAddress2(int addressId)
        {
            return _context.Students
                .ToList()
                .FindAll(s => s.AddressId == addressId);
        }



        public List<Card> GetCardsByStudentsAtAddress(int addressId)
        {
            return _context.Students // Titta i listan med studenter...
                .Where(s => s.AddressId == addressId) // ...och välj bara de med rätt adress...
                .Select(s => s.Card) // ...och plocka sedan ut deras kort.
                .ToList();
        }



        public List<Student> GetStudentsAtSameAddress(int studentId)
        {
            int addressId = _context.Students // Titta i listan med studenter.
                .FirstOrDefault(s => s.Id == studentId) // Hämta studenten baserat på id...
                .AddressId; // ...sedan hämtas AddressId från den studenten.
            return _context.Students
                .Where(s => s.AddressId == addressId)
                .ToList();
        }



        // Many-to-many-relation där kopplingstabellen autogenereras.
        // Course och Teacher har här en varsin navigation collection med varandra.
        public List<Course> GetCoursesByTeacher(int teacherId)
        {
            return _context.Teachers // Titta i listan med lärare.
                .Include(t => t.Courses) // Inkludera (hämta från databasen) kurser.
                .FirstOrDefault(t => t.Id == teacherId) // Hämta rätt lärare från listan baserat på id.
                .Courses // Hämta kurserna som läraren har.
                .ToList();
        }



        // Many-to-many-relation där kopplingstabellen skapats manuellt.
        // Course och Student länkar inte direkt till varandra med navigation collection, utan till CourseStudents som har FK till båda.
        public List<Course> GetCoursesByStudent1(int studentId)
        {
            return _context.Courses // Titta i listan med kurser.
                .Include(c => c.CourseStudents) // Överflödig?
                .Where(c => c.CourseStudents // Titta i kursernas CourseStudent (kopplingar)...
                    .Any(cs => cs.StudentId == studentId)) // ...och använd bara de kopplingar där StudentId matchar. (Om ingen matchning finns returnerar Any() false, så denna koppling ignoreras av Where()-raden.)
                .ToList();
        }

        public List<Course> GetCoursesByStudent2(int studentId)
        {
            // Troligtvis den bästa metoden.
            // Ska ge den mest effektiva sql-queryn och är lättast att läsa.
            return _context.CourseStudents // I listan med kopplingar...
                .Include(cs => cs.Course) // Inkludera (hämta från databasen) kurser. (Denna behövs egentligen inte för att Select() gör en join implicit.)
                .Where(cs => cs.StudentId == studentId) // ...där kopplingen går till vald student...
                .Select(cs => cs.Course) // ...tas kurserna ut.
                .ToList();
        }



        public List<Course> GetCoursesByCard()
        {


            throw new NotImplementedException();
        }



        public List<Course> GetCoursesByStudentsAtAddress(int addressId)
        {
            

            throw new NotImplementedException();
        }


    }
}
