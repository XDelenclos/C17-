using BotFactory.Models.Tools;
using BotFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{

    public abstract class ReportingUnit : BuildableUnit, IBuildableUnit
    {

        public event OnStatusChangedDelegate UnitStatusChanged;

        public void OnStatusChanged(StatusChangedEventArgs status)
        {
            if (UnitStatusChanged != null)
                UnitStatusChanged(this, status);
        }

    }

}



