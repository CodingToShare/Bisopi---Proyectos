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
    public class APICountriesController : Controller
    {
        private DataContext _context;

        public APICountriesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var countries = _context.Countries.Select(i => new {
                i.CountryID,
                i.CountryName,
                i.Abbreviation,
                i.CountryNameAndAbbreviation,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CountryID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(countries, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            if (loadOptions.Filter != null && loadOptions.Filter.Count > 0 && loadOptions.Filter[0].ToString().Contains("CountryNameAndAbbreviation"))
            {

                var sample = loadOptions.Filter[0].ToString();
                var data = $"[{sample.Replace("CountryNameAndAbbreviation", "CountryName")}, \"or\" ,{sample.Replace("CountryNameAndAbbreviation", "Abbreviation")}]";
                var filter = JsonConvert.DeserializeObject(data) as IList;
                loadOptions.Filter = filter;
            }

            var countries = _context.Countries.Where(x => x.IsActive).Select(i => new {
                i.CountryID,
                i.CountryName,
                i.Abbreviation,
                i.CountryNameAndAbbreviation
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CountryID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(countries, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Country();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.CountryID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Countries.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CountryID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Countries.FirstOrDefaultAsync(item => item.CountryID == key);
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
            var model = await _context.Countries.FirstOrDefaultAsync(item => item.CountryID == key);

            _context.Countries.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Country model, IDictionary values) {
            string COUNTRY_ID = nameof(Country.CountryID);
            string COUNTRY_NAME = nameof(Country.CountryName);
            string ABBREVIATION = nameof(Country.Abbreviation);
            string COUNTRY_NAME_AND_ABBREVIATION = nameof(Country.CountryNameAndAbbreviation);
            string IS_ACTIVE = nameof(Country.IsActive);
            string CREATED_BY = nameof(Country.CreatedBy);
            string CREATED = nameof(Country.Created);
            string MODIFIED_BY = nameof(Country.ModifiedBy);
            string MODIFIED = nameof(Country.Modified);

            if(values.Contains(COUNTRY_ID)) {
                model.CountryID = ConvertTo<System.Guid>(values[COUNTRY_ID]);
            }

            if(values.Contains(COUNTRY_NAME)) {
                model.CountryName = Convert.ToString(values[COUNTRY_NAME]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
            }

            if(values.Contains(COUNTRY_NAME_AND_ABBREVIATION)) {
                model.CountryNameAndAbbreviation = Convert.ToString(values[COUNTRY_NAME_AND_ABBREVIATION]);
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