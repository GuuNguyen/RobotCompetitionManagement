using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.CheckPointPage
{
    public class UpdateModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();
        [BindProperty]
        public CheckpointDTO CheckpointDTO { get; set; }
        public IActionResult OnGet(string id)
        {
            CheckpointDTO = _robotService.getCheckPointById(id);
            if( CheckpointDTO == null ) return Page();
            return Page();
        }

        public IActionResult OnPost()
        {
            if(CheckpointDTO != null)
            {
                _robotService.UpdateCheckPoint(CheckpointDTO);
                if (CheckpointDTO == null) return Page();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
