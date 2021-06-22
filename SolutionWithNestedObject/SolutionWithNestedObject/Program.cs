using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SolutionWithNestedObject
{
    class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection constring = new SqlConnection();
            constring.ConnectionString = "Server=DESKTOP-AG2EEU5\\SQLEXPRESS;Database=demo;User Id=aspnetb5;Password=123456;";
            var obj = new MyORM<House>(constring);

            List<Window> win_lst1 = new List<Window>()
            {
                new Window(1,1,"red"),
                new Window(2,1,"green"),
                new Window(3,1,"blue")
            };

            List<Window> win_lst2 = new List<Window>()
            {
                new Window(4,2,"white"),
                new Window(5,2,"dark")
            };

            List<Room> room_lst1 = new List<Room>()
            {
                new Room(1,1,3000,win_lst1),
                new Room(2,1,5000,win_lst2)
            };

            var stud1 = new House(1, room_lst1);
            //obj.Insert(stud1);   //insert ke comment kore rakha hoyeche onno method gulo check korar jonno...eti comment out kore dite hobe.

            stud1.Name = "sohel arman"; //updated value for name of object ins;
            obj.Update(stud1);

            obj.Delete(stud1.Id);
            obj.Delete(stud1);

            var res = obj.GetById(7);
            Console.WriteLine("{0}, {1}, {2}", res.Id, res.Name, res.Age);

            var rcrd = obj.GetAll();
            for (var i = 0; i < rcrd.Count; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", rcrd[i].Name, rcrd[i].Id, rcrd[i].Age);
            }
        }
    }
}
