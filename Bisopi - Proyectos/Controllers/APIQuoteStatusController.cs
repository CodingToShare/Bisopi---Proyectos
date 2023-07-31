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
    public class APIQuoteStatusController : Controller
    {
        private DataContext _context;

        public APIQuoteStatusController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var quotesstatus = _context.QuotesStatus.Select(i => new {
                i.QuoteStatusID,
                i.QuoteStatusName,
                i.Abbreviation,
                i.QuoteStatusNameAndAbbreviation,
                i.Order,
                i.Color,
                i.Visible,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            }).OrderBy(x => x.Order);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "QuoteStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(quotesstatus, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            if (loadOptions.Filter != null && loadOptions.Filter.Count > 0 && loadOptions.Filter[0].ToString().Contains("QuoteStatusNameAndAbbreviation"))
            {

                var sample = loadOptions.Filter[0].ToString();
                var data = $"[{sample.Replace("QuoteStatusNameAndAbbreviation", "QuoteStatusName")}, \"or\" ,{sample.Replace("QuoteStatusNameAndAbbreviation", "Abbreviation")}]";
                var filter = JsonConvert.DeserializeObject(data) as IList;
                loadOptions.Filter = filter;
            }

            var quotesstatus = _context.QuotesStatus.Select(i => new {
                i.QuoteStatusID,
                i.QuoteStatusName,
                i.Abbreviation,
                i.QuoteStatusNameAndAbbreviation,

            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "QuoteStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(quotesstatus, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new QuoteStatus();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.QuoteStatusID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.QuotesStatus.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.QuoteStatusID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.QuotesStatus.FirstOrDefaultAsync(item => item.QuoteStatusID == key);
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
            var model = await _context.QuotesStatus.FirstOrDefaultAsync(item => item.QuoteStatusID == key);

            _context.QuotesStatus.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(QuoteStatus model, IDictionary values) {
            string QUOTE_STATUS_ID = nameof(QuoteStatus.QuoteStatusID);
            string QUOTE_STATUS_NAME = nameof(QuoteStatus.QuoteStatusName);
            string ABBREVIATION = nameof(QuoteStatus.Abbreviation);
            string QUOTE_STATUS_NAME_AND_ABBREVIATION = nameof(QuoteStatus.QuoteStatusNameAndAbbreviation);
            string ORDER = nameof(QuoteStatus.Order);
            string COLOR = nameof(QuoteStatus.Color);
            string VISIBLE = nameof(QuoteStatus.Visible);
            string IS_ACTIVE = nameof(QuoteStatus.IsActive);
            string CREATED_BY = nameof(QuoteStatus.CreatedBy);
            string CREATED = nameof(QuoteStatus.Created);
            string MODIFIED_BY = nameof(QuoteStatus.ModifiedBy);
            string MODIFIED = nameof(QuoteStatus.Modified);

            if(values.Contains(QUOTE_STATUS_ID)) {
                model.QuoteStatusID = ConvertTo<System.Guid>(values[QUOTE_STATUS_ID]);
            }

            if(values.Contains(QUOTE_STATUS_NAME)) {
                model.QuoteStatusName = Convert.ToString(values[QUOTE_STATUS_NAME]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
            }

            if(values.Contains(QUOTE_STATUS_NAME_AND_ABBREVIATION)) {
                model.QuoteStatusNameAndAbbreviation = Convert.ToString(values[QUOTE_STATUS_NAME_AND_ABBREVIATION]);
            }

            if(values.Contains(ORDER)) {
                model.Order = Convert.ToInt32(values[ORDER]);
            }

            if(values.Contains(COLOR)) {
                model.Color = Convert.ToString(values[COLOR]);
            }

            if(values.Contains(VISIBLE)) {
                model.Visible = Convert.ToBoolean(values[VISIBLE]);
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