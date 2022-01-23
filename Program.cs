using EfCoreExempel.Data;
using EfCoreExempel.Models;
using System;
using System.Collections.Generic;

namespace EfCoreExempel
{
    internal class Program
    {
        static void Main()
        {
            DataAccess dataAccess = new();
            //dataAccess.RecreateDatabaseWithoutMigrations();
            //dataAccess.RecreateDatabaseWithMigrations();
            //dataAccess.SeedDatabase();

            //PrintList(dataAccess.GetCourses());
            //PrintList(dataAccess.GetStudents());
            //PrintList(dataAccess.GetCourseStudents());
            //PrintList(dataAccess.GetTeachers());
            //PrintList(dataAccess.GetCards());
            //PrintList(dataAccess.GetAddresses());

            PrintList(dataAccess.GetStudentsByAddress1(1));
            PrintList(dataAccess.GetStudentsByAddress2(1));

            //PrintList(dataAccess.GetCoursesByStudent1(1));
            //PrintList(dataAccess.GetCoursesByStudent2(1));
            //PrintList(dataAccess.GetCoursesByTeacher(1));
        }

        static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
