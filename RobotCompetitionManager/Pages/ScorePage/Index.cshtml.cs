using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.ScorePage
{
    public class ScoreIndexModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        public List<ScoreDTO>? Scores { get; set; } = default!;
        public IActionResult OnGet()
        {
            Scores = _robotService.GetScores();
            if (Scores == null)
            {
                Scores = new List<ScoreDTO>();
                return Page();
            }
            return Page();
        }
    }
}
