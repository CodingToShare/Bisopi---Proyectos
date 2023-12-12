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
    public class APIInvoiceReportsController : Controller
    {
        private DataContext _context;

        public APIInvoiceReportsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {

            var invoicereports = _context.InvoiceReports.Where(x => x.IsActive).AsNoTracking().ToList();

            foreach (var item in invoicereports)
            {
                try
                {


                    var modelProject = _context.Projects.AsNoTracking().Where(x => x.ProjectID == item.ProjectID).FirstOrDefault();
                    var modelRetention = _context.RetentionPercentages.AsNoTracking().Where(x => x.CountryID == modelProject.CountryID && x.IsActive && (x.EffectiveStartDate < DateTime.UtcNow.AddHours(-5) || x.ValidEndDate > DateTime.UtcNow.AddHours(-5))).FirstOrDefault();
                    var modelTRM = _context.RepresentativeMarketRates.AsNoTracking().Where(x => x.CurrencyID == item.CurrencyID && x.IsActive).FirstOrDefault();

                    if (item.Value != null)
                    {
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
                catch(Exception ex) 
                { }

            }

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "InvoiceReportID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(DataSourceLoader.Load(invoicereports, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new InvoiceReport();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.InvoiceReports.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.InvoiceReportID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.InvoiceReports.FirstOrDefaultAsync(item => item.InvoiceReportID == key);
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
            var model = await _context.InvoiceReports.FirstOrDefaultAsync(item => item.InvoiceReportID == key);

            _context.InvoiceReports.Remove(model);
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
        public async Task<IActionResult> ProjectsStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ProjectsStatus
                         orderby i.ProjectStatusName
                         select new {
                             Value = i.ProjectStatusID,
                             Text = i.ProjectStatusName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
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

        private void PopulateModel(InvoiceReport model, IDictionary values) {
            string INVOICE_REPORT_ID = nameof(InvoiceReport.InvoiceReportID);
            string BILL_ID = nameof(InvoiceReport.BillID);
            string MILESTONE_ID = nameof(InvoiceReport.MilestoneID);
            string PROJECT_ID = nameof(InvoiceReport.ProjectID);
            string PROJECT_NAME = nameof(InvoiceReport.ProjectName);
            string COUNTRY_ID = nameof(InvoiceReport.CountryID);
            string CLIENT_ID = nameof(InvoiceReport.ClientID);
            string PROJECT_STATUS_ID = nameof(InvoiceReport.ProjectStatusID);
            string IS_IT_CHANGE_CONTROL = nameof(InvoiceReport.IsItChangeControl);
            string MILESTONE_NUMBER = nameof(InvoiceReport.MilestoneNumber);
            string MILESTONE_DATE = nameof(InvoiceReport.MilestoneDate);
            string CURRENCY_ID = nameof(InvoiceReport.CurrencyID);
            string VALUE = nameof(InvoiceReport.Value);
            string TRM = nameof(InvoiceReport.TRM);
            string SUB_TOTAL_BILL_COP = nameof(InvoiceReport.SubTotalBillCOP);
            string RETENTION_VALUE_COP = nameof(InvoiceReport.RetentionValueCOP);
            string VALUE_ADDED_TAX = nameof(InvoiceReport.ValueAddedTax);
            string VALUE_ADDED_TAX_WUTHHOLDING = nameof(InvoiceReport.ValueAddedTaxWuthholding);
            string TOTAL_BILL_COP = nameof(InvoiceReport.TotalBillCOP);
            string BILLING_APPROVED_DATE = nameof(InvoiceReport.BillingApprovedDate);
            string BILLED_DATE = nameof(InvoiceReport.BilledDate);
            string DATE_PAID = nameof(InvoiceReport.DatePaid);
            string IS_ACTIVE = nameof(InvoiceReport.IsActive);
            string CREATED_BY = nameof(InvoiceReport.CreatedBy);
            string CREATED = nameof(InvoiceReport.Created);
            string MODIFIED_BY = nameof(InvoiceReport.ModifiedBy);
            string MODIFIED = nameof(InvoiceReport.Modified);

            if(values.Contains(INVOICE_REPORT_ID)) {
                model.InvoiceReportID = ConvertTo<System.Guid>(values[INVOICE_REPORT_ID]);
            }

            if(values.Contains(BILL_ID)) {
                model.BillID = ConvertTo<System.Guid>(values[BILL_ID]);
            }

            if(values.Contains(MILESTONE_ID)) {
                model.MilestoneID = ConvertTo<System.Guid>(values[MILESTONE_ID]);
            }

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = values[PROJECT_ID] != null ? ConvertTo<System.Guid>(values[PROJECT_ID]) : (Guid?)null;
            }

            if(values.Contains(PROJECT_NAME)) {
                model.ProjectName = Convert.ToString(values[PROJECT_NAME]);
            }

            if(values.Contains(COUNTRY_ID)) {
                model.CountryID = values[COUNTRY_ID] != null ? ConvertTo<System.Guid>(values[COUNTRY_ID]) : (Guid?)null;
            }

            if(values.Contains(CLIENT_ID)) {
                model.ClientID = values[CLIENT_ID] != null ? ConvertTo<System.Guid>(values[CLIENT_ID]) : (Guid?)null;
            }

            if(values.Contains(PROJECT_STATUS_ID)) {
                model.ProjectStatusID = values[PROJECT_STATUS_ID] != null ? ConvertTo<System.Guid>(values[PROJECT_STATUS_ID]) : (Guid?)null;
            }

            if(values.Contains(IS_IT_CHANGE_CONTROL)) {
                model.IsItChangeControl = Convert.ToBoolean(values[IS_IT_CHANGE_CONTROL]);
            }

            if(values.Contains(MILESTONE_NUMBER)) {
                model.MilestoneNumber = values[MILESTONE_NUMBER] != null ? Convert.ToInt32(values[MILESTONE_NUMBER]) : (int?)null;
            }

            if(values.Contains(MILESTONE_DATE)) {
                model.MilestoneDate = Convert.ToDateTime(values[MILESTONE_DATE]);
            }

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = values[CURRENCY_ID] != null ? ConvertTo<System.Guid>(values[CURRENCY_ID]) : (Guid?)null;
            }

            if(values.Contains(VALUE)) {
                model.Value = values[VALUE] != null ? Convert.ToDouble(values[VALUE], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(TRM)) {
                model.TRM = values[TRM] != null ? Convert.ToDouble(values[TRM], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(SUB_TOTAL_BILL_COP)) {
                model.SubTotalBillCOP = values[SUB_TOTAL_BILL_COP] != null ? Convert.ToDouble(values[SUB_TOTAL_BILL_COP], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(RETENTION_VALUE_COP)) {
                model.RetentionValueCOP = values[RETENTION_VALUE_COP] != null ? Convert.ToDouble(values[RETENTION_VALUE_COP], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(VALUE_ADDED_TAX)) {
                model.ValueAddedTax = values[VALUE_ADDED_TAX] != null ? Convert.ToDouble(values[VALUE_ADDED_TAX], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(VALUE_ADDED_TAX_WUTHHOLDING)) {
                model.ValueAddedTaxWuthholding = values[VALUE_ADDED_TAX_WUTHHOLDING] != null ? Convert.ToDouble(values[VALUE_ADDED_TAX_WUTHHOLDING], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(TOTAL_BILL_COP)) {
                model.TotalBillCOP = values[TOTAL_BILL_COP] != null ? Convert.ToDouble(values[TOTAL_BILL_COP], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(BILLING_APPROVED_DATE)) {
                model.BillingApprovedDate = values[BILLING_APPROVED_DATE] != null ? Convert.ToDateTime(values[BILLING_APPROVED_DATE]) : (DateTime?)null;
            }

            if(values.Contains(BILLED_DATE)) {
                model.BilledDate = values[BILLED_DATE] != null ? Convert.ToDateTime(values[BILLED_DATE]) : (DateTime?)null;
            }

            if(values.Contains(DATE_PAID)) {
                model.DatePaid = values[DATE_PAID] != null ? Convert.ToDateTime(values[DATE_PAID]) : (DateTime?)null;
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