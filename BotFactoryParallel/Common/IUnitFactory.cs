using BotFactory.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Interfaces
{
    public interface IUnitFactory
    {
        event EventHandler FactoryProgress;
        int StorageCapacity { get; }
        int QueueCapacity { get; }
        int QueueFreeSlots { get; }
        int StorageFreeSlots { get; }
        TimeSpan QueueTime { get; set; }
        Queue<IFactoryQueueElement> Queue { get; set; }
        List<ITestingUnit> Storage { get; set; }
        bool AddWorkableUnitToQueue(Type model, string name, Coordinates parkingpos, Coordinates workingpos);
    }
}
