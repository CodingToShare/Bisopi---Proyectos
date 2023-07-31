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
    public class APILeadsController : Controller
    {
        private DataContext _context;

        public APILeadsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var leads = _context.Leads.Where(x => x.IsActive).Select(i => new {
                i.LeadID,
                i.LeadName,
                i.ClientID,
                i.ResponsibleClient,
                i.QuoteStatusID,
                i.CurrencyID,
                i.LeadValue,
                i.Comments,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "LeadID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(leads, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Lead();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Leads.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.LeadID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Leads.FirstOrDefaultAsync(item => item.LeadID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(Guid key) {
            var model = await _context.Leads.FirstOrDefaultAsync(item => item.LeadID == key);

            _context.Leads.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ClientsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Clients
                         orderby i.ClientName
                         select new {
                             Value = i.ClientID,
                             Text = i.ClientName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> QuotesStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.QuotesStatus
                         orderby i.QuoteStatusName
                         select new {
                             Value = i.QuoteStatusID,
                             Text = i.QuoteStatusName
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

        private void PopulateModel(Lead model, IDictionary values) {
            string LEAD_ID = nameof(Lead.LeadID);
            string LEAD_NAME = nameof(Lead.LeadName);
            string CLIENT_ID = nameof(Lead.ClientID);
            string RESPONSIBLE_CLIENT = nameof(Lead.ResponsibleClient);
            string QUOTE_STATUS_ID = nameof(Lead.QuoteStatusID);
            string CURRENCY_ID = nameof(Lead.CurrencyID);
            string LEAD_VALUE = nameof(Lead.LeadValue);
            string COMMENTS = nameof(Lead.Comments);
            string IS_ACTIVE = nameof(Lead.IsActive);
            string CREATED_BY = nameof(Lead.CreatedBy);
            string CREATED = nameof(Lead.Created);
            string MODIFIED_BY = nameof(Lead.ModifiedBy);
            string MODIFIED = nameof(Lead.Modified);

            if(values.Contains(LEAD_ID)) {
                model.LeadID = ConvertTo<System.Guid>(values[LEAD_ID]);
            }

            if(values.Contains(LEAD_NAME)) {
                model.LeadName = Convert.ToString(values[LEAD_NAME]);
            }

            if(values.Contains(CLIENT_ID)) {
                model.ClientID = ConvertTo<System.Guid>(values[CLIENT_ID]);
            }

            if(values.Contains(RESPONSIBLE_CLIENT)) {
                model.ResponsibleClient = Convert.ToString(values[RESPONSIBLE_CLIENT]);
            }

            if(values.Contains(QUOTE_STATUS_ID)) {
                model.QuoteStatusID = ConvertTo<System.Guid>(values[QUOTE_STATUS_ID]);
            }

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = values[CURRENCY_ID] != null ? ConvertTo<System.Guid>(values[CURRENCY_ID]) : (Guid?)null;
            }

            if(values.Contains(LEAD_VALUE)) {
                model.LeadValue = values[LEAD_VALUE] != null ? Convert.ToDouble(values[LEAD_VALUE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(COMMENTS)) {
                model.Comments = Convert.ToString(values[COMMENTS]);
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