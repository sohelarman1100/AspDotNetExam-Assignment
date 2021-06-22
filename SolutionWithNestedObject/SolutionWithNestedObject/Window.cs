using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionWithNestedObject
{
    public class Window:IData
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string window_col { get; set; }
        public Window()
        {

        }
        public Window(int id, int roomid,string win)
        {
            Id = id;
            RoomId = roomid;
            window_col = win;
        }
    }
}
