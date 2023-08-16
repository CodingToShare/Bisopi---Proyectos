using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bisopi___Proyectos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Project> model = _context.Projects.Include(x => x.Tasks).Include(x => x.Country).Where(x => x.Tasks != null && x.Tasks.Any()).ToList();
            foreach (var item in model)
            {
                if (item.Tasks != null)
                {
                    bool deleteTask = false;
                    foreach (var subitem in item.Tasks)
                    {
                        if (subitem.IsActive)
                        {
                            subitem.TaskGroup = await _context.TaskGroup.FindAsync(subitem.TaskGroupID);
                            subitem.TaskStatus = await _context.ProjectTaskStatus.FindAsync(subitem.TaskStatusID);
                        }
                        else
                        {
                            deleteTask = true;
                        }
                    }
                    if (deleteTask)
                    {
                        item.Tasks.RemoveAll(x => x.IsActive == false);
                    }
                }
            }
            List<ProjectTaskStatus> listStatus = _context.ProjectTaskStatus.ToList();
            if (listStatus.Count > 0)
            {
                TempData["listStatus"] = listStatus;
            }
            return View(model.OrderBy(x => x.ProjectName).ToList());
        }
        [HttpPut]
        [Route("/project/task/updateStatus/{taskId}/{taskStatus}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UpdateStatus(Guid taskId, Guid taskStatus)
        {
            try
            {
                ProjectTask task = _context.ProjectTask.Find(taskId) ?? throw new Exception("La tarea no existe");
                task.TaskStatusID = taskStatus;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/project/task/play/{taskID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> PlayTask(Guid taskID)
        {
            try
            {
                ProjectTaskRegistry newRecord = new()
                {
                    ProjectTaskRegistryID = Guid.NewGuid(),
                    ProjectTaskID = taskID,
                    RegistryDate = DateTime.Now,
                    McaExecution = true,
                    McaManual = false,
                    McaHistorico = false,
                    IsActive = true,
                    Created = DateTime.UtcNow.AddHours(-5),
                    CreatedBy = User.Identity.Name,
                    Modified = DateTime.UtcNow.AddHours(-5),
                    ModifiedBy = User.Identity.Name
                };
                await _context.AddAsync(newRecord);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}