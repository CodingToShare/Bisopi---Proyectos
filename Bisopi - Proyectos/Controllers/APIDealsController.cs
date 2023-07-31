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
    public class APIDealsController : Controller
    {
        private DataContext _context;

        public APIDealsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var deals = _context.Deals.Select(i => new {
                i.DealID,
                i.DealName,
                i.ClientID,
                i.ResponsibleClient,
                i.ProposalStatusID,
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
            // loadOptions.PrimaryKey = new[] { "DealID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(deals, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Deal();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Deals.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.DealID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Deals.FirstOrDefaultAsync(item => item.DealID == key);
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
            var model = await _context.Deals.FirstOrDefaultAsync(item => item.DealID == key);

            _context.Deals.Remove(model);
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
        public async Task<IActionResult> ProposalsStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ProposalsStatus
                         orderby i.ProposalStatusName
                         select new {
                             Value = i.ProposalStatusID,
                             Text = i.ProposalStatusName
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

        private void PopulateModel(Deal model, IDictionary values) {
            string DEAL_ID = nameof(Deal.DealID);
            string DEAL_NAME = nameof(Deal.DealName);
            string CLIENT_ID = nameof(Deal.ClientID);
            string RESPONSIBLE_CLIENT = nameof(Deal.ResponsibleClient);
            string PROPOSAL_STATUS_ID = nameof(Deal.ProposalStatusID);
            string CURRENCY_ID = nameof(Deal.CurrencyID);
            string LEAD_VALUE = nameof(Deal.LeadValue);
            string COMMENTS = nameof(Deal.Comments);
            string IS_ACTIVE = nameof(Deal.IsActive);
            string CREATED_BY = nameof(Deal.CreatedBy);
            string CREATED = nameof(Deal.Created);
            string MODIFIED_BY = nameof(Deal.ModifiedBy);
            string MODIFIED = nameof(Deal.Modified);

            if(values.Contains(DEAL_ID)) {
                model.DealID = ConvertTo<System.Guid>(values[DEAL_ID]);
            }

            if(values.Contains(DEAL_NAME)) {
                model.DealName = Convert.ToString(values[DEAL_NAME]);
            }

            if(values.Contains(CLIENT_ID)) {
                model.ClientID = ConvertTo<System.Guid>(values[CLIENT_ID]);
            }

            if(values.Contains(RESPONSIBLE_CLIENT)) {
                model.ResponsibleClient = Convert.ToString(values[RESPONSIBLE_CLIENT]);
            }

            if(values.Contains(PROPOSAL_STATUS_ID)) {
                model.ProposalStatusID = ConvertTo<System.Guid>(values[PROPOSAL_STATUS_ID]);
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