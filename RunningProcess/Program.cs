// See https://aka.ms/new-console-template for more information

using RunningProcess;

Console.WriteLine("Hello, World!");

var runTask = new RunTask();
runTask.StartCountJob();

Console.ReadLine();
