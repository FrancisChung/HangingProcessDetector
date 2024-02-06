// See https://aka.ms/new-console-template for more information

using HangingProcessDetector;

Console.WriteLine("Starting Hanging Process");
HangingProcess hangingProcess = new HangingProcess();
hangingProcess.Start2();
Console.WriteLine("Shouldn't see this message");
/* Create a new process with the following features
 - Use C# to create a new process
 - There is one method, Load()
 - Load() creates a hanging process
 */