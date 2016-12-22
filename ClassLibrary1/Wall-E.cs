using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class Wall_E : WorkingUnit
    {
        public Wall_E(string name) : base(name, 2)
        {
            this.BuildTime = 4;
            this.Model = "Wall-E";
            WorkingPos = new Coordinates(2, 2) ;
            ParkingPos = new Coordinates(0, 0);
        }
    }
}
