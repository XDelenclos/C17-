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
        public bool FlagWorking { get; set; }

        #region Prop
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
        #endregion


        public UnitFactory(int queuecapacity, int storagecapacity)
        {
            this._queueCapacity = queuecapacity;
            this._storageCapacity = storagecapacity;
            Queue = new Queue<IFactoryQueueElement>();
            Storage = new List<ITestingUnit>();
            FlagWorking = false;            
        }

        /// <summary>
        /// Fonction booléen (renvoyant TRUE or FALSE) vérifiant le nombre de places disponibles dans la QUEUE et le STORAGE.
        /// Si TRUE ajoute un objet à la QUEUE, met à jour la QueueTime, et appele la fonction BuildTask().
        /// </summary>
        /// <param name="model">de type TYPE</param>
        /// <param name="name">de type string</param>
        /// <param name="parkingpos">de type Coordinates (int x, int y)</param>
        /// <param name="workingpos">de type Coordinates (int x, int y)</param>
        /// <returns></returns>
        public bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingpos, Coordinates workingpos)
        {
            // si le nombre de robot à créer est SUPÉRIEUR à la file d'attente OU à la capacité de l'entrepot
            if ((Queue.Count > QueueCapacity - 1 || Queue.Count > StorageCapacity) || ((Queue.Count + Storage.Count) + 1 > StorageCapacity))
                return false;

            {
                //alors on peut ajouter un nouveau robot a la liste
                var FqE = new FactoryQueueElement() { Name = name, Model = model, ParkingPos = parkingpos, WorkingPos = workingpos };
                Queue.Enqueue(FqE);
                var robot = Activator.CreateInstance((model), new object[] { name });
                ITestingUnit unit = robot as ITestingUnit;
                QueueTime += TimeSpan.FromSeconds(unit.BuildTime);
                FactoryProgress?.Invoke(FqE, null);

                //si le nombre de robot à créer == 0 OU le nombre de robots en stock SUPÉRIEUR à la capacité de stockage
                if (Queue.Count == 0 || Storage.Count > StorageCapacity)
                    return false;


                BuildTask();
                return true;

            }
        }
        /// <summary>
        /// Méthode vérifiant si le booléen FlagWorking est TRUE or FALSE, 
        /// si FALSE alors on entre dans la boucle faisant appel à BuildUnit().
        /// </summary>
        public void BuildTask()
        {
            if (!FlagWorking)
            {
                Task.Run(async () =>
              {
                  // on passe le booléen qui identifie si l'usine est déjà en construction a VRAI
                  FlagWorking = true;

                  // tant que  la file d'attente n'est pas égale a 0
                  while (Queue.Count != 0)
                  {
                      // alors on attend la construction des robots en attentes
                      await BuildUnit();
                  }

                  // On repasse le booléen à faux car la construction du robo est terminée. 
                  FlagWorking = false;
              });
            }
        }

        /// <summary>
        /// Fonction Asynchrone renvoyant TRUE or FALSE,
        /// elle instancie le 1er robot ajouté à la QUEUE, elle "construit" le robot durant X secondes
        /// puis l'enlève de la QUEUE pour l'ajouter au STORAGE.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> BuildUnit()
        {
            if (Queue.Count == 0)
                return false;

            else
            {
                // on instancie l'objet on prenant le 1er objet mis dans la queue
                var t = Queue.Peek();
                var robot = Activator.CreateInstance((t.Model), new object[] { t.Name });
                ITestingUnit unit = robot as ITestingUnit;

                // on récupère le temps de construction du robot
                TimeSpan _buildTime = TimeSpan.FromSeconds(unit.BuildTime);
                FactoryProgress?.Invoke(unit, null);

                // on attend la construction du robot
                await Task.Delay(_buildTime);

                // une fois créé on le supprime de la File d'attente et on l'ajoute au Storage
                Queue.Dequeue();
                Storage.Add(unit);
                QueueTime -= _buildTime;
                FactoryProgress?.Invoke(unit, null);

                return true;
            }
        }
    }
}



















