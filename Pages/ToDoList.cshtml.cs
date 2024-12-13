using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using WorkingWithUserAccounts.Data;
using Microsoft.Identity.Client;
using System.Linq;

namespace WorkingWithUserAccounts.Pages
{
    public class ToDoListModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<ToDo>? ToDoList { get; set; }

        public ToDoListModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager ?? throw new ArgumentException(nameof(signInManager)); 
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
           
        }

        public void OnGet()
        {
            if (User.Identity != null) 
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(User);

                    if (_context.ToDos != null)
                    {
                        ToDoList = _context.ToDos
                            .Where(todo => todo.UserId == userId)
                            .ToList();
                    }
                }
            }
            else
            {
                RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (_context.ToDos != null)
            {
                var itemToDelete = await _context.ToDos.FindAsync(id);

                if (itemToDelete == null)
                {
                    return NotFound();
                }

                _context.ToDos.Remove(itemToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
