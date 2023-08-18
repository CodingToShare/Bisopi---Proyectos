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
    public class APIResourcePlanningsController : Controller
    {
        private DataContext _context;

        public APIResourcePlanningsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var resourcesplannings = _context.ResourcesPlannings.Where(x => x.ProjectID == id && x.IsActive).Select(i => new {
                i.ResourcePlanningID,
                i.ProjectID,
                i.DealID,
                i.LeadID,
                i.ResourceID,
                i.PositionID,
                i.PlannedHours,
                i.EtcHour,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ResourcePlanningID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(resourcesplannings, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ResourcePlanning();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ResourcePlanningID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ResourcesPlannings.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ResourcePlanningID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ResourcesPlannings.FirstOrDefaultAsync(item => item.ResourcePlanningID == key);
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
            var model = await _context.ResourcesPlannings.FirstOrDefaultAsync(item => item.ResourcePlanningID == key);

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

        private void PopulateModel(ResourcePlanning model, IDictionary values) {
            string RESOURCE_PLANNING_ID = nameof(ResourcePlanning.ResourcePlanningID);
            string PROJECT_ID = nameof(ResourcePlanning.ProjectID);
            string DEAL_ID = nameof(ResourcePlanning.DealID);
            string LEAD_ID = nameof(ResourcePlanning.LeadID);
            string RESOURCE_ID = nameof(ResourcePlanning.ResourceID);
            string POSITION_ID = nameof(ResourcePlanning.PositionID);
            string PLANNED_HOURS = nameof(ResourcePlanning.PlannedHours);
            string ETC_HOUR = nameof(ResourcePlanning.EtcHour);
            string IS_ACTIVE = nameof(ResourcePlanning.IsActive);
            string CREATED_BY = nameof(ResourcePlanning.CreatedBy);
            string CREATED = nameof(ResourcePlanning.Created);
            string MODIFIED_BY = nameof(ResourcePlanning.ModifiedBy);
            string MODIFIED = nameof(ResourcePlanning.Modified);

            if(values.Contains(RESOURCE_PLANNING_ID)) {
                model.ResourcePlanningID = ConvertTo<System.Guid>(values[RESOURCE_PLANNING_ID]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = values[PROJECT_ID] != null ? ConvertTo<System.Guid>(values[PROJECT_ID]) : (Guid?)null;
            }

            if(values.Contains(DEAL_ID)) {
                model.DealID = values[DEAL_ID] != null ? ConvertTo<System.Guid>(values[DEAL_ID]) : (Guid?)null;
            }

            if(values.Contains(LEAD_ID)) {
                model.LeadID = values[LEAD_ID] != null ? ConvertTo<System.Guid>(values[LEAD_ID]) : (Guid?)null;
            }

            if(values.Contains(RESOURCE_ID)) {
                model.ResourceID = values[RESOURCE_ID] != null ? ConvertTo<System.Guid>(values[RESOURCE_ID]) : (Guid?)null;
            }

            if(values.Contains(POSITION_ID)) {
                model.PositionID = values[POSITION_ID] != null ? ConvertTo<System.Guid>(values[POSITION_ID]) : (Guid?)null;
            }

            if(values.Contains(PLANNED_HOURS)) {
                model.PlannedHours = Convert.ToDouble(values[PLANNED_HOURS], CultureInfo.InvariantCulture);
            }

            if(values.Contains(ETC_HOUR)) {
                model.EtcHour = values[ETC_HOUR] != null ? Convert.ToDouble(values[ETC_HOUR], CultureInfo.InvariantCulture) : (double?)null;
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