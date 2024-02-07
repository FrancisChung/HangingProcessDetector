// See https://aka.ms/new-console-template for more information

using HangingProcessDetector;

Console.WriteLine("Starting Hanging Process");
var hangingProcess = new HangingProcess();
hangingProcess.StartSqrtCalc();
Console.WriteLine("Shouldn't see this message");
Console.ReadLine();