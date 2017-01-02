using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotFactory.Interfaces;
using BotFactory.Models.Tools;
using System.Threading;

namespace BotFactory.Factories
{
    public class UnitFactory : IUnitFactory
    {
        private int _storageCapacity;
        private int _queueCapacity;
        private object _taskLock = new object();

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
            Queue = new Queue<IFactoryQueueElement>();           // Queue system FIFO : First In First Out (1er entré, 1er dehors )
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
            // si le nombre de robot en file d'attente est SUPÉRIEUR à la file d'attente OU à la capacité de l'entrepot 
            //OU  si la somme de la file d'attente et des robots déjà en entrepot est supérieur à la capacité totale de l'entreprot.
            if ((Queue.Count > QueueCapacity - 1 || Queue.Count > StorageCapacity) || ((Queue.Count + Storage.Count) + 1 > StorageCapacity))
                return false;

            {
                var FqE = new FactoryQueueElement() { Name = name, Model = model, ParkingPos = parkingpos, WorkingPos = workingpos };
                Queue.Enqueue(FqE);                                                     //alors on peut ajouter un nouveau robot a la liste   
                var robot = Activator.CreateInstance((model), new object[] { name });  //on instancie l'objet afin de mettre à jour le temps de la file d'attente totale.
                ITestingUnit unit = robot as ITestingUnit;
                QueueTime += TimeSpan.FromSeconds(unit.BuildTime);

                if (Queue.Count == 0 || Storage.Count > StorageCapacity)              //si le nombre de robot à créer == 0 OU le nombre de robots en stock SUPÉRIEUR à la capacité de stockage
                    return false;

                Thread t = new Thread(BuildTask);
                t.Start();
                return true;

            }
        }
        /// <summary>
        /// Méthode vérifiant si le booléen FlagWorking est TRUE or FALSE, 
        /// si FALSE alors on entre dans la boucle faisant appel à BuildUnit().
        /// </summary>
        public void BuildTask()
        {
            lock (_taskLock)
            {
                if (!FlagWorking)
                {
                    while (Queue.Count != 0)              // tant que la queue de la file d'attente est différente de 0
                    {
                        FlagWorking = true;
                        BuildUnit();
                        FlagWorking = false;
                    }
                  
                }
                else
                    return;
            }
        }

        /// <summary>
        /// Fonction Asynchrone renvoyant TRUE or FALSE,
        /// elle instancie le 1er robot ajouté à la QUEUE, elle "construit" le robot durant X secondes
        /// puis l'enlève de la QUEUE pour l'ajouter au STORAGE.
        /// </summary>
        /// <returns></returns>
        public void BuildUnit()
        {
            if (Queue.Count == 0)
                return;

            else
            {
                var t = Queue.Peek();                                                        // on instancie l'objet on prenant le 1er objet mis dans la queue
                var robot = Activator.CreateInstance((t.Model), new object[] { t.Name });
                ITestingUnit unit = robot as ITestingUnit;
                TimeSpan _buildTime = TimeSpan.FromSeconds(unit.BuildTime);                  // on récupère le temps de construction du robot
                FactoryProgress?.Invoke(unit, null);

                Thread.Sleep(_buildTime);                                                    // on attend la construction du robot

                Queue.Dequeue();                                                             // une fois créé on le supprime de la File d'attente 
                Storage.Add(unit);                                                           //et on l'ajoute au Storage

                QueueTime -= _buildTime;                                                     // on déduit son temps de création au temps de temps de la file d'attente totale.
                FactoryProgress?.Invoke(unit, null);

                return;
            }
        }
    }
}
