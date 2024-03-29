﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangingProcessDetector
{
    internal class HangingProcess
    {

        public void StartSqrtCalc()
        {
            while (true)
            {
                Console.WriteLine("Running Hanging Process Looping sqrt ...");
                // Introduce some computation to keep the loop busy
                for (int i = 0; i < 1000000000; i++)
                {
                    double result = Math.Sqrt(i);
                    Thread.Sleep(1000);
                }
            }
        }

        public void StartThreadSleep()
        {
            while (true)
            {
                Console.WriteLine("Running Hanging Process <Thread.Sleep>...");
                Thread.Sleep(10000000);
            }
        }


        public void StartThreadSpinWait()
        {
            while (true)
            {
                Console.WriteLine("Running Hanging Process <Thread.SpinWait>...");
                Thread.SpinWait(1000000);
            }
        }


    }
}
