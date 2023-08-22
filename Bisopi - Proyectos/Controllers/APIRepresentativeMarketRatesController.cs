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
    public class APIRepresentativeMarketRatesController : Controller
    {
        private DataContext _context;

        public APIRepresentativeMarketRatesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var representativemarketrates = _context.RepresentativeMarketRates.Select(i => new {
                i.RepresentativeMarketRateID,
                i.ProjectedRm,
                i.CurrencyID,
                i.Year,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "RepresentativeMarketRateID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(representativemarketrates, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RepresentativeMarketRate();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.RepresentativeMarketRateID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RepresentativeMarketRates.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.RepresentativeMarketRateID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.RepresentativeMarketRates.FirstOrDefaultAsync(item => item.RepresentativeMarketRateID == key);
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
            var model = await _context.RepresentativeMarketRates.FirstOrDefaultAsync(item => item.RepresentativeMarketRateID == key);

            _context.RepresentativeMarketRates.Remove(model);
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

        private void PopulateModel(RepresentativeMarketRate model, IDictionary values) {
            string REPRESENTATIVE_MARKET_RATE_ID = nameof(RepresentativeMarketRate.RepresentativeMarketRateID);
            string PROJECTED_RM = nameof(RepresentativeMarketRate.ProjectedRm);
            string CURRENCY_ID = nameof(RepresentativeMarketRate.CurrencyID);
            string YEAR = nameof(RepresentativeMarketRate.Year);
            string IS_ACTIVE = nameof(RepresentativeMarketRate.IsActive);
            string CREATED_BY = nameof(RepresentativeMarketRate.CreatedBy);
            string CREATED = nameof(RepresentativeMarketRate.Created);
            string MODIFIED_BY = nameof(RepresentativeMarketRate.ModifiedBy);
            string MODIFIED = nameof(RepresentativeMarketRate.Modified);

            if(values.Contains(REPRESENTATIVE_MARKET_RATE_ID)) {
                model.RepresentativeMarketRateID = ConvertTo<System.Guid>(values[REPRESENTATIVE_MARKET_RATE_ID]);
            }

            if(values.Contains(PROJECTED_RM)) {
                model.ProjectedRm = values[PROJECTED_RM] != null ? Convert.ToDouble(values[PROJECTED_RM], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = values[CURRENCY_ID] != null ? ConvertTo<System.Guid>(values[CURRENCY_ID]) : (Guid?)null;
            }

            if(values.Contains(YEAR)) {
                model.Year = values[YEAR] != null ? Convert.ToInt32(values[YEAR]) : (int?)null;
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