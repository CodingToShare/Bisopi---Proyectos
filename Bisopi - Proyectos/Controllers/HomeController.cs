using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            List<Guid> taskIds = new();
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            taskIds = await _context.ProjectTask.Include(x => x.Project).Where(x => x.Project.LeaderID == Guid.Parse(user.Value)).Select(x => x.TaskID).ToListAsync();
            ProjectTaskRegistry? taskRegistry = await _context.TaskRegistry.Where(x => taskIds.Contains(x.ProjectTaskID) && x.McaExecution == true).FirstOrDefaultAsync();
            if (taskRegistry != null)
            {
                Response.Cookies.Append("executingTask", taskRegistry.ProjectTaskID.ToString());
                Response.Cookies.Append("taskRegistry", taskRegistry.ProjectTaskRegistryID.ToString());
            }
            return View();
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
                ProjectTask task = await _context.ProjectTask.FindAsync(newRecord.ProjectTaskID) ?? throw new Exception();
                Response.Cookies.Append("executingTask", task.TaskID.ToString());
                Response.Cookies.Append("taskRegistry", newRecord.ProjectTaskRegistryID.ToString());
                return Json(new { id = newRecord.ProjectTaskRegistryID, executionDate = newRecord.RegistryDate, executionTime = task.ExecutionTime });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("/project/task/stop/{registryID}/{time}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> StopTask(Guid registryID, int time)
        {
            try
            {
                ProjectTaskRegistry record = await _context.TaskRegistry.FindAsync(registryID) ?? throw new Exception();
                ProjectTask task = await _context.ProjectTask.FindAsync(record.ProjectTaskID) ?? throw new Exception();

                record.ExecutionTime = time <= 0 ? 0 : Math.Abs(time - task.ExecutionTime);
                record.McaExecution = false;
                record.McaHistorico = true;
                record.Modified = DateTime.UtcNow.AddHours(-5);
                record.ModifiedBy = User.Identity.Name;


                task.ExecutionTime = time <= 0 ? task.ExecutionTime : time;
                await _context.SaveChangesAsync();
                Response.Cookies.Delete("executingTask");
                Response.Cookies.Delete("taskRegistry");
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("/project/task/executionDate/{executeRegistryID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetExecutionDate(Guid executeRegistryID)
        {
            try
            {
                ProjectTaskRegistry registry = await _context.TaskRegistry
                    .Include(x => x.ProjectTask)
                    .FirstOrDefaultAsync(x => x.ProjectTaskRegistryID == executeRegistryID) ?? throw new Exception();
                return Json(new { executionDate = registry.RegistryDate, executionTime = registry.ProjectTask.ExecutionTime, taskID = registry.ProjectTask.TaskID });
            }
            catch
            {
                throw;
            }

        }
        [HttpGet]
        [Route("/project/task/get/{taskID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTask(Guid taskID)
        {
            try
            {
                var task = await _context.ProjectTask
                    .Include(x => x.Project)
                    .Where(x => x.TaskID == taskID)
                    .Select(x => new { ProjectName = x.Project.ProjectName, TaskName = x.TaskName })
                    .FirstOrDefaultAsync() ?? throw new Exception();
                return Ok(task);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("/project/task/GetRegistry/{registryID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetRegistry(Guid registryID)
        {
            try
            {
                var task = await _context.TaskRegistry
                    .Include(x=> x.ProjectTask)
                    .Where(x=> x.ProjectTaskRegistryID == registryID)
                    .Select(x=> new
                    {
                        task = x.ProjectTaskID,
                        registry = x.ProjectTaskRegistryID,
                        date = x.RegistryDate,
                        hours = x.ExecutionTime / 3600,
                        minutes = (x.ExecutionTime % 3600) / 60,
                        comment = x.Comment
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception();
                return Ok(task);
            }
            catch { throw; }
        }
        private static string formateTime(int timeInSeconds)
        {
            int hours = timeInSeconds / 3600;
            int minutes = (timeInSeconds % 3600) / 60;
            int seconds = timeInSeconds % 60;

            string executionTime;
            if (hours > 0)
            {
                executionTime = $"{hours:D2}:{minutes:D2}:{seconds:D2} horas";
            }
            else if (minutes > 0)
            {
                executionTime = $"{minutes:D2}:{seconds:D2} min";
            }
            else
            {
                executionTime = $"{seconds:D2} seg";
            }
            return executionTime;
        }
        [HttpGet]
        [Route("/project/task/historial/{taskID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Historial(Guid taskID)
        {
            try
            {
                var task = await _context.ProjectTask.FindAsync(taskID) ?? throw new Exception();
                string plantilla = $@"
                    <div class=""modal-header"">
                        <h5 class=""modal-title"" id=""ModalTitleHistorial"">Historial: {task.TaskName}</h5>
                    </div>
                    <div class=""modal-body"" >
                        <div class=""form-row"">
                            <div class=""form-group col-md-12"">
                                {task.Comment}
                            </div>
                            <div class=""form-group col-md-16"">
                                <label for=""fecha_registro"">Tiempo Ejecutado: </label> <span id=""div_crono_3_{taskID}"">{formateTime(task.ExecutionTime)}</span>
                            </div>
                        </div>
                        <table class=""table table-striped"">
                            <thead class=""thead-dark"">
                                <tr style=""text-align:center;"">
                                    <th class=""p-1"" scope=""col"">Fecha Registro</th>
                                    <th class=""p-1"" scope=""col"">Tiempo Reportado</th>
                                    <th class=""p-1"" scope=""col"">Comentario</th>
                                    <th class=""p-1"" scope=""col""></th>
                                    <th class=""p-1"" scope=""col""></th>
                                </tr>
                            </thead>
                            <tbody>
                ";
                List<ProjectTaskRegistry> list = new();
                var lista = from i in _context.TaskRegistry
                            orderby i.IsActive descending,i.RegistryDate ascending
                            where i.ProjectTaskID == taskID
                            select i;
                list = await lista.ToListAsync();
                if (list.Count == 0)
                {
                    plantilla += @$"
                                <tr class=""text-center""><th colspan=""5"">No se han hecho registros</th></tr>
                            </tbody>
                        </table>
                    </div>
                    <div class=""modal-footer"">
                        <input type=""hidden"" id=""id_proyecto_tarea"" name=""id_proyecto_tarea"" value=""{task.TaskID}"">
                        <button type=""button"" class=""btn btn-dark"" onclick=""hideHistorialModal()"">Cancelar</button>
                    </div>
                ";
                }
                else
                {
                    foreach (var item in list)
                    {
                        string delete = item.IsActive ? @$"<img onclick=""deleteHistorialRecord('{item.ProjectTaskRegistryID}','{task.TaskID}')"" src='/img/trash.png' style='width:20px;cursor:pointer' title='Eliminar'/>" : "";
                        string edit = item.IsActive ? $@"<img onclick=""editRegister('{item.ProjectTaskRegistryID}')"" src=""/img/edit.png"" style=""width:20px;cursor:pointer"" title=""Editar"" />" : "";
                        plantilla += $@"
	                        <tr style=""text-align:center;"">
                                        <td class=""p-1"">{item.RegistryDate.ToString("yyyy-MM-dd")}</td>
                                        <td class=""p-1"">{formateTime(item.ExecutionTime)}</td>
                                        <td class=""p-1"" style=""text-align:left;"">{item.Comment}</td>
                                        <td class=""p-1"" style=""text-align:left;"">
			                                {edit}
		                                </td>
                                        <td class=""p-1""  style=""text-align:left;"">
                                           {delete}   
		                                </td>
	                        </tr>
                        ";
                    }
                    plantilla += $@"
                            </tbody>
                            </table>
                        </div>
                        <div class=""modal-footer"">
                            <input type=""hidden"" id=""id_proyecto_tarea"" name=""id_proyecto_tarea"" value=""{task.TaskID}"">
                            <button type=""button"" class=""btn btn-dark"" onclick=""hideHistorialModal()"">Cancelar</button>
                        </div>
                    ";
                }
                return Content(plantilla, "text/html");
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("/project/task/history/{registryID}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> TaskLog(Guid registryID)
        {
            try
            {
                ProjectTaskRegistry record = await _context.TaskRegistry.FindAsync(registryID) ?? throw new Exception();
                record.IsActive = false;
                record.Modified = DateTime.UtcNow.AddHours(-5);
                record.ModifiedBy = User.Identity.Name;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> TaskLog(ProjectTaskRegistry model)
        {
            try
            {
                ProjectTaskRegistry? registry = await _context.TaskRegistry.Where(x=> x.ProjectTaskRegistryID == model.ProjectTaskRegistryID).FirstOrDefaultAsync();
                ProjectTask task = await _context.ProjectTask.FindAsync(model.ProjectTaskID) ?? throw new Exception();
                if (registry == null)
                {
                    model.ExecutionTime = model.ExecutionTimeHours * 3600 + model.ExecutionTimeMinutes * 60;
                    model.ProjectTaskRegistryID = Guid.NewGuid();
                    model.McaExecution = false;
                    model.McaHistorico = true;
                    model.McaManual = false;
                    model.IsActive = true;
                    model.Created = DateTime.UtcNow.AddHours(-5);
                    model.CreatedBy = User.Identity.Name;
                    model.Modified = DateTime.UtcNow.AddHours(-5);
                    model.ModifiedBy = User.Identity.Name;

                    task.ExecutionTime = task.ExecutionTime + model.ExecutionTime;

                    await _context.AddAsync(model);
                }
                else
                {
                    int executionTime = model.ExecutionTimeHours * 3600 + model.ExecutionTimeMinutes * 60;
                    if(registry.ExecutionTime != executionTime && executionTime != 0)
                    {
                        registry.ExecutionTime = executionTime;
                        task.ExecutionTime = task.ExecutionTime + registry.ExecutionTime;
                    }
                    if(model.RegistryDate != registry.RegistryDate)
                    {
                        registry.RegistryDate = model.RegistryDate;
                    }
                    if(model.Comment != registry.Comment)
                    {
                        registry.Comment = model.Comment;
                    }
                    registry.Modified = DateTime.UtcNow.AddHours(-5);
                    registry.ModifiedBy = User.Identity.Name;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
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