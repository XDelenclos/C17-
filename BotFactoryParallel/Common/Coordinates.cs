using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Interfaces;

namespace BotFactory.Models.Tools

{
    public class Coordinates : ICoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }


        public override bool Equals(object obj)
        {
            Coordinates ObjCoordinates = obj as Coordinates;

            if (ObjCoordinates == null)
                return false;


            if (!(ObjCoordinates is Coordinates))
                return false;

            return X == (ObjCoordinates).X && Y == (ObjCoordinates).Y;
        }
        public override int GetHashCode()
        {
            return (int)(X + Y);
        }
    }
}
