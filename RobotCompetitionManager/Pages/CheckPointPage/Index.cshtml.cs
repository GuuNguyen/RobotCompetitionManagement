using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.CheckPointPage
{
    public class IndexModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        public List<CheckpointDTO>? Checkpoints { get; set; } = default!;
        public IActionResult OnGet()
        {
            Checkpoints = _robotService.GetCheckPoints();
            if (Checkpoints == null)
            {
                Checkpoints = new List<CheckpointDTO>();
                return Page();
            }
            return Page();
        }
    }
}
