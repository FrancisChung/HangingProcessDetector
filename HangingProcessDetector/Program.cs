namespace HangingProcessDetector {
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Diagnostics;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            var detector = new HangDetector();
            // var hangProcessName = "HangingForms";
            // hangProcessName = "HangingProcess";
            // var hangProcessName = "DeadlockedProcess";
            var hangProcessName = "RunningProcess";
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Loop {i}");
                var hangingAPI = detector.IsProcessRunningUsingHungAppAPI(hangProcessName);
                Console.WriteLine($"Is {hangProcessName} hanging (API)?: {hangingAPI}");

                var hangingTimer = detector.IsProcessRunningUsingTimer(hangProcessName);
                Console.WriteLine($"Is {hangProcessName} hanging (timer)?: {hangingTimer}");
                Console.WriteLine();
                Console.ReadLine();
            }
 
        }
    }
}
