using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HangingProcessDetector
{

    public class HangDetector {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GhostWindowFromHungWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern IntPtr HungWindowFromGhostWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern bool IsHungAppWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint procId);
        

        public bool IsProcessRunningUsingHungAppAPI(string process)
        {
            var hangingProcess = FindHangingProcess(process);
            return IsProcessHungUsingHungAppWindow(hangingProcess);
        }

        public bool IsProcessRunningUsingTimer(string process)
        {
            var result = IsProcessHungParent(process);
            return result;
        }


        private bool IsProcessHungParent(string processName)
        {

            Console.WriteLine($"Checking if process '{processName}' is hung...");
            int timeoutMilliseconds = 5000; // Set your desired timeout in milliseconds (e.g., 5000 for 5 seconds)

            bool isHung = IsProcessHungUsingTimer(processName, timeoutMilliseconds);

            if (isHung)
            {
                Console.WriteLine($"The process '{processName}' is hung.");
                return true;
            }
            else
            {
                Console.WriteLine($"The process '{processName}' is running normally.");
                return false;
            }
        }



        private static Process? FindHangingProcess(string process)
        {
            Process[] processes = Process.GetProcessesByName(process);
            Console.WriteLine($"Number of Hanging Process: {processes.Length}");

            Process? hangingProcess =  processes.FirstOrDefault();
            return hangingProcess;
        }


        /// <summary>
        /// Works with UI, not with processes
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <returns></returns>
        static bool IsProcessHungUsingTimer(string processName, int timeoutMilliseconds)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
            {
                Console.WriteLine($"No process with the name '{processName}' found.");
                Console.ReadLine();
                return false;
            }

            Process targetProcess = processes[0];

            // Get the process start time
            DateTime startTime = targetProcess.StartTime;

            // Create a timer to periodically check the process's responsiveness
            using (var timer = new Timer(state =>
                   {
                       if (!IsProcessResponding(targetProcess))
                       {
                           // Kill the process if it's not responding after the timeout
                           targetProcess.Kill();
                       }
                   }, null, 0, timeoutMilliseconds))
            {
                // Let the timer check for responsiveness for a certain duration
                Thread.Sleep(timeoutMilliseconds);

                // Stop the timer
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            // Check if the process is still running
            return !targetProcess.HasExited;
        }

        static bool IsProcessResponding(Process process)
        {
            try
            {
                // Try to get the process's main window handle
                IntPtr mainWindowHandle = process.MainWindowHandle;

                // Check if the process is responding by sending a null message
                return mainWindowHandle != IntPtr.Zero && NativeMethods.SendMessage(mainWindowHandle, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero;
            }
            catch
            {
                // An exception occurred, indicating that the process is not responding
                return false;
            }
        }

        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        }

        #region NotWorking

        private bool IsProcessHungUsingHungAppWindow(Process? hangingProcess)
        {
            if (hangingProcess != null)
            {
                IntPtr windowHandle = hangingProcess.MainWindowHandle;
                Console.WriteLine($"{hangingProcess} - {windowHandle}");
                return IsWindowHung(windowHandle);
            }
            else
            {
                Console.WriteLine("Empty Process Passed");
                return false;
            }
        }

        private bool IsWindowHung(IntPtr hwnd)
        {
            if (IsHungAppWindow(hwnd))
            {
                var hwndReal = HungWindowFromGhostWindow(hwnd);
                uint procId = 0;
                GetWindowThreadProcessId(hwndReal, out procId);
                if (procId > 0)
                {
                    Process proc = null;
                    try
                    {
                        proc = Process.GetProcessById((int)procId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Could not get process with Id '{0}': {1}", procId, ex);
                        return false;
                    }

                    if (proc != null)
                    {
                        Console.WriteLine("Ghost hwnd: {0}, Real hwnd: {1}. ProcessId: {2}, Proccess name: {3}",
                            hwnd, hwndReal, procId, proc.ProcessName);
                        return true;
                    }
                    Console.WriteLine("proc came back null for GetProcessById");
                }
            }
            Console.WriteLine("Not IsHungAppWindow");
            return false;
        }

        #endregion

    }
}
