using Bisopi___Proyectos.Calendar;
using Bisopi___Proyectos.Controllers;
using Bisopi___Proyectos.Models;

namespace Bisopi___Proyectos.ViewModels
{
    public class ProgrammingViewModel
    {
        public List<Programming> ProgrammingsList { get; set; }

        public Programming Programming { get; set; }

        public Project Project { get; set; }

        public List<Month> Months { get; set; }

        public List<Project> ProjectsList { get; set; }

        public List<UserData> UsersDataList { get; set; }

    }
}
