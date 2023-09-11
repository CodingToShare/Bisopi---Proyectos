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
    public class APIResourcePlanningRealsController : Controller
    {
        private DataContext _context;

        public APIResourcePlanningRealsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var resourceplanningreals = _context.ResourcePlanningReals.Where(x => x.ProjectID == id && x.IsActive).Select(i => new {
                i.ResourcePlanningRealID,
                i.ProjectID,
                i.DateAnalysis,
                i.ResourceID,
                i.PositionID,
                i.PlannedHours,
                i.PercentComplete,
                i.ExpectedPercentage,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ResourcePlanningRealID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(resourceplanningreals, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ResourcePlanningReal();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ResourcePlanningRealID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ResourcePlanningReals.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ResourcePlanningRealID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ResourcePlanningReals.FirstOrDefaultAsync(item => item.ResourcePlanningRealID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.PlannedHours = 0;

            var tasks = _context.ProjectTask.Where(x => x.ProjectID == model.ProjectID && x.ResponsibleID == model.ResourceID).ToList();

            foreach (var task in tasks)
            {
                var taskRegisters = _context.TaskRegistry.Where(x => x.ProjectTaskID == task.TaskID && x.RegistryDate <= model.DateAnalysis).ToList();

                foreach (var taskRegister in taskRegisters)
                {
                    model.PlannedHours = model.PlannedHours + (double)taskRegister.ExecutionTime / 3600;
                }
            }

            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(Guid key) {
            var model = await _context.ResourcePlanningReals.FirstOrDefaultAsync(item => item.ResourcePlanningRealID == key);

            model.IsActive = false;

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

        private void PopulateModel(ResourcePlanningReal model, IDictionary values) {
            string RESOURCE_PLANNING_REAL_ID = nameof(ResourcePlanningReal.ResourcePlanningRealID);
            string PROJECT_ID = nameof(ResourcePlanningReal.ProjectID);
            string DATE_ANALYSIS = nameof(ResourcePlanningReal.DateAnalysis);
            string RESOURCE_ID = nameof(ResourcePlanningReal.ResourceID);
            string POSITION_ID = nameof(ResourcePlanningReal.PositionID);
            string PLANNED_HOURS = nameof(ResourcePlanningReal.PlannedHours);
            string PERCENT_COMPLETE = nameof(ResourcePlanningReal.PercentComplete);
            string EXPECTED_PERCENTAGE = nameof(ResourcePlanningReal.ExpectedPercentage);
            string IS_ACTIVE = nameof(ResourcePlanningReal.IsActive);
            string CREATED_BY = nameof(ResourcePlanningReal.CreatedBy);
            string CREATED = nameof(ResourcePlanningReal.Created);
            string MODIFIED_BY = nameof(ResourcePlanningReal.ModifiedBy);
            string MODIFIED = nameof(ResourcePlanningReal.Modified);

            if(values.Contains(RESOURCE_PLANNING_REAL_ID)) {
                model.ResourcePlanningRealID = ConvertTo<System.Guid>(values[RESOURCE_PLANNING_REAL_ID]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = values[PROJECT_ID] != null ? ConvertTo<System.Guid>(values[PROJECT_ID]) : (Guid?)null;
            }

            if(values.Contains(DATE_ANALYSIS)) {
                model.DateAnalysis = values[DATE_ANALYSIS] != null ? Convert.ToDateTime(values[DATE_ANALYSIS]) : (DateTime?)null;
            }

            if(values.Contains(RESOURCE_ID)) {
                model.ResourceID = values[RESOURCE_ID] != null ? ConvertTo<System.Guid>(values[RESOURCE_ID]) : (Guid?)null;
            }

            if(values.Contains(POSITION_ID)) {
                model.PositionID = values[POSITION_ID] != null ? ConvertTo<System.Guid>(values[POSITION_ID]) : (Guid?)null;
            }

            if(values.Contains(PLANNED_HOURS)) {
                model.PlannedHours = values[PLANNED_HOURS] != null ? Convert.ToDouble(values[PLANNED_HOURS], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(PERCENT_COMPLETE)) {
                model.PercentComplete = values[PERCENT_COMPLETE] != null ? Convert.ToDouble(values[PERCENT_COMPLETE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(EXPECTED_PERCENTAGE)) {
                model.ExpectedPercentage = values[EXPECTED_PERCENTAGE] != null ? Convert.ToDouble(values[EXPECTED_PERCENTAGE], CultureInfo.InvariantCulture) : (double?)null;
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