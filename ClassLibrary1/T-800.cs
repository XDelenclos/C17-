using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class T_800 : WorkingUnit
    {
        public T_800(string name) : base(name, 3)
        {
            this.BuildTime = 10;
            this.Model = "T-800";
            this.WorkingPos = new Coordinates(3.1, 1.92);
            this.ParkingPos = new Coordinates(0, 0);
        }
    }
}
