using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Interfaces
{
    public interface IBaseUnit
    {
        string Name { get; set; }

        double Vitesse { get; set; }

        Coordinates CurrentPos { get; set; }

        Task<bool> Move(Coordinates destination);


    }
}
