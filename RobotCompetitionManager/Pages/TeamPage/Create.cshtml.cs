using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RobotCompetition.Repo;

namespace RobotCompetitionManager.Pages
{
    public class CreateModel : PageModel
    {
        private readonly RobotService robotService = new RobotService();

        [BindProperty]
        public Team Team { get; set; } = default!;


        public IActionResult OnPostAsync()
        {

            if (!ModelState.IsValid || Team == null)
            {
                return Page();
            }
            Team.Status = 1;
            robotService.CreateTeam(Team);
            return RedirectToPage("/Index");
        }
    }
}
