using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionWithNestedObject
{
    public class Room:IData
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public double Rent { get; set; }
        public List<Window> windows { get; set; }
        public Room()
        {

        }
        public Room(int id,int houseid,double rent,List<Window> win)
        {
            Id = id;
            HouseId = houseid;
            Rent = rent;
            windows = win;
        }
    }
}
