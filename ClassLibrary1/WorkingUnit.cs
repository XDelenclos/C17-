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
        
        //Fonction retournant TRUE si la position du robot correspond à la position de son lieu de travail, retourne FALSE si les deux positions sont différentes.
        // -->reste a implémenter les status.
        //--> génère une erreur  :   NullReferenceException
        public async Task<bool> WorkBegins()
        {

            //try
            //{
            
            if (this.CurrentPos != this.WorkingPos)
            {
                IsWorking = false; 
                await this.Move(this.WorkingPos);
                IsWorking = true;
                return IsWorking;                
            }

            else
            {
                return IsWorking = true;
            }
            //}
            //catch
            //{
            //   
            //}

        }
        //Fonction retournant TRUE si la position du robot correspond à la position de son lieu de stationnement, retourne FALSE si les deux positions sont différentes.
        // -->reste a implémenter les status.
        //--> génère une erreur     :   NullReferenceException
        public async Task<bool> WorkEnds()
        {
            if (CurrentPos != ParkingPos)
            {
                IsWorking = true;
                await this.Move(ParkingPos);
                IsWorking = false;
                return IsWorking ;
            }

            else
                return IsWorking = false;
        }
    }
}
