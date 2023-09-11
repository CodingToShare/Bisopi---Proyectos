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
    public class APIMilestonesController : Controller
    {
        private DataContext _context;

        public APIMilestonesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var milestones = _context.Milestones.Where(x => x.ProjectID == id && x.IsActive).ToList();

            foreach (var item in milestones)
            {
                var modelProject = _context.Projects.AsNoTracking().Where(x => x.ProjectID == item.ProjectID).FirstOrDefault();
                var modelRetention = _context.RetentionPercentages.AsNoTracking().Where(x => x.CountryID == modelProject.CountryID && x.IsActive && (x.EffectiveStartDate < DateTime.UtcNow.AddHours(-5) || x.ValidEndDate > DateTime.UtcNow.AddHours(-5))).FirstOrDefault();
                var modelTRM = _context.RepresentativeMarketRates.AsNoTracking().Where(x => x.CurrencyID == item.CurrencyID && x.IsActive).FirstOrDefault();

                item.Retention = modelRetention.Retention;
                if(item.Value != null)
                {
                    item.RetentionValue = item.Value * (modelRetention.Retention/100);
                    item.TotalBill = item.Value * (modelRetention.Retention/100);
                    item.TRM = modelProject.TRMProject;
                    if (item.CurrencyID != null)
                    {
                        item.SubTotalBillCOP = modelTRM.ProjectedRm * item.Value;
                        item.RetentionValueCOP = item.SubTotalBillCOP * (modelRetention.Retention / 100);
                        item.ValueAddedTax = (modelRetention.ValueAddedTax * item.SubTotalBillCOP) / 100;
                        item.ValueAddedTaxWuthholding = (modelRetention.ValueAddedTax * item.ValueAddedTax) / 100;
                        item.TotalBillCOP = item.SubTotalBillCOP - item.RetentionValueCOP + item.ValueAddedTax - item.ValueAddedTaxWuthholding;
                    }
                    

                }
                else
                {
                    item.TRM = modelProject.TRMProject;
                }

            }

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "MilestoneID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(DataSourceLoader.Load(milestones, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Milestone();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.MilestoneID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Milestones.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MilestoneID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Milestones.FirstOrDefaultAsync(item => item.MilestoneID == key);
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
            var model = await _context.Milestones.FirstOrDefaultAsync(item => item.MilestoneID == key);

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

        [HttpGet]
        public async Task<IActionResult> CurrenciesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Currencies
                         orderby i.CurrencyName
                         select new
                         {
                             Value = i.CurrencyID,
                             Text = i.CurrencyName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Milestone model, IDictionary values) {
            string MILESTONE_ID = nameof(Milestone.MilestoneID);
            string PROJECT_ID = nameof(Milestone.ProjectID);
            string DEAL_ID = nameof(Milestone.DealID);
            string LEAD_ID = nameof(Milestone.LeadID);
            string MILESTONE_DATE = nameof(Milestone.MilestoneDate);
            string CURRENCY_ID = nameof(Milestone.CurrencyID);
            string PERCENTAGE = nameof(Milestone.Percentage);
            string VALUE = nameof(Milestone.Value);
            string MILESTONE_NUMBER = nameof(Milestone.MilestoneNumber);
            string IS_IT_CHANGE_CONTROL = nameof(Milestone.IsItChangeControl);
            string HOURS = nameof(Milestone.Hours);
            string COMMENT = nameof(Milestone.Comment);
            string IS_ACTIVE = nameof(Milestone.IsActive);
            string CREATED_BY = nameof(Milestone.CreatedBy);
            string CREATED = nameof(Milestone.Created);
            string MODIFIED_BY = nameof(Milestone.ModifiedBy);
            string MODIFIED = nameof(Milestone.Modified);

            if(values.Contains(MILESTONE_ID)) {
                model.MilestoneID = ConvertTo<System.Guid>(values[MILESTONE_ID]);
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

            if(values.Contains(HOURS)) {
                model.Hours = Convert.ToInt32(values[HOURS]);
            }

            if(values.Contains(IS_IT_CHANGE_CONTROL)) {
                model.IsItChangeControl = Convert.ToBoolean(values[IS_IT_CHANGE_CONTROL]);
            }

            if (values.Contains(MILESTONE_NUMBER))
            {
                model.MilestoneNumber = Convert.ToInt32(values[MILESTONE_NUMBER]);
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