using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkingWithUserAccounts.Data;

namespace WorkingWithUserAccounts.Pages
{
    public class EditToDoTaskModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ToDo? ToDoTaskToEdit { get; set; }

        public EditToDoTaskModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_context.ToDos != null)
            {
                ToDoTaskToEdit = await _context.ToDos.FindAsync(id);

                if (ToDoTaskToEdit == null)
                {
                    return NotFound();
                }

                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id, string newName, string newDescription)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.ToDos != null)
            {

                ToDo newToDoTask = await _context.ToDos.FindAsync(id);

                if (newToDoTask == null)
                {
                    return NotFound();
                }

                newToDoTask.ToDoName = newName;
                newToDoTask.ToDoDescription = newDescription;

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
