
using System.Text;

abstract class Worker
{
    public string Name { get; }
    public string Position, WorkDay;
    public Worker(string name)
    {
        Name = name;
    }

    void AddTask(string task)
    {
        WorkDay = string.IsNullOrEmpty(WorkDay) ? task : WorkDay + $", {task}";
    }
    public void Call()
    {
        AddTask("Call");
        //Console.WriteLine("Caaall");
    }
    public void WriteCode()
    {
        AddTask("WriteCode");
        //Console.WriteLine("Write Code");
    }
    public void Relax()
    {
        AddTask("Relax");
        //Console.WriteLine("Relaaaax");
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
        worker.FillWorkDay();
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
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Введіть назву команди: ");
        string team_name = Console.ReadLine();
        Team t = new Team(team_name);

        bool exit = true;
        do
        {
            Console.WriteLine("'eхit' - щоб вийти, 'add' - щоб додати співробітника, 'info' - інфа про команду, 'dinfo' - детальна інфа про команду ");
            string command = Console.ReadLine();
            if (command == "exit") exit = false;
            else if (command == "add")
            {
                Console.Write("Введіть ПІП співробітника ");
                string worker_name = Console.ReadLine();
                Console.Write("Введіть тип співробітника: 'd' - Розробник 'm' - Менеджер ");
                string worker_type = Console.ReadLine();

                if (worker_type == "d")
                {
                    Developer d = new Developer(worker_name);
                    t.AddWorker(d);
                }
                else if (worker_type == "m")
                {
                    Manager m = new Manager(worker_name);
                    t.AddWorker(m);
                }
                else Console.WriteLine("Не коректне введення");
            }
            else if (command == "info") t.InfoAboutTeam();
            else if (command == "dinfo") t.DetailInfoAboutTeam();
            else Console.WriteLine("Не коректне введення");
        }
        while (exit);

        Console.ReadKey();
    }
}