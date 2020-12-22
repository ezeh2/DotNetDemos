using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SchedulerConsoleApp
{
    public class OwnTask
    {
        private string name;
        private int loopCount1;
        private bool finishedEarly;
        public List<string> Lines { get; private set; } = new List<string>();

        public OwnTask(string name, int loopCount1, bool finishedEarly)
        {
            this.name = name;
            this.loopCount1 = loopCount1;
            this.finishedEarly = finishedEarly;
        }

        public void Execute(SyncTasks syncTasks)
        {
            for(int i=0;i< loopCount1;i++)
            {
                WriteLine($"{name}: line");
                Thread.Sleep(1000);
            }
            if (finishedEarly)
            {
                WriteLine("finished early");
                return;
            }

            syncTasks.WaitForAll(this, 
            () => WriteLine("executeBefore"),
            (ownTask) => WriteLine($"{ownTask.name}: WaitForAll done"),
            () => WriteLine("executeAfter")
            );

            WriteLine("finished");
        }

        private void WriteLine(string value)
        {
            Console.WriteLine(value);
            Lines.Add(value);
        }
    }
}
