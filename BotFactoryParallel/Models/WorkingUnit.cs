using BotFactory.Models.Tools;
using BotFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public abstract class WorkingUnit : BaseUnit, ITestingUnit
    {
        public Coordinates ParkingPos { get; set; }
        public Coordinates WorkingPos { get; set; }
        public bool IsWorking { get; set; }
        public WorkingUnit(string name, double vitesse = 1) : base(name, vitesse)
        {

        }
        /// <summary>
        /// Fonction Booléenne asynchronne (renvoyant TRUE or FALSE) comparant 2 coordonnées (Coordinates),
        /// Si elles sont différentes, elles déclenchent la fonction Move(coordinates destination) et met à jour le statut (NewStatus)
        /// Sinon renvoye le statut (NewStatus): "Au travail"
        /// </summary>
        /// <returns></returns>
        public async Task<bool> WorkBegins()
        {

            if (this.CurrentPos != this.WorkingPos)
            {
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "En mouvement..." });
                IsWorking = false;
                await this.Move(this.WorkingPos);
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "Au travail." });
                IsWorking = true;
                return IsWorking;
            }

            else
            {
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "Déjà au travail." });
                return IsWorking = true;
            }

        }
        /// <summary>
        /// Fonction Booléenne asynchronne (renvoyant TRUE or FALSE) comparant 2 coordonnées (Coordinates),
        /// Si elles sont différentes, elles déclenchent la fonction Move(coordinates destination) et met à jour le statut (NewStatus)
        /// Sinon renvoye le statut (NewStatus): "Au Parking"
        /// </summary>
        /// <returns></returns>
        public async Task<bool> WorkEnds()
        {

            if (CurrentPos != ParkingPos)
            {
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "Sur le retour..." });
                IsWorking = true;
                await this.Move(ParkingPos);
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "Au Parking." });
                IsWorking = false;
                return IsWorking;
            }

            else
            {
                OnStatusChanged(new StatusChangedEventArgs { NewStatus = "Déjà au parking." });
                return IsWorking = false;
            }

        }
    }
}

