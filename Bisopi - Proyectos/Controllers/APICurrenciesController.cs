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
    public class APICurrenciesController : Controller
    {
        private DataContext _context;

        public APICurrenciesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var currencies = _context.Currencies.Select(i => new {
                i.CurrencyID,
                i.CurrencyName,
                i.Abbreviation,
                i.CurrencyNameAndAbbreviation,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CurrencyID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(currencies, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            if (loadOptions.Filter != null && loadOptions.Filter.Count > 0 && loadOptions.Filter[0].ToString().Contains("CurrencyNameAndAbbreviation"))
            {

                var sample = loadOptions.Filter[0].ToString();
                var data = $"[{sample.Replace("CurrencyNameAndAbbreviation", "CurrencyName")}, \"or\" ,{sample.Replace("CurrencyNameAndAbbreviation", "Abbreviation")}]";
                var filter = JsonConvert.DeserializeObject(data) as IList;
                loadOptions.Filter = filter;
            }

            var currencies = _context.Currencies.Where(x => x.IsActive).Select(i => new {
                i.CurrencyID,
                i.CurrencyName,
                i.Abbreviation,
                i.CurrencyNameAndAbbreviation
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CurrencyID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(currencies, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Currency();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.CurrencyID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Currencies.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CurrencyID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Currencies.FirstOrDefaultAsync(item => item.CurrencyID == key);
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
            var model = await _context.Currencies.FirstOrDefaultAsync(item => item.CurrencyID == key);

            _context.Currencies.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Currency model, IDictionary values) {
            string CURRENCY_ID = nameof(Currency.CurrencyID);
            string CURRENCY_NAME = nameof(Currency.CurrencyName);
            string ABBREVIATION = nameof(Currency.Abbreviation);
            string CURRENCY_NAME_AND_ABBREVIATION = nameof(Currency.CurrencyNameAndAbbreviation);
            string IS_ACTIVE = nameof(Currency.IsActive);
            string CREATED_BY = nameof(Currency.CreatedBy);
            string CREATED = nameof(Currency.Created);
            string MODIFIED_BY = nameof(Currency.ModifiedBy);
            string MODIFIED = nameof(Currency.Modified);

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = ConvertTo<System.Guid>(values[CURRENCY_ID]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
            }

            if (values.Contains(CURRENCY_NAME))
            {
                model.CurrencyName = Convert.ToString(values[CURRENCY_NAME]);
            }

            if (values.Contains(CURRENCY_NAME_AND_ABBREVIATION))
            {
                model.CurrencyNameAndAbbreviation = Convert.ToString(values[CURRENCY_NAME_AND_ABBREVIATION]);
            }

            if (values.Contains(IS_ACTIVE)) {
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