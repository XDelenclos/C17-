using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Interfaces;
using BotFactory.Models.Tools;
using System.Reflection;
using BotFactory.Models;
using System.Threading;
using System.Timers;

namespace BotFactory.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private int _storageCapacity;
        private int _queueCapacity;

        public event EventHandler FactoryProgress;
        public int StorageCapacity
        {
            get { return this._storageCapacity; }
        }

        public int QueueCapacity
        {
            get { return this._queueCapacity; }
        }

        public int QueueFreeSlots
        {
            get { return QueueCapacity - Queue.Count; }
        }

        public int StorageFreeSlots
        {
            get { return StorageCapacity - Storage.Count; }
        }

        public TimeSpan QueueTime { get; set; }

        public string Model { get; set; }

        public Queue<IFactoryQueueElement> Queue { get; set; }

        public List<ITestingUnit> Storage { get; set; }
        public UnitFactory(int queuecapacity, int storagecapacity)
        {
            this._queueCapacity = queuecapacity;
            this._storageCapacity = storagecapacity;
            Queue = new Queue<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();
        }

        public async Task<bool> AddWorkableUnitToQueue(Type model, string name, Coordinates parkingpos, Coordinates workingpos)
        {
            // si le nombre de robot à créer est SUPÉRIEUR à la file d'attente OU à la capacité de l'entrepot
            if (Queue.Count > QueueCapacity - 1 || Queue.Count > StorageCapacity)
                return false;

            {

                //alors on vérifie si (le nombre de robot à créer et le nombre de nombre en entrepot) +1 sont SUPÉRIEUR a la capacité de l'entrepot 
                if ((Queue.Count + Storage.Count) + 1 > StorageCapacity)
                    return false;

                {
                    //alors on peut ajouter un nouveau robot a la liste
                    var FqE = new FactoryQueueElement() { Name = name, Model = model, ParkingPos = parkingpos, WorkingPos = workingpos };
                    Queue.Enqueue(FqE);
                   
                        if (FactoryProgress != null)
                        FactoryProgress(FqE, null);
                   
                    //si le nombre de robot à créer == 0 OU le nombre de robots en stock SUPÉRIEUR à la capacité de stockage
                    if (Queue.Count == 0 || Storage.Count > StorageCapacity)
                        return false;

                    {
                        var robot = Activator.CreateInstance((model), new object[] { name });
                        ITestingUnit unit = robot as ITestingUnit;
                        QueueTime += TimeSpan.FromSeconds(unit.BuildTime);
                        foreach (var i in Queue.ToArray())
                        {
                            await Task.Delay(TimeSpan.FromSeconds(unit.BuildTime));
                        }
                        QueueTime -= TimeSpan.FromSeconds(unit.BuildTime);
                        Queue.Dequeue();
                        Storage.Add(unit);
                        if (FactoryProgress != null)
                            FactoryProgress(unit, null);
                        return true;
                    }

                }
            }
        }
    }
}

