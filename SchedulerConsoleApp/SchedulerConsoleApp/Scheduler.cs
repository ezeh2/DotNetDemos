using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerConsoleApp
{
    class Scheduler
    {
        private SyncTasks syncTasks = new SyncTasks();

        public void ExecuteInParallel(List<OwnTask> ownTasks)
        {
            syncTasks.NotifyBeforeParallelExecution(ownTasks);

            List<Task> tasks = new List<Task>();
            foreach(var ownTask in ownTasks)
            {
                Task task = Task.Factory.StartNew(() => { 
                    ownTask.Execute(syncTasks);
                    List<OwnTask> y = new List<OwnTask>();
                    y.Add(ownTask);
                    syncTasks.NotifyAfterParallelExecution(y);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            
        }
    }
}
