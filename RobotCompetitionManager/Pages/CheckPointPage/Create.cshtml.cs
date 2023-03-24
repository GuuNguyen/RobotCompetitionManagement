using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages.CheckPointPage
{
    public class CreateModel : PageModel
    {
        private readonly RobotService _robotService = new RobotService();

        [BindProperty]
        public Checkpoint Checkpoint { get; set; } = default;
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid || Checkpoint == null)
            {
                return Page();
            }
            _robotService.CreateCheckPoint(Checkpoint);
            return RedirectToPage("./Index");
        }
    }
}
