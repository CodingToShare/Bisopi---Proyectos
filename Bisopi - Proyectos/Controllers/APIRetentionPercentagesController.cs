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
    public class APIRetentionPercentagesController : Controller
    {
        private DataContext _context;

        public APIRetentionPercentagesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var retentionpercentages = _context.RetentionPercentages.Select(i => new {
                i.RetentionPercentageID,
                i.CountryID,
                i.Retention,
                i.ValueAddedTax,
                i.ValueAddedTaxWuthholding,
                i.EffectiveStartDate,
                i.ValidEndDate,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "RetentionPercentageID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(retentionpercentages, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RetentionPercentage();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.RetentionPercentageID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RetentionPercentages.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.RetentionPercentageID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.RetentionPercentages.FirstOrDefaultAsync(item => item.RetentionPercentageID == key);
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
            var model = await _context.RetentionPercentages.FirstOrDefaultAsync(item => item.RetentionPercentageID == key);

            _context.RetentionPercentages.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> CountriesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Countries
                         orderby i.CountryName
                         select new {
                             Value = i.CountryID,
                             Text = i.CountryName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(RetentionPercentage model, IDictionary values) {
            string RETENTION_PERCENTAGE_ID = nameof(RetentionPercentage.RetentionPercentageID);
            string COUNTRY_ID = nameof(RetentionPercentage.CountryID);
            string RETENTION = nameof(RetentionPercentage.Retention);
            string VALUE_ADDED_TAX = nameof(RetentionPercentage.ValueAddedTax);
            string VALUE_ADDED_TAX_WUTHHOLDING = nameof(RetentionPercentage.ValueAddedTaxWuthholding);
            string EFFECTIVE_START_DATE = nameof(RetentionPercentage.EffectiveStartDate);
            string VALID_END_DATE = nameof(RetentionPercentage.ValidEndDate);
            string IS_ACTIVE = nameof(RetentionPercentage.IsActive);
            string CREATED_BY = nameof(RetentionPercentage.CreatedBy);
            string CREATED = nameof(RetentionPercentage.Created);
            string MODIFIED_BY = nameof(RetentionPercentage.ModifiedBy);
            string MODIFIED = nameof(RetentionPercentage.Modified);

            if(values.Contains(RETENTION_PERCENTAGE_ID)) {
                model.RetentionPercentageID = ConvertTo<System.Guid>(values[RETENTION_PERCENTAGE_ID]);
            }

            if(values.Contains(COUNTRY_ID)) {
                model.CountryID = values[COUNTRY_ID] != null ? ConvertTo<System.Guid>(values[COUNTRY_ID]) : (Guid?)null;
            }

            if(values.Contains(RETENTION)) {
                model.Retention = values[RETENTION] != null ? Convert.ToDouble(values[RETENTION], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(VALUE_ADDED_TAX)) {
                model.ValueAddedTax = values[VALUE_ADDED_TAX] != null ? Convert.ToDouble(values[VALUE_ADDED_TAX], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(VALUE_ADDED_TAX_WUTHHOLDING)) {
                model.ValueAddedTaxWuthholding = values[VALUE_ADDED_TAX_WUTHHOLDING] != null ? Convert.ToDouble(values[VALUE_ADDED_TAX_WUTHHOLDING], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(EFFECTIVE_START_DATE)) {
                model.EffectiveStartDate = values[EFFECTIVE_START_DATE] != null ? Convert.ToDateTime(values[EFFECTIVE_START_DATE]) : (DateTime?)null;
            }

            if(values.Contains(VALID_END_DATE)) {
                model.ValidEndDate = values[VALID_END_DATE] != null ? Convert.ToDateTime(values[VALID_END_DATE]) : (DateTime?)null;
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