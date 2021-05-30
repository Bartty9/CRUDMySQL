using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_MySQL
{
    public class Meeting
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        

        public Meeting(string name, string place, string date, string time)
        {
            Name = name;
            Place = place;
            Date = date;
            Time = time;
        }
    }
}
