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

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIProjectTaskStatusController : Controller
    {
        private DataContext _context;

        public APIProjectTaskStatusController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var projecttaskstatus = _context.ProjectTaskStatus.Select(i => new {
                i.ProjectTaskStatusID,
                i.StatusName,
                i.Abbreviation,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectTaskStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projecttaskstatus, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectTaskStatus();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ProjectTaskStatusID = new Guid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectTaskStatus.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectTaskStatusID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectTaskStatus.FirstOrDefaultAsync(item => item.ProjectTaskStatusID == key);
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
            var model = await _context.ProjectTaskStatus.FirstOrDefaultAsync(item => item.ProjectTaskStatusID == key);

            _context.ProjectTaskStatus.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ProjectTaskStatus model, IDictionary values) {
            string PROJECT_TASK_STATUS_ID = nameof(ProjectTaskStatus.ProjectTaskStatusID);
            string STATUS_NAME = nameof(ProjectTaskStatus.StatusName);
            string ABBREVIATION = nameof(ProjectTaskStatus.Abbreviation);
            string IS_ACTIVE = nameof(ProjectTaskStatus.IsActive);
            string CREATED_BY = nameof(ProjectTaskStatus.CreatedBy);
            string CREATED = nameof(ProjectTaskStatus.Created);
            string MODIFIED_BY = nameof(ProjectTaskStatus.ModifiedBy);
            string MODIFIED = nameof(ProjectTaskStatus.Modified);

            if(values.Contains(PROJECT_TASK_STATUS_ID)) {
                model.ProjectTaskStatusID = ConvertTo<System.Guid>(values[PROJECT_TASK_STATUS_ID]);
            }

            if(values.Contains(STATUS_NAME)) {
                model.StatusName = Convert.ToString(values[STATUS_NAME]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
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