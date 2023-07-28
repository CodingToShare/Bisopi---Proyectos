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
    public class APIProjectTypesController : Controller
    {
        private DataContext _context;

        public APIProjectTypesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var projectstypes = _context.ProjectsTypes.Select(i => new {
                i.ProjectTypeID,
                i.ProjectTypeName,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectTypeID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projectstypes, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            var projectstypes = _context.ProjectsTypes.Select(i => new {
                i.ProjectTypeID,
                i.ProjectTypeName,
                i.IsActive,
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ProjectTypeID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(projectstypes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ProjectType();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ProjectTypeID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.ProjectsTypes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ProjectTypeID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.ProjectsTypes.FirstOrDefaultAsync(item => item.ProjectTypeID == key);
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
            var model = await _context.ProjectsTypes.FirstOrDefaultAsync(item => item.ProjectTypeID == key);

            _context.ProjectsTypes.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(ProjectType model, IDictionary values) {
            string PROJECT_TYPE_ID = nameof(ProjectType.ProjectTypeID);
            string PROJECT_TYPE_NAME = nameof(ProjectType.ProjectTypeName);
            string IS_ACTIVE = nameof(ProjectType.IsActive);
            string CREATED_BY = nameof(ProjectType.CreatedBy);
            string CREATED = nameof(ProjectType.Created);
            string MODIFIED_BY = nameof(ProjectType.ModifiedBy);
            string MODIFIED = nameof(ProjectType.Modified);

            if(values.Contains(PROJECT_TYPE_ID)) {
                model.ProjectTypeID = ConvertTo<System.Guid>(values[PROJECT_TYPE_ID]);
            }

            if(values.Contains(PROJECT_TYPE_NAME)) {
                model.ProjectTypeName = Convert.ToString(values[PROJECT_TYPE_NAME]);
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