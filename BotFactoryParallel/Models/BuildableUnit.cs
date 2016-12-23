using BotFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public abstract class BuildableUnit : IBuildableUnit
    {
        public double BuildTime { get; set; }
        public string Model { get; set; }
        public BuildableUnit(double BuildTime = 5)
        {

        }
    }
}
