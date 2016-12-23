using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models.Tools
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length { get; set; }

        public static Vector FromCoordinates(Coordinates begin, Coordinates end)
        {
            Vector vectoriel = new Vector() { X = (end.X - begin.X), Y = (end.Y - begin.Y), Length = (Math.Sqrt(Math.Pow(end.X - begin.X, 2) + Math.Pow(end.Y - begin.Y, 2))) };

            return vectoriel;
        }


    }
}