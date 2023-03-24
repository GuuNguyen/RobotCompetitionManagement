using FireSharp.Config;
using FireSharp.Interfaces;
using RobotCompetition.Repo;
using TestConnectRealtimeDb;




//Console.WriteLine("Code");
//string Code = Console.ReadLine();
//Console.WriteLine("Name");
//string Name = Console.ReadLine(); 
//Console.WriteLine("SchoolName");
//string SchoolName = Console.ReadLine();

RobotService service = new RobotService();
List<Team> teams= service.GetTeams();
foreach(Team team in teams)
{
    Console.WriteLine(team.Name);
}
//if (Console.ReadLine() == "a")
//{
//    Team team = new Team() { Code = Int32.Parse(Code), Name = Name, SchoolName = SchoolName };
    
//   service.CreateTeam(team);
//}


