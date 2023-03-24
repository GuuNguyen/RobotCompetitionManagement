using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.DashboardPage
{
    public class IndexModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        
        public List<TeamDTO> Teams { get; set; }
        public List<CheckpointDTO> Checkpoints { get; set; }   
        public List<ScoreDTO> listScore { get; set; }
        public void OnGet()
        {
            Teams = _robotService.GetTeams();
            Checkpoints = _robotService.GetCheckPoints();
            listScore = _robotService.GetScores();
            
        }
    }
}
