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
    public class APISenioritiesController : Controller
    {
        private DataContext _context;

        public APISenioritiesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var seniorities = _context.Seniorities.Select(i => new {
                i.SeniorityID,
                i.SeniorityName,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "SeniorityID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(seniorities, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            var seniorities = _context.Seniorities.Where(x => x.IsActive).Select(i => new {
                i.SeniorityID,
                i.SeniorityName,
                i.IsActive

            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "QuoteStatusID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(seniorities, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Seniority();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.SeniorityID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Seniorities.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.SeniorityID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Seniorities.FirstOrDefaultAsync(item => item.SeniorityID == key);
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
            var model = await _context.Seniorities.FirstOrDefaultAsync(item => item.SeniorityID == key);

            _context.Seniorities.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Seniority model, IDictionary values) {
            string SENIORITY_ID = nameof(Seniority.SeniorityID);
            string SENIORITY_NAME = nameof(Seniority.SeniorityName);
            string IS_ACTIVE = nameof(Seniority.IsActive);
            string CREATED_BY = nameof(Seniority.CreatedBy);
            string CREATED = nameof(Seniority.Created);
            string MODIFIED_BY = nameof(Seniority.ModifiedBy);
            string MODIFIED = nameof(Seniority.Modified);

            if(values.Contains(SENIORITY_ID)) {
                model.SeniorityID = ConvertTo<System.Guid>(values[SENIORITY_ID]);
            }

            if(values.Contains(SENIORITY_NAME)) {
                model.SeniorityName = Convert.ToString(values[SENIORITY_NAME]);
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