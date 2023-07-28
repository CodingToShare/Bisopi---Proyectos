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
    public class APIProjectStatusController : Controller
    {
        private DataContext _context;

        public APIProjectStatusController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var projectsstatus = _context.ProjectsStatus.Select(i => new {
                i.ProjectStatusID,
                i.ProjectStatusName,
                i.Abbreviation,
                i.ProjectStatusNameAndAbbreviation,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projectsstatus, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            if (loadOptions.Filter != null && loadOptions.Filter.Count > 0 && loadOptions.Filter[0].ToString().Contains("ProjectStatusNameAndAbbreviation"))
            {

                var sample = loadOptions.Filter[0].ToString();
                var data = $"[{sample.Replace("ProjectStatusNameAndAbbreviation", "ProjectStatusName")}, \"or\" ,{sample.Replace("ProjectStatusNameAndAbbreviation", "Abbreviation")}]";
                var filter = JsonConvert.DeserializeObject(data) as IList;
                loadOptions.Filter = filter;
            }

            var projectsstatus = _context.ProjectsStatus.Where(x => x.IsActive).Select(i => new {
                i.ProjectStatusID,
                i.ProjectStatusName,
                i.Abbreviation,
                i.ProjectStatusNameAndAbbreviation
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projectsstatus, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectStatus();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ProjectStatusID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectsStatus.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectStatusID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectsStatus.FirstOrDefaultAsync(item => item.ProjectStatusID == key);
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
            var model = await _context.ProjectsStatus.FirstOrDefaultAsync(item => item.ProjectStatusID == key);

            _context.ProjectsStatus.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ProjectStatus model, IDictionary values) {
            string PROJECT_STATUS_ID = nameof(ProjectStatus.ProjectStatusID);
            string PROJECT_STATUS_NAME = nameof(ProjectStatus.ProjectStatusName);
            string ABBREVIATION = nameof(ProjectStatus.Abbreviation);
            string PROJECT_STATUS_NAME_AND_ABBREVIATION = nameof(ProjectStatus.ProjectStatusNameAndAbbreviation);
            string IS_ACTIVE = nameof(ProjectStatus.IsActive);
            string CREATED_BY = nameof(ProjectStatus.CreatedBy);
            string CREATED = nameof(ProjectStatus.Created);
            string MODIFIED_BY = nameof(ProjectStatus.ModifiedBy);
            string MODIFIED = nameof(ProjectStatus.Modified);

            if(values.Contains(PROJECT_STATUS_ID)) {
                model.ProjectStatusID = ConvertTo<System.Guid>(values[PROJECT_STATUS_ID]);
            }

            if(values.Contains(PROJECT_STATUS_NAME)) {
                model.ProjectStatusName = Convert.ToString(values[PROJECT_STATUS_NAME]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
            }

            if(values.Contains(PROJECT_STATUS_NAME_AND_ABBREVIATION)) {
                model.ProjectStatusNameAndAbbreviation = Convert.ToString(values[PROJECT_STATUS_NAME_AND_ABBREVIATION]);
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