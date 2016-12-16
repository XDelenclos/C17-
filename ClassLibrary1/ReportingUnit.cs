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

        //public delegate void OnStatusChangedDelegate(object source, StatusChangedEventArgs args);

        public event OnStatusChangedDelegate UnitStatusChanged;
                
        public void OnStatusChanged(StatusChangedEventArgs status, string etat)
        {
            StatusChangedEventArgs statusChanged = new StatusChangedEventArgs(etat);
            UnitStatusChanged(status, statusChanged);
        }

        //public EventHandler Unitstatuschanged { get; set; }

        //public virtual void OnStatusChanged(StatusChangedEventArgs status)
        //{

        //    if (UnitStatusChanged == null)
        //        UnitStatusChanged(this, status);
        //}


    }

}



