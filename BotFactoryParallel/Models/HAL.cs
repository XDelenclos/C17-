using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class HAL : WorkingUnit
    {
        public HAL(string name) : base(name, 0.5)
        {
            this.BuildTime = 7;
            this.Model = "HAL";
            this.WorkingPos = new Coordinates(1.8, 0.9);
            this.ParkingPos = new Coordinates(0, 0);
        }
    }
}
