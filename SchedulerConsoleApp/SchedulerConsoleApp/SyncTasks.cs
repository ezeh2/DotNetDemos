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
        private Action executeOneTimeBefore;
        private Action executeOneTimeAfter;
        private List<Action<OwnTask>> waitingActions = new List<Action<OwnTask>>();
        private Semaphore semaphore;

        public void NotifyBeforeParallelExecution(List<OwnTask> tasks)
        {
            if (tasks.Count<=0)
            {
                throw new ArgumentException("tasks-list should have at least one entry");
            }

            lock(this)
            {
                ownTasks.AddRange(tasks);
                semaphore = new Semaphore(0, ownTasks.Count);
            }
        }

        public void NotifyAfterParallelExecution(List<OwnTask> tasks)
        {
            if (tasks.Count <= 0)
            {
                throw new ArgumentException("tasks-list should have at least one entry");
            }

            lock (this)
            {
                foreach (var task in tasks)
                {
                    if (!ownTasks.Remove(task))
                    {
                        throw new ApplicationException("e1");
                    }
                }
                ExecuteWaitingActions();
            }
        }

        /// <summary>
        /// waits until all OwnTasks have called this method. Then following happens:
        /// * executeOneTimeBefore is executed once
        /// * action is executed for all OwnTask-objects, which have called WaitForAll
        /// * executeOneTimeAfter is executed once
        /// </summary>
        /// <param name="ownTask"></param>
        /// <param name="executeOneTimeBefore"></param>
        /// <param name="action"></param>
        /// <param name="executeOneTimeAfter"></param>
        public void WaitForAll(OwnTask ownTask, Action executeOneTimeBefore, Action<OwnTask> action, Action executeOneTimeAfter)
        {
            bool semaphoreWait = false;
            lock(this)
            {
                this.executeOneTimeBefore = executeOneTimeBefore;
                this.executeOneTimeAfter = executeOneTimeAfter;
                this.waitingOwnTasks.Add(ownTask);
                this.waitingActions.Add(action);
                if (waitingActions.Count < ownTasks.Count)
                {
                    semaphoreWait = true;
                }
                ExecuteWaitingActions();
            }
            if (semaphoreWait)
            {
                // outside of lock(this)
                semaphore.WaitOne();
            }
        }

        private void ExecuteWaitingActions()
        {
            if ( (waitingActions.Count>0) && (waitingActions.Count == ownTasks.Count) )
            {
                this.executeOneTimeBefore();
                for (int k = 0; k < waitingOwnTasks.Count; k++)
                {
                    waitingActions[k](waitingOwnTasks[k]);
                }
                this.executeOneTimeAfter();
                // release all
                if (semaphore!=null)
                {
                    semaphore.Release(waitingActions.Count);
                    semaphore.Close();
                    semaphore = null;
                }
                waitingActions.Clear();
                waitingOwnTasks.Clear();
            }
        }
    }
}
