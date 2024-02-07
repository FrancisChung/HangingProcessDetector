using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningProcess
{
    internal class RunTask
    {
        public void StartCountJob()
        {
            while (true)
            {
                Console.WriteLine("Running a long running Task counting til 1000000000 ");
                // Introduce some computation to keep the loop busy
                for (int i = 0; i < 1000000000; i++)
                {
                    double result = Math.Sqrt(i);
                    Console.WriteLine($" Output: {result}");
                }
            }
        }
    }
}
