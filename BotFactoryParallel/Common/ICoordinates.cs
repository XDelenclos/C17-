using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Interfaces
{
    interface ICoordinates
    {
        double X { get; set; }
        double Y { get; set; }
        bool Equals(object obj);
    }
}
