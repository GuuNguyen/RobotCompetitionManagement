using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.TeamPage
{
    public class DeleteModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        public TeamDTO Team { get; set; }
        public void OnGet(string id)
        {
            Team = _robotService.getTeamById(id);
        }
        public IActionResult OnPost(string id)
        {
            Team = _robotService.getTeamById(id);
            if(Team != null)
            {
                _robotService.DeleteTeamById(id);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }
    }
}
