using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_programming
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<Action> measure = (body) =>
             {
                 var startTime = DateTime.Now;
                 body();
                 Console.WriteLine("{0} {1}", DateTime.Now - startTime,
                     Thread.CurrentThread.ManagedThreadId);

             };

            Action calcJob = () => { for (int i = 0; i < 3500000; i++) ; };
            Action IoJob = () => { Thread.Sleep(1000); };

            //measure(() =>
            //{
            //    var tasks = Enumerable.Range(1, 10)
            //    .Select(_ => Task.Factory.StartNew(() => measure(IoJob)))
            //    .ToArray();
            //    Task.WaitAll(tasks);
            //});

            //Parallel.For(0, 10, _ => { measure(IoJob); });

            ThreadPool.SetMinThreads(5, 5);

            ParallelEnumerable.Range(1, 10)
            .WithDegreeOfParallelism(10)
            .ForAll(_ => measure(IoJob));
        }
    }
}
