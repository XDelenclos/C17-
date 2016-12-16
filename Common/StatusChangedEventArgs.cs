using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Interfaces;

namespace BotFactory.Models
{

    public class StatusChangedEventArgs : System.EventArgs, IStatusChangedEventArgs 
    {
        public StatusChangedEventArgs()
        {

        }
        public StatusChangedEventArgs(string newStatus)
        {
            this.NewStatus = newStatus;
        }
        public string NewStatus { get; set; }
       
    }    
}
