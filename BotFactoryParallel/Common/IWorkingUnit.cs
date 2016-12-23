using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Interfaces
{
    public interface IWorkingUnit : IBuildableUnit, IBaseUnit, IReportingUnit
    {
        Coordinates ParkingPos { get; set; }
        Coordinates WorkingPos { get; set; }
        bool IsWorking { get; set; }
        Task<bool> WorkBegins();
        Task<bool> WorkEnds();

    }
}
