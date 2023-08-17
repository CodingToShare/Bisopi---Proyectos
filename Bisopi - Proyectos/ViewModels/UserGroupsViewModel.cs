namespace Bisopi___Proyectos.ViewModels
{
    public class UserGroupsViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
