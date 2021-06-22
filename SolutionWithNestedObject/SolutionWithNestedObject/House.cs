using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionWithNestedObject
{
    public class House : IData
    {
        public int Id { get; set; }
        public List<Room> Rooms { get; set; }


        public House()
        {

        }
        public House(int Id, List<Room> lst)
        {
            this.Id = Id;
            Rooms = lst;
        }
    }
}
