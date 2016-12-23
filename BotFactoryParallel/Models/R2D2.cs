using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class R2D2 : WorkingUnit
    {
        public R2D2(string name) : base(name, 1.5)
        {
            this.BuildTime = 5.5;
            this.Model = "R2D2";
            this.WorkingPos = new Coordinates(2.5, 3);
            this.ParkingPos = new Coordinates(0, 0);
        }
    }
}
