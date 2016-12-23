using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Models;

namespace BotFactory.Interfaces
{
    public interface IReportingUnit
    {
        event OnStatusChangedDelegate UnitStatusChanged;
    }

    public delegate void OnStatusChangedDelegate(object source, StatusChangedEventArgs args);

}
