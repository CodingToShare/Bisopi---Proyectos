using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Bisopi___Proyectos.Data;
using Bisopi___Proyectos.Models;
using Microsoft.AspNetCore.Identity;

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIProjectTasksController : Controller
    {
        private DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public APIProjectTasksController(DataContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var projecttask = _context.ProjectTask.Select(i => new {
                i.TaskID,
                i.TaskName,
                i.TaskGroupID,
                i.Comment,
                i.ProjectID,
                i.TaskStatusID,
                i.StartDate,
                i.EndDate,
                i.EstimateTime,
                i.ExecutionTime,
                i.PositionID,
                i.ResponsibleID,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "TaskID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projecttask, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectTask();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectTask.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.TaskID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectTask.FirstOrDefaultAsync(item => item.TaskID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(Guid key) {
            var model = await _context.ProjectTask.FirstOrDefaultAsync(item => item.TaskID == key);

            _context.ProjectTask.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> TaskGroupLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.TaskGroup
                         orderby i.Name
                         select new {
                             Value = i.TaskGroupID,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> ProjectTaskStatusLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.ProjectTaskStatus
                         orderby i.StatusName
                         select new
                         {
                             Value = i.ProjectTaskStatusID,
                             Text = i.StatusName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public IActionResult ResponsibleLookup()
        {
            var usersWithRole = _userManager.Users;
            var usersData = usersWithRole.Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName }).ToListAsync().Result;

            var resultList = new List<UserData>();

            foreach (var user in usersData)
            {
                var result = new UserData();

                result.UserDataID = Guid.Parse(user.Id);
                result.FirstNameAndLastNAme = $"{user.FirstName} {user.LastName}";

                resultList.Add(result);
            }
            return Ok(resultList);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Projects
                         orderby i.ProjectName
                         select new {
                             Value = i.ProjectID,
                             Text = i.ProjectName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PositionsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Positions
                         orderby i.Name
                         select new {
                             Value = i.PositionID,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(ProjectTask model, IDictionary values) {
            string TASK_ID = nameof(ProjectTask.TaskID);
            string TASK_NAME = nameof(ProjectTask.TaskName);
            string TASK_GROUP_ID = nameof(ProjectTask.TaskGroupID);
            string COMMENT = nameof(ProjectTask.Comment);
            string PROJECT_ID = nameof(ProjectTask.ProjectID);
            string TASK_STATUS_ID = nameof(ProjectTask.TaskStatusID);
            string START_DATE = nameof(ProjectTask.StartDate);
            string END_DATE = nameof(ProjectTask.EndDate);
            string ESTIMATE_TIME = nameof(ProjectTask.EstimateTime);
            string EXECUTION_TIME = nameof(ProjectTask.ExecutionTime);
            string POSITION_ID = nameof(ProjectTask.PositionID);
            string RESPONSIBLE_ID = nameof(ProjectTask.ResponsibleID);
            string IS_ACTIVE = nameof(ProjectTask.IsActive);
            string CREATED_BY = nameof(ProjectTask.CreatedBy);
            string CREATED = nameof(ProjectTask.Created);
            string MODIFIED_BY = nameof(ProjectTask.ModifiedBy);
            string MODIFIED = nameof(ProjectTask.Modified);

            if(values.Contains(TASK_ID)) {
                model.TaskID = ConvertTo<System.Guid>(values[TASK_ID]);
            }

            if(values.Contains(TASK_NAME)) {
                model.TaskName = Convert.ToString(values[TASK_NAME]);
            }

            if(values.Contains(TASK_GROUP_ID)) {
                model.TaskGroupID = ConvertTo<System.Guid>(values[TASK_GROUP_ID]);
            }

            if(values.Contains(COMMENT)) {
                model.Comment = Convert.ToString(values[COMMENT]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = ConvertTo<System.Guid>(values[PROJECT_ID]);
            }

            if(values.Contains(TASK_STATUS_ID)) {
                model.TaskStatusID = ConvertTo<System.Guid>(values[TASK_STATUS_ID]);
            }

            if(values.Contains(START_DATE)) {
                model.StartDate = Convert.ToDateTime(values[START_DATE]);
            }

            if(values.Contains(END_DATE)) {
                model.EndDate = Convert.ToDateTime(values[END_DATE]);
            }

            if(values.Contains(ESTIMATE_TIME)) {
                model.EstimateTime = Convert.ToInt32(values[ESTIMATE_TIME]);
            }

            if(values.Contains(EXECUTION_TIME)) {
                model.ExecutionTime = Convert.ToInt32(values[EXECUTION_TIME]);
            }

            if(values.Contains(POSITION_ID)) {
                model.PositionID = ConvertTo<System.Guid>(values[POSITION_ID]);
            }

            if(values.Contains(RESPONSIBLE_ID)) {
                model.ResponsibleID = ConvertTo<System.Guid>(values[RESPONSIBLE_ID]);
            }

            if(values.Contains(IS_ACTIVE)) {
                model.IsActive = Convert.ToBoolean(values[IS_ACTIVE]);
            }

            if(values.Contains(CREATED_BY)) {
                model.CreatedBy = Convert.ToString(values[CREATED_BY]);
            }

            if(values.Contains(CREATED)) {
                model.Created = values[CREATED] != null ? Convert.ToDateTime(values[CREATED]) : (DateTime?)null;
            }

            if(values.Contains(MODIFIED_BY)) {
                model.ModifiedBy = Convert.ToString(values[MODIFIED_BY]);
            }

            if(values.Contains(MODIFIED)) {
                model.Modified = values[MODIFIED] != null ? Convert.ToDateTime(values[MODIFIED]) : (DateTime?)null;
            }
        }

        private T ConvertTo<T>(object value) {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if(converter != null) {
                return (T)converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            } else {
                // If necessary, implement a type conversion here
                throw new NotImplementedException();
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}