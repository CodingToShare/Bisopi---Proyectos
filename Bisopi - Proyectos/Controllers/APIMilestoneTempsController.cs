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
using Bisopi___Proyectos.Models;

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIMilestoneTempsController : Controller
    {
        private DataContext _context;

        public APIMilestoneTempsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var milestonestemps = _context.MilestonesTemps.Where(x => x.ProjectID == id || x.DealID == id || x.LeadID == id).Select(i => new {
                i.MilestoneTempID,
                i.ProjectID,
                i.DealID,
                i.LeadID,
                i.MilestoneDate,
                i.CurrencyID,
                i.Percentage,
                i.Value,
                i.MilestoneNumber,
                i.IsItChangeControl,
                i.Hours,
                i.Comment,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "MilestoneTempID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(milestonestemps, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new MilestoneTemp();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.MilestoneTempID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.MilestonesTemps.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MilestoneTempID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.MilestonesTemps.FirstOrDefaultAsync(item => item.MilestoneTempID == key);
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
            var model = await _context.MilestonesTemps.FirstOrDefaultAsync(item => item.MilestoneTempID == key);

            _context.MilestonesTemps.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CurrenciesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Currencies
                         orderby i.CurrencyName
                         select new {
                             Value = i.CurrencyID,
                             Text = i.CurrencyName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(MilestoneTemp model, IDictionary values) {
            string MILESTONE_TEMP_ID = nameof(MilestoneTemp.MilestoneTempID);
            string PROJECT_ID = nameof(MilestoneTemp.ProjectID);
            string DEAL_ID = nameof(MilestoneTemp.DealID);
            string LEAD_ID = nameof(MilestoneTemp.LeadID);
            string MILESTONE_DATE = nameof(MilestoneTemp.MilestoneDate);
            string CURRENCY_ID = nameof(MilestoneTemp.CurrencyID);
            string PERCENTAGE = nameof(MilestoneTemp.Percentage);
            string VALUE = nameof(MilestoneTemp.Value);
            string MILESTONE_NUMBER = nameof(MilestoneTemp.MilestoneNumber);
            string IS_IT_CHANGE_CONTROL = nameof(MilestoneTemp.IsItChangeControl);
            string HOURS = nameof(Milestone.Hours);
            string COMMENT = nameof(MilestoneTemp.Comment);
            string IS_ACTIVE = nameof(MilestoneTemp.IsActive);
            string CREATED_BY = nameof(MilestoneTemp.CreatedBy);
            string CREATED = nameof(MilestoneTemp.Created);
            string MODIFIED_BY = nameof(MilestoneTemp.ModifiedBy);
            string MODIFIED = nameof(MilestoneTemp.Modified);

            if(values.Contains(MILESTONE_TEMP_ID)) {
                model.MilestoneTempID = ConvertTo<System.Guid>(values[MILESTONE_TEMP_ID]);
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

            if(values.Contains(MILESTONE_DATE)) {
                model.MilestoneDate = Convert.ToDateTime(values[MILESTONE_DATE]);
            }

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = values[CURRENCY_ID] != null ? ConvertTo<System.Guid>(values[CURRENCY_ID]) : (Guid?)null;
            }

            if(values.Contains(PERCENTAGE)) {
                model.Percentage = Convert.ToInt32(values[PERCENTAGE]);
            }

            if(values.Contains(VALUE)) {
                model.Value = values[VALUE] != null ? Convert.ToDouble(values[VALUE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(MILESTONE_NUMBER)) {
                model.MilestoneNumber = Convert.ToInt32(values[MILESTONE_NUMBER]);
            }

            if(values.Contains(IS_IT_CHANGE_CONTROL)) {
                model.IsItChangeControl = Convert.ToBoolean(values[IS_IT_CHANGE_CONTROL]);
            }

            if (values.Contains(HOURS))
            {
                model.Hours = Convert.ToInt32(values[HOURS]);
            }

            if (values.Contains(COMMENT)) {
                model.Comment = Convert.ToString(values[COMMENT]);
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