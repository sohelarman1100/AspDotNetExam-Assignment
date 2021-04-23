using System;
using System.Collections.Generic;
using System.Linq;

namespace ex1Task4
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> list1 = new List<Student> { };
            List<Student> list2 = new List<Student> { };

            int i,j,n, m;
            Console.WriteLine("number of student in list1->");
            n = Convert.ToInt32(Console.ReadLine());
            Student[] stud1 = new Student[n];
            Console.WriteLine("enter student info:");
            for(i=0; i<n; i++)
            {
                stud1[i] = new Student();
                var info = Console.ReadLine();
                var data = info.Split(' ');
                string name = data[0];
                int age = int.Parse(data[1]);
                stud1[i].Name = name;
                stud1[i].Age = age;
            }
            Console.WriteLine("number of student in list2->");
            n = Convert.ToInt32(Console.ReadLine());
            Student[] stud2 = new Student[n];
            for (i = 0; i < n; i++)
            {
                stud2[i] = new Student();
                var info = Console.ReadLine();
                var data = info.Split(' ');
                string name = data[0];
                int age = int.Parse(data[1]);
                stud2[i].Name = name;
                stud2[i].Age = age;
            }

            var res = stud1.Concat(stud2).OrderBy(x => x.Name).ThenBy(x => x.Age).Select(x => x.Name).ToArray();
            foreach(var x in res)
            {
                Console.WriteLine(x);
            }
        }
    }
}
