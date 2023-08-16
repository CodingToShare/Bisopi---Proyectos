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
    public class APIProjectCommitmentsController : Controller
    {
        private DataContext _context;

        public APIProjectCommitmentsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var projectcommitments = _context.ProjectCommitments.Where(x => x.ProjectID == id).Select(i => new {
                i.ProjectCommitmentID,
                i.ProjectID,
                i.ProjectCommitmentName,
                i.CommitmentNumber,
                i.Responsible,
                i.TaskStatusID,
                i.PlannedDate,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectCommitmentID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projectcommitments, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectCommitment();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ProjectCommitmentID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectCommitments.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectCommitmentID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectCommitments.FirstOrDefaultAsync(item => item.ProjectCommitmentID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(Guid key) {
            var model = await _context.ProjectCommitments.FirstOrDefaultAsync(item => item.ProjectCommitmentID == key);

            _context.ProjectCommitments.Remove(model);
            await _context.SaveChangesAsync();
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
        public async Task<IActionResult> ProjectTaskStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ProjectTaskStatus
                         orderby i.StatusName
                         select new {
                             Value = i.ProjectTaskStatusID,
                             Text = i.StatusName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(ProjectCommitment model, IDictionary values) {
            string PROJECT_COMMITMENT_ID = nameof(ProjectCommitment.ProjectCommitmentID);
            string PROJECT_ID = nameof(ProjectCommitment.ProjectID);
            string PROJECT_COMMITMENT_NAME = nameof(ProjectCommitment.ProjectCommitmentName);
            string COMMITMENT_NUMBER = nameof(ProjectCommitment.CommitmentNumber);
            string RESPONSIBLE = nameof(ProjectCommitment.Responsible);
            string TASK_STATUS_ID = nameof(ProjectCommitment.TaskStatusID);
            string PLANNED_DATE = nameof(ProjectCommitment.PlannedDate);
            string IS_ACTIVE = nameof(ProjectCommitment.IsActive);
            string CREATED_BY = nameof(ProjectCommitment.CreatedBy);
            string CREATED = nameof(ProjectCommitment.Created);
            string MODIFIED_BY = nameof(ProjectCommitment.ModifiedBy);
            string MODIFIED = nameof(ProjectCommitment.Modified);

            if(values.Contains(PROJECT_COMMITMENT_ID)) {
                model.ProjectCommitmentID = ConvertTo<System.Guid>(values[PROJECT_COMMITMENT_ID]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = ConvertTo<System.Guid>(values[PROJECT_ID]);
            }

            if(values.Contains(PROJECT_COMMITMENT_NAME)) {
                model.ProjectCommitmentName = Convert.ToString(values[PROJECT_COMMITMENT_NAME]);
            }

            if(values.Contains(COMMITMENT_NUMBER)) {
                model.CommitmentNumber = Convert.ToInt32(values[COMMITMENT_NUMBER]);
            }

            if(values.Contains(RESPONSIBLE)) {
                model.Responsible = Convert.ToString(values[RESPONSIBLE]);
            }

            if(values.Contains(TASK_STATUS_ID)) {
                model.TaskStatusID = values[TASK_STATUS_ID] != null ? ConvertTo<System.Guid>(values[TASK_STATUS_ID]) : (Guid?)null;
            }

            if(values.Contains(PLANNED_DATE)) {
                model.PlannedDate = Convert.ToDateTime(values[PLANNED_DATE]);
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