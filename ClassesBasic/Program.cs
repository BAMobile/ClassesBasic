
abstract class Worker
{
    public string Name { get; }
    public string Position, WorkDay;
    public Worker(string name)
    {
        Name = name;
    }

    public void Call()
    {
        Console.WriteLine("Caaall");
    }
    public void WriteCode()
    {
        Console.WriteLine("Write Code");
    }
    public void Relax()
    {
        Console.WriteLine("Relaaaax");
    }
    public abstract void FillWorkDay();
}

class Developer : Worker
{
    public Developer(string name) : base(name)
    {
        Position = "Розробник";
    }
    
    public override void FillWorkDay()
    {
        WriteCode(); Call(); Relax(); WriteCode();
    }
}

class Manager : Worker
{
    private Random rand = new Random();
    public Manager(string name) : base(name)
    {
        Position = "Менеджер";
    }

    public override void FillWorkDay()
    {
        for (int i = 0; i < rand.Next(1, 10); i++)
        {
            Call();
        }
        Relax();
        for (int i = 0; i < rand.Next(1, 5); i++)
        {
            Call();
        }
    }
}

class Team
{
    public string Name { get; }
    private List<Worker> workers = new List<Worker>();
    public Team(string name)
    {
        Name = name;
    }
    public void AddWorker(Worker worker)
    {
        workers.Add(worker);
    }
    public void InfoAboutTeam()
    {
        Console.WriteLine("Назва команди: {0}", Name);
        foreach (var worker in workers)
        {
            Console.WriteLine(worker.Name);
        }
    }
    public void DetailInfoAboutTeam()
    {
        Console.WriteLine("Назва команди: {0}", Name);
        foreach (var worker in workers)
        {
            Console.WriteLine("{0} - {1} - {2}", worker.Name, worker.Position, worker.WorkDay);
        }
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}