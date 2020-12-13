using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerConsoleApp
{
    class Scheduler
    {
        private SyncTasks syncTasks = new SyncTasks();

        public void Do()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask(syncTasks, "t1",5, true));
            ownTasks.Add(new OwnTask(syncTasks, "t2", 15, false));
            ownTasks.Add(new OwnTask(syncTasks, "t3", 10, false));
            syncTasks.NotifyBeforeParallelExecution(ownTasks);

            List<Task> tasks = new List<Task>();
            foreach(var ownTask in ownTasks)
            {
                Task task = Task.Factory.StartNew(() => { 
                    ownTask.Execute();
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
