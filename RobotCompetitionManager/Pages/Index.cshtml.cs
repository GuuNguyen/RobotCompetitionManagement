using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RobotService robotService = new RobotService();

        [BindProperty]
        public List<TeamDTO>? Teams { get; set; } = default!;
        public IActionResult OnGet()
        {
            Teams = robotService.GetTeams();
            if(Teams == null)
            {
                Teams = new List<TeamDTO>();
                return Page();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            Teams = robotService.GetTeams();
            
            if(Teams == null) {
                Teams = new List<TeamDTO>();
                return Page(); }
                robotService.DeleteTeam();
                Teams = robotService.GetTeams();
                if (Teams == null)
                {
                    Teams = new List<TeamDTO>();
                    return Page();
                }
            return Page();
        }
    }
}