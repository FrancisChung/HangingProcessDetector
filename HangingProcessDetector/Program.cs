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
            var hangProcessName = "HangingForms";
            for (int i = 0; i < 10; i++)
            {
                var hanging = detector.IsProcessRunningUsingHungAppAPI(hangProcessName);
                Console.WriteLine($"Is {hangProcessName} hanging?: {hanging}");
                Console.ReadLine();
            }
 
        }
    }
}
