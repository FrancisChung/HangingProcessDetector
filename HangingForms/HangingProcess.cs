using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangingForms {
    internal class HangingProcess
    {

        public static void StartLoopCalc()
        {
            while (true)
            {
                Console.WriteLine("Running Hanging Process Looping sqrt ...");
                // Introduce some computation to keep the loop busy
                for (int i = 0; i < 1000000000; i++)
                {
                    double result = Math.Sqrt(i);
                }
            }
        }

        public static void StartThreadSleep()
        {
            while (true)
            {
                Console.WriteLine("Running Hanging Process <Thread.Sleep>...");
                Thread.Sleep(10000000);
            }
        }
        

    }
}
