using System.Text;

abstract class Worker
{
	public string Name { get; }
	public string WorkDay { get; private set; }
	public string Position { get; protected set; }
	public Worker(string name)
	{
		Name = name;
	}

	void AddTask(string task)
	{
		WorkDay = string.IsNullOrEmpty(WorkDay) ? task : $"{WorkDay}, {task}";
	}

	public void Call()
	{
		AddTask("Call");
	}

	public void WriteCode()
	{
		AddTask("WriteCode");
	}

	public void Relax()
	{
		AddTask("Relax");
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
	private Random _rand;
	public Manager(string name) : base(name)
	{
		Position = "Менеджер";
		_rand = new Random();
	}

	public override void FillWorkDay()
	{
		for (int i = 0; i < _rand.Next(1, 10); i++)
		{
			Call();
		}
		Relax();
		for (int i = 0; i < _rand.Next(1, 5); i++)
		{
			Call();
		}
	}
}

class Team
{
	public string Name { get; }
	// так одразу видно глобальну приватну змінну
	private List<Worker> _workers;

	public Team(string name)
	{
		Name = name;
		_workers = new List<Worker>();
	}
	public void AddWorker(Worker worker)
	{
		worker.FillWorkDay();
		_workers.Add(worker);
	}

	public void InfoAboutTeam()
	{
		baseInfo();
		foreach (var worker in _workers)
		{
			Console.WriteLine(worker.Name);
		}
	}

	public void DetailInfoAboutTeam()
	{
		baseInfo();
		foreach (var worker in _workers)
		{
			// легше читається текст
			Console.WriteLine($"{worker.Name} - {worker.Position} - {worker.WorkDay}");
		}
	}

	private void baseInfo()
	{
		Console.WriteLine($"Назва команди: {Name}");
		if (_workers.Count == 0)
		{
			Console.WriteLine(@"Немає співробітників");
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
		Team team = new Team(team_name);

		bool exit = true;
		do
		{
			// для назв команд бажано було обрати цифри або укр. літери,
			// бо зараз доводиться весь час перемикатися між англ та укр мовами

			Console.WriteLine(@"'eхit' - щоб вийти, 'add' - щоб додати співробітника, 'info' - інфа про команду,
								'dinfo' - детальна інфа про команду ");
			string command = Console.ReadLine();
			switch (command)
			{
				case "exit":
					{
						exit = false;
						break;
					}
				case "add":
					{
						addWorker(team);
						break;
					}
				case "info":
					{
						team.InfoAboutTeam();
						break;
					}
				case "dinfo":
					{
						team.DetailInfoAboutTeam();
						break;
					}
				default:
					{
						Console.WriteLine(@"Не коректне введення");
						break;
					}
			}
		}
		while (exit);

		Console.ReadKey();
	}

	private static void addWorker(Team team)
	{
		Console.Write(@"Введіть ПІП співробітника ");
		string worker_name = Console.ReadLine();
		Console.Write(@"Введіть тип співробітника: 'd' - Розробник 'm' - Менеджер ");
		string worker_type = Console.ReadLine();
		switch (worker_type)
		{
			case "d":
				{
					team.AddWorker(new Developer(worker_name));
					break;
				}
			case "m":
				{
					team.AddWorker(new Manager(worker_name));
					break;
				}
			default:
				{
					Console.WriteLine(@"Не коректне введення");
					break;
				}
		}
	}
}
