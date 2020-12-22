using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SchedulerConsoleApp.UnitTests
{
    class Scheduler_Test
    {
        [Test]
        public void Test_0Task_01()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();

            Scheduler scheduler = new Scheduler();

            Assert.Throws<ArgumentException>(() =>
            {
                scheduler.ExecuteInParallel(ownTasks);
            });
        }

        [Test]
        [Timeout(6000)]
        public void Test_1Task_01()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, false));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));
        }

        [Test]
        [Timeout(6000)]
        public void Test_1Task_02()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, true));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);

            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));
        }

        [Test]
        [Timeout(16000)]
        public void Test_3Tasks_01()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, false));
            ownTasks.Add(new OwnTask("t2", 15, false));
            ownTasks.Add(new OwnTask("t3", 10, false));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);

            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));
        }

        [Test]
        [Timeout(16000)]
        public void Test_3Tasks_02()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, true));
            ownTasks.Add(new OwnTask("t2", 15, false));
            ownTasks.Add(new OwnTask("t3", 10, false));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);

            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));
        }

        [Test]
        [Timeout(16000)]
        public void Test_3Tasks_03()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, false));
            ownTasks.Add(new OwnTask("t2", 15, true));
            ownTasks.Add(new OwnTask("t3", 10, false));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);

            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(0, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));
        }


        [Test]
        [Timeout(16000)]
        public void Test_3Tasks_04()
        {
            List<OwnTask> ownTasks = new List<OwnTask>();
            ownTasks.Add(new OwnTask("t1", 5, false));
            ownTasks.Add(new OwnTask("t2", 15, false));
            ownTasks.Add(new OwnTask("t3", 10, true));

            Scheduler scheduler = new Scheduler();
            scheduler.ExecuteInParallel(ownTasks);

            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[0].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[0].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));
        }
    }
}
