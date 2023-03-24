using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.CheckPointPage
{
    public class DeleteModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        public CheckpointDTO Checkpoints { get; set; }
        public void OnGet(string id)
        {
            Checkpoints = _robotService.getCheckPointById(id);
        }

        public IActionResult OnPost(string id)
        {
            Checkpoints = _robotService.getCheckPointById(id);
            if(Checkpoints != null)
            {
                _robotService.DeleteCheckpointById(id);
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }
    }
}
