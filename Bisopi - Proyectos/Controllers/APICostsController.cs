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
    public class APICostsController : Controller
    {
        private DataContext _context;

        public APICostsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var costs = _context.Costs.Where(x => x.UserID == id && x.IsActive).Select(i => new {
                i.CostID,
                i.UserID,
                i.SeniorityID,
                i.DateFrom,
                i.DateUntil,
                i.ProjectValue,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CostID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(costs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Cost();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.SeniorityID = Guid.NewGuid();
            model.IsActive = true;
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Costs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CostID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Costs.FirstOrDefaultAsync(item => item.CostID == key);
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
            var model = await _context.Costs.FirstOrDefaultAsync(item => item.CostID == key);

            model.IsActive = false;

            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> SenioritiesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Seniorities
                         orderby i.SeniorityName
                         select new {
                             Value = i.SeniorityID,
                             Text = i.SeniorityName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Cost model, IDictionary values) {
            string COST_ID = nameof(Cost.CostID);
            string USER_ID = nameof(Cost.UserID);
            string SENIORITY_ID = nameof(Cost.SeniorityID);
            string DATE_FROM = nameof(Cost.DateFrom);
            string DATE_UNTIL = nameof(Cost.DateUntil);
            string PROJECT_VALUE = nameof(Cost.ProjectValue);
            string IS_ACTIVE = nameof(Cost.IsActive);
            string CREATED_BY = nameof(Cost.CreatedBy);
            string CREATED = nameof(Cost.Created);
            string MODIFIED_BY = nameof(Cost.ModifiedBy);
            string MODIFIED = nameof(Cost.Modified);

            if(values.Contains(COST_ID)) {
                model.CostID = ConvertTo<System.Guid>(values[COST_ID]);
            }

            if(values.Contains(USER_ID)) {
                model.UserID = values[USER_ID] != null ? ConvertTo<System.Guid>(values[USER_ID]) : (Guid?)null;
            }

            if(values.Contains(SENIORITY_ID)) {
                model.SeniorityID = values[SENIORITY_ID] != null ? ConvertTo<System.Guid>(values[SENIORITY_ID]) : (Guid?)null;
            }

            if(values.Contains(DATE_FROM)) {
                model.DateFrom = values[DATE_FROM] != null ? Convert.ToDateTime(values[DATE_FROM]) : (DateTime?)null;
            }

            if(values.Contains(DATE_UNTIL)) {
                model.DateUntil = values[DATE_UNTIL] != null ? Convert.ToDateTime(values[DATE_UNTIL]) : (DateTime?)null;
            }

            if(values.Contains(PROJECT_VALUE)) {
                model.ProjectValue = values[PROJECT_VALUE] != null ? Convert.ToDouble(values[PROJECT_VALUE], CultureInfo.InvariantCulture) : (double?)null;
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