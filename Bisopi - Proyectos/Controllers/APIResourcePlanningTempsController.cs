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
using Bisopi___Proyectos.ModelsTemps;

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIResourcePlanningTempsController : Controller
    {
        private DataContext _context;

        public APIResourcePlanningTempsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var resourcesplanningstemps = _context.ResourcesPlanningsTemps.Where(x => x.ProjectID == id || x.DealID == id || x.LeadID == id).Select(i => new {
                i.ResourcePlanningTempID,
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
            // loadOptions.PrimaryKey = new[] { "ResourcePlanningTempID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(resourcesplanningstemps, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ResourcePlanningTemp();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ResourcePlanningTempID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ResourcesPlanningsTemps.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ResourcePlanningTempID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ResourcesPlanningsTemps.FirstOrDefaultAsync(item => item.ResourcePlanningTempID == key);
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
            var model = await _context.ResourcesPlanningsTemps.FirstOrDefaultAsync(item => item.ResourcePlanningTempID == key);

            _context.ResourcesPlanningsTemps.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ResourcePlanningTemp model, IDictionary values) {
            string RESOURCE_PLANNING_TEMP_ID = nameof(ResourcePlanningTemp.ResourcePlanningTempID);
            string PROJECT_ID = nameof(ResourcePlanningTemp.ProjectID);
            string DEAL_ID = nameof(ResourcePlanningTemp.DealID);
            string LEAD_ID = nameof(ResourcePlanningTemp.LeadID);
            string RESOURCE_ID = nameof(ResourcePlanningTemp.ResourceID);
            string POSITION_ID = nameof(ResourcePlanningTemp.PositionID);
            string PLANNED_HOURS = nameof(ResourcePlanningTemp.PlannedHours);
            string ETC_HOUR = nameof(ResourcePlanningTemp.EtcHour);
            string IS_ACTIVE = nameof(ResourcePlanningTemp.IsActive);
            string CREATED_BY = nameof(ResourcePlanningTemp.CreatedBy);
            string CREATED = nameof(ResourcePlanningTemp.Created);
            string MODIFIED_BY = nameof(ResourcePlanningTemp.ModifiedBy);
            string MODIFIED = nameof(ResourcePlanningTemp.Modified);

            if(values.Contains(RESOURCE_PLANNING_TEMP_ID)) {
                model.ResourcePlanningTempID = ConvertTo<System.Guid>(values[RESOURCE_PLANNING_TEMP_ID]);
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