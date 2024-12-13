using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using WorkingWithUserAccounts.Data;

namespace WorkingWithUserAccounts.Pages
{
    public class CreateNewToDoTaskModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateNewToDoTaskModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string toDoName, string toDoDescription)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            var NewToDoTask = new ToDo { ToDoName = toDoName, ToDoDescription = toDoDescription, UserId = userId };

            if (_context.ToDos != null)
            {
                _context.ToDos.Add(NewToDoTask);
                await _context.SaveChangesAsync();
                return RedirectToPage("/ToDoList");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
