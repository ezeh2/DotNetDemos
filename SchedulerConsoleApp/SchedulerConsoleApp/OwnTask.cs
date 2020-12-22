﻿using System;
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
                Console.WriteLine($"{name}: line");
                Thread.Sleep(1000);
            }
            if (finishedEarly)
            {
                Console.WriteLine("finished early");
                return;
            }

            syncTasks.WaitForAll(this, (ownTask) =>
             {
                 Console.WriteLine($"{ownTask.name}: WaitForAll done");
             });

            Console.WriteLine("finished");
        }
    }
}