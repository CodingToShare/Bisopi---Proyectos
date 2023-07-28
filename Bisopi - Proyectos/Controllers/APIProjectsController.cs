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
    public class APIProjectsController : Controller
    {
        private DataContext _context;

        public APIProjectsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var projects = _context.Projects.Select(i => new {
                i.ProjectID,
                i.ProjectName,
                i.CountryID,
                i.ClientID,
                i.CustomerManager,
                i.LeaderID,
                i.ProjectManagerID,
                i.ProjectStatusID,
                i.ProjectTypeID,
                i.SupportStatusID,
                i.EstimateRequestDate,
                i.AnswerDate,
                i.RequestPriority,
                i.ScalerCode,
                i.ClarityCode,
                i.EstimatedHours,
                i.StartDate,
                i.EstimatedDeliveryDate,
                i.ActualCompletionDate,
                i.CurrencyID,
                i.ProjectValue,
                i.ProjectCost,
                i.TRMProject,
                i.GrossMargin,
                i.Billable,
                i.Justification,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projects, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Project();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Projects.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Projects.FirstOrDefaultAsync(item => item.ProjectID == key);
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
            var model = await _context.Projects.FirstOrDefaultAsync(item => item.ProjectID == key);

            _context.Projects.Remove(model);
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
        public async Task<IActionResult> ProjectsTypesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ProjectsTypes
                         orderby i.ProjectTypeName
                         select new {
                             Value = i.ProjectTypeID,
                             Text = i.ProjectTypeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SupportsStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SupportsStatus
                         orderby i.SupportStatusName
                         select new {
                             Value = i.SupportStatusID,
                             Text = i.SupportStatusName
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

        private void PopulateModel(Project model, IDictionary values) {
            string PROJECT_ID = nameof(Project.ProjectID);
            string PROJECT_NAME = nameof(Project.ProjectName);
            string COUNTRY_ID = nameof(Project.CountryID);
            string CLIENT_ID = nameof(Project.ClientID);
            string CUSTOMER_MANAGER = nameof(Project.CustomerManager);
            string LEADER_ID = nameof(Project.LeaderID);
            string PROJECT_MANAGER_ID = nameof(Project.ProjectManagerID);
            string PROJECT_STATUS_ID = nameof(Project.ProjectStatusID);
            string PROJECT_TYPE_ID = nameof(Project.ProjectTypeID);
            string SUPPORT_STATUS_ID = nameof(Project.SupportStatusID);
            string ESTIMATE_REQUEST_DATE = nameof(Project.EstimateRequestDate);
            string ANSWER_DATE = nameof(Project.AnswerDate);
            string REQUEST_PRIORITY = nameof(Project.RequestPriority);
            string SCALER_CODE = nameof(Project.ScalerCode);
            string CLARITY_CODE = nameof(Project.ClarityCode);
            string ESTIMATED_HOURS = nameof(Project.EstimatedHours);
            string START_DATE = nameof(Project.StartDate);
            string ESTIMATED_DELIVERY_DATE = nameof(Project.EstimatedDeliveryDate);
            string ACTUAL_COMPLETION_DATE = nameof(Project.ActualCompletionDate);
            string CURRENCY_ID = nameof(Project.CurrencyID);
            string PROJECT_VALUE = nameof(Project.ProjectValue);
            string PROJECT_COST = nameof(Project.ProjectCost);
            string TRMPROJECT = nameof(Project.TRMProject);
            string GROSS_MARGIN = nameof(Project.GrossMargin);
            string BILLABLE = nameof(Project.Billable);
            string JUSTIFICATION = nameof(Project.Justification);
            string CREATED_BY = nameof(Project.CreatedBy);
            string CREATED = nameof(Project.Created);
            string MODIFIED_BY = nameof(Project.ModifiedBy);
            string MODIFIED = nameof(Project.Modified);

            if(values.Contains(PROJECT_ID)) {
                model.ProjectID = ConvertTo<System.Guid>(values[PROJECT_ID]);
            }

            if(values.Contains(PROJECT_NAME)) {
                model.ProjectName = Convert.ToString(values[PROJECT_NAME]);
            }

            if(values.Contains(COUNTRY_ID)) {
                model.CountryID = ConvertTo<System.Guid>(values[COUNTRY_ID]);
            }

            if(values.Contains(CLIENT_ID)) {
                model.ClientID = ConvertTo<System.Guid>(values[CLIENT_ID]);
            }

            if(values.Contains(CUSTOMER_MANAGER)) {
                model.CustomerManager = Convert.ToString(values[CUSTOMER_MANAGER]);
            }

            if(values.Contains(LEADER_ID)) {
                model.LeaderID = ConvertTo<System.Guid>(values[LEADER_ID]);
            }

            if(values.Contains(PROJECT_MANAGER_ID)) {
                model.ProjectManagerID = ConvertTo<System.Guid>(values[PROJECT_MANAGER_ID]);
            }

            if(values.Contains(PROJECT_STATUS_ID)) {
                model.ProjectStatusID = ConvertTo<System.Guid>(values[PROJECT_STATUS_ID]);
            }

            if(values.Contains(PROJECT_TYPE_ID)) {
                model.ProjectTypeID = ConvertTo<System.Guid>(values[PROJECT_TYPE_ID]);
            }

            if(values.Contains(SUPPORT_STATUS_ID)) {
                model.SupportStatusID = ConvertTo<System.Guid>(values[SUPPORT_STATUS_ID]);
            }

            if(values.Contains(ESTIMATE_REQUEST_DATE)) {
                model.EstimateRequestDate = values[ESTIMATE_REQUEST_DATE] != null ? Convert.ToDateTime(values[ESTIMATE_REQUEST_DATE]) : (DateTime?)null;
            }

            if(values.Contains(ANSWER_DATE)) {
                model.AnswerDate = values[ANSWER_DATE] != null ? Convert.ToDateTime(values[ANSWER_DATE]) : (DateTime?)null;
            }

            if(values.Contains(REQUEST_PRIORITY)) {
                model.RequestPriority = Convert.ToString(values[REQUEST_PRIORITY]);
            }

            if(values.Contains(SCALER_CODE)) {
                model.ScalerCode = Convert.ToString(values[SCALER_CODE]);
            }

            if(values.Contains(CLARITY_CODE)) {
                model.ClarityCode = Convert.ToString(values[CLARITY_CODE]);
            }

            if(values.Contains(ESTIMATED_HOURS)) {
                model.EstimatedHours = Convert.ToInt32(values[ESTIMATED_HOURS]);
            }

            if(values.Contains(START_DATE)) {
                model.StartDate = Convert.ToDateTime(values[START_DATE]);
            }

            if(values.Contains(ESTIMATED_DELIVERY_DATE)) {
                model.EstimatedDeliveryDate = Convert.ToDateTime(values[ESTIMATED_DELIVERY_DATE]);
            }

            if(values.Contains(ACTUAL_COMPLETION_DATE)) {
                model.ActualCompletionDate = values[ACTUAL_COMPLETION_DATE] != null ? Convert.ToDateTime(values[ACTUAL_COMPLETION_DATE]) : (DateTime?)null;
            }

            if(values.Contains(CURRENCY_ID)) {
                model.CurrencyID = ConvertTo<System.Guid>(values[CURRENCY_ID]);
            }

            if(values.Contains(PROJECT_VALUE)) {
                model.ProjectValue = Convert.ToDouble(values[PROJECT_VALUE], CultureInfo.InvariantCulture);
            }

            if(values.Contains(PROJECT_COST)) {
                model.ProjectCost = Convert.ToDouble(values[PROJECT_COST], CultureInfo.InvariantCulture);
            }

            if(values.Contains(TRMPROJECT)) {
                model.TRMProject = Convert.ToDouble(values[TRMPROJECT], CultureInfo.InvariantCulture);
            }

            if(values.Contains(GROSS_MARGIN)) {
                model.GrossMargin = Convert.ToDouble(values[GROSS_MARGIN], CultureInfo.InvariantCulture);
            }

            if(values.Contains(BILLABLE)) {
                model.Billable = Convert.ToBoolean(values[BILLABLE]);
            }

            if(values.Contains(JUSTIFICATION)) {
                model.Justification = Convert.ToString(values[JUSTIFICATION]);
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