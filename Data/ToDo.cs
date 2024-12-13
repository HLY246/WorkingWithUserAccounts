namespace WorkingWithUserAccounts.Data
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? ToDoName { get; set; }
        public string? ToDoDescription { get; set; }

        //realting to user
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
