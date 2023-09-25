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
    public class APIProjectTrackingsController : Controller
    {
        private DataContext _context;

        public APIProjectTrackingsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var projecttrackings = _context.ProjectTrackings.Where(x => x.ProjectID == id && x.IsActive).Select(i => new {
                i.ProjectTrackingID,
                i.ProjectID,
                i.MonitoringDate,
                i.PlannedPercentage,
                i.RealPercentage,
                i.Comments,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectTrackingID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projecttrackings, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectTracking();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ProjectTrackingID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectTrackings.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectTrackingID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectTrackings.FirstOrDefaultAsync(item => item.ProjectTrackingID == key);
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
            var model = await _context.ProjectTrackings.FirstOrDefaultAsync(item => item.ProjectTrackingID == key);

            _context.ProjectTrackings.Remove(model);
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

        private void PopulateModel(ProjectTracking model, IDictionary values) {
            string PROJECT_TRACKING_ID = nameof(ProjectTracking.ProjectTrackingID);
            string PROJECT_ID = nameof(ProjectTracking.ProjectID);
            string MONITORING_DATE = nameof(ProjectTracking.MonitoringDate);
            string PLANNED_PERCENTAGE = nameof(ProjectTracking.PlannedPercentage);
            string REAL_PERCENTAGE = nameof(ProjectTracking.RealPercentage);
            string COMMENTS = nameof(ProjectTracking.Comments);
            string IS_ACTIVE = nameof(ProjectTracking.IsActive);
            string CREATED_BY = nameof(ProjectTracking.CreatedBy);
            string CREATED = nameof(ProjectTracking.Created);
            string MODIFIED_BY = nameof(ProjectTracking.ModifiedBy);
            string MODIFIED = nameof(ProjectTracking.Modified);

            if(values.Contains(PROJECT_TRACKING_ID)) {
                model.ProjectTrackingID = ConvertTo<System.Guid>(values[PROJECT_TRACKING_ID]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = values[PROJECT_ID] != null ? ConvertTo<System.Guid>(values[PROJECT_ID]) : (Guid?)null;
            }

            if(values.Contains(MONITORING_DATE)) {
                model.MonitoringDate = values[MONITORING_DATE] != null ? Convert.ToDateTime(values[MONITORING_DATE]) : (DateTime?)null;
            }

            if(values.Contains(PLANNED_PERCENTAGE)) {
                model.PlannedPercentage = values[PLANNED_PERCENTAGE] != null ? Convert.ToDouble(values[PLANNED_PERCENTAGE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(REAL_PERCENTAGE)) {
                model.RealPercentage = values[REAL_PERCENTAGE] != null ? Convert.ToDouble(values[REAL_PERCENTAGE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(COMMENTS)) {
                model.Comments = Convert.ToString(values[COMMENTS]);
            }

            if(values.Contains(IS_ACTIVE)) {
                model.IsActive = Convert.ToBoolean(values[IS_ACTIVE]);
            }

            if(values.Contains(CREATED_BY)) {
                model.CreatedBy = Convert.ToString(values[CREATED_BY]);
            }

            if(values.Contains(CREATED)) {
                model.Created = Convert.ToDateTime(values[CREATED]);
            }

            if(values.Contains(MODIFIED_BY)) {
                model.ModifiedBy = Convert.ToString(values[MODIFIED_BY]);
            }

            if(values.Contains(MODIFIED)) {
                model.Modified = Convert.ToDateTime(values[MODIFIED]);
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