using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SchedulerConsoleApp
{
    public class SyncTasks
    {
        private List<OwnTask> ownTasks = new List<OwnTask>();
        private List<OwnTask> waitingOwnTasks = new List<OwnTask>();
        private List<Action<OwnTask>> waitingActions = new List<Action<OwnTask>>();
        private Semaphore semaphore;

        public void NotifyBeforeParallelExecution(List<OwnTask> tasks)
        {
            lock(this)
            {
                ownTasks.AddRange(tasks);
                semaphore = new Semaphore(0, ownTasks.Count);
            }
        }

        public void NotifyAfterParallelExecution(List<OwnTask> tasks)
        {
            lock(this)
            {
                foreach (var task in tasks)
                {
                    if (!ownTasks.Remove(task))
                    {
                        throw new ApplicationException("e1");
                    }
                }
            }
        }

        public void WaitForAll(OwnTask ownTask, Action<OwnTask> action)
        {
            bool semaphoreWait = false;
            lock(this)
            {
                waitingOwnTasks.Add(ownTask);
                waitingActions.Add(action);
                if (waitingActions.Count < ownTasks.Count)
                {
                    semaphoreWait = true;
                }
                else
                {
                    for (int k = 0; k < waitingOwnTasks.Count; k++)
                    {
                        waitingActions[k](waitingOwnTasks[k]);
                    }
                    // release all
                    semaphore.Release(waitingActions.Count);
                    semaphore.Close();
                    semaphore = null;
                    waitingActions.Clear();
                    waitingOwnTasks.Clear();
                }
            }
            if (semaphoreWait)
            {
                // outside of lock(this)
                semaphore.WaitOne();
            }
        }
    }
}
