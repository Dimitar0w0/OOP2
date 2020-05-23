using System;
using System.Collections.Generic;
using System.Linq;

namespace IzbiraemiDisciplini
{

    public class Student
    {
        public string Name { get; set; }
        public string StudentID { get; set; }
        public Dictionary<string, double> Courses { get; set; }
        public double Grade { get; set; }
        public string CourseName { get; set; }
        public Student()
        {
            this.Courses = new Dictionary<string, double>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                {
                    break;
                }

                string[] data = input.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(str => str.Trim())
                    .ToArray();
                string courseName = data[0];
                string name = data[1];
                string studentID = data[2];
                double grade = double.Parse(data[3]);

                Student s = students.Where(stu => stu.StudentID.Equals(studentID)).FirstOrDefault();
                if (s == null)
                {
                    s = new Student() { Name = name, StudentID = studentID, CourseName = courseName, Grade = grade };
                    s.Courses.Add(courseName, grade);
                    students.Add(s);
                }
                else
                {
                    if (s.Courses.Count == 0)
                    {
                        s.Courses.Add(courseName, grade);
                    }
                    else continue;
                }
            }


            foreach (var student in students.OrderByDescending(x => x.Grade))
            {
                Console.WriteLine($"{student.Name} {student.CourseName} {student.Grade}");
            }

            Console.ReadLine();
        }
    }
}
