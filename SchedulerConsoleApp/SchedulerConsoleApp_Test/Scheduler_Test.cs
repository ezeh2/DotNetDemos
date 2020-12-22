using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerConsoleApp.UnitTests
{
    class Scheduler_Test
    {
        [Test]
        public void Test1()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, true));
            ownTasks.Add(new OwnTask("t2", 15, false));
            ownTasks.Add(new OwnTask("t3", 10, false));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);
        }
    }
}
