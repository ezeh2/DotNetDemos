using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SchedulerConsoleApp.UnitTests
{
    class Scheduler_Test
    {
        [SetUp]
        public void Setup()
        {
            OwnTask.AllLines.Clear();
        }

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

            // executeBefore and executeAfter is only executed for "t2", because it is the last, 
            // which calls syncTasks.WaitForAll
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));

            // should be in order "t1", "t2", "t3"
            // and not t1, t3, t2
            List<string> waitForAllList = OwnTask.AllLines.Where(it => it.Contains("WaitForAll done")).ToList();
            Assert.AreEqual(3, waitForAllList.Count);
            Assert.IsTrue(waitForAllList[0].StartsWith("t1"));
            Assert.IsTrue(waitForAllList[1].StartsWith("t3"));
            Assert.IsTrue(waitForAllList[2].StartsWith("t2"));
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

            // executeBefore and executeAfter is only executed for "t2", because it is the last, 
            // which calls syncTasks.WaitForAll
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));

            // should be in order "t2", "t3"
            // and not t3, t2
            List<string> waitForAllList = OwnTask.AllLines.Where(it => it.Contains("WaitForAll done")).ToList();
            Assert.AreEqual(2, waitForAllList.Count);
            Assert.IsTrue(waitForAllList[0].StartsWith("t3"));
            Assert.IsTrue(waitForAllList[1].StartsWith("t2"));
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

            // executeBefore and executeAfter is only executed for "t3", because it is the last, 
            // which calls syncTasks.WaitForAll (and "t2" never calls syncTasks.WaitForAll because it finished earlier)
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));

            // should be in order "t1", "t3"
            List<string> waitForAllList = OwnTask.AllLines.Where(it => it.Contains("WaitForAll done")).ToList();
            Assert.AreEqual(2, waitForAllList.Count);
            Assert.IsTrue(waitForAllList[0].StartsWith("t1"));
            Assert.IsTrue(waitForAllList[1].StartsWith("t3"));
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

            // executeBefore and executeAfter is only executed for "t2", because it is the last, 
            // which calls syncTasks.WaitForAll
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[1].Lines.Count(x => x.Contains("finished")));

            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeBefore")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("WaitForAll done")));
            Assert.AreEqual(0, ownTasks[2].Lines.Count(x => x.Contains("executeAfter")));
            Assert.AreEqual(1, ownTasks[2].Lines.Count(x => x.Contains("finished")));

            // should be in order "t1", "t2"
            List<string> waitForAllList = OwnTask.AllLines.Where(it => it.Contains("WaitForAll done")).ToList();
            Assert.AreEqual(2, waitForAllList.Count);
            Assert.IsTrue(waitForAllList[0].StartsWith("t1"));
            Assert.IsTrue(waitForAllList[1].StartsWith("t2"));
        }
    }
}
