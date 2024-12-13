using Microsoft.AspNetCore.Identity;

namespace WorkingWithUserAccounts.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }

        //Navigation Property for ToDo list.
        public ICollection<ToDo>? ToDos { get; set; }
    }
}
