using Framework.Worker.Register;

const string WorkerName = "Framework.Worker.Register";
const string HostName = "localhost";
const string QueueName = "registrations";

Console.WriteLine(WorkerName);

var work = new Work(HostName, QueueName);
work.Execute(DoAction);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static void DoAction(string message)
{
    Console.WriteLine(message);
}