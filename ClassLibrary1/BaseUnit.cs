using BotFactory.Models.Tools;
using BotFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BotFactory.Models
{
    public abstract class BaseUnit : ReportingUnit, IBaseUnit
    {
        private string r_name;
        public string Name
        {
            get { return r_name; }
            set
            {
                if (r_name != value)
                {
                    r_name = value;
                }

            }
        }
        public double Vitesse { get; set; }
        public Coordinates CurrentPos { get; set; }
        public BaseUnit(string name, double vitesse = 1)
        {
            this.Vitesse = vitesse;
            this.r_name = name;
            this.CurrentPos = new Coordinates(0, 0);
        }
        public async Task<bool> Move(Coordinates destination)
        {
            if (CurrentPos.Equals(destination) != true)
            {
                await Task.Delay(TimeSpan.FromSeconds(Vector.FromCoordinates(CurrentPos, destination).Length / Vitesse));
                CurrentPos = destination;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
