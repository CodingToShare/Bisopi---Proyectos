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
    public class APIClientsController : Controller
    {
        private DataContext _context;

        public APIClientsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var clients = _context.Clients.Select(i => new {
                i.ClientID,
                i.ClientName,
                i.Abbreviation,
                i.ClientNameAndAbbreviation,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ClientID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(clients, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectBox(DataSourceLoadOptions loadOptions)
        {
            if (loadOptions.Filter != null && loadOptions.Filter.Count > 0 && loadOptions.Filter[0].ToString().Contains("ClientNameAndAbbreviation"))
            {

                var sample = loadOptions.Filter[0].ToString();
                var data = $"[{sample.Replace("ClientNameAndAbbreviation", "ClientName")}, \"or\" ,{sample.Replace("ClientNameAndAbbreviation", "Abbreviation")}]";
                var filter = JsonConvert.DeserializeObject(data) as IList;
                loadOptions.Filter = filter;
            }

            var clients = _context.Clients.Where(x => x.IsActive).Select(i => new {
                i.ClientID,
                i.ClientName,
                i.Abbreviation,
                i.ClientNameAndAbbreviation
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ClientID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(clients, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Client();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.ClientID = Guid.NewGuid();
            model.Created = DateTime.UtcNow.AddHours(-5);
            model.CreatedBy = User.Identity.Name;
            model.Modified = DateTime.UtcNow.AddHours(-5);
            model.ModifiedBy = User.Identity.Name;

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));


            var result = _context.Clients.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ClientID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Clients.FirstOrDefaultAsync(item => item.ClientID == key);
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
            var model = await _context.Clients.FirstOrDefaultAsync(item => item.ClientID == key);

            _context.Clients.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Client model, IDictionary values) {
            string CLIENT_ID = nameof(Client.ClientID);
            string CLIENT_NAME = nameof(Client.ClientName);
            string ABBREVIATION = nameof(Client.Abbreviation);
            string CLIENT_NAME_AND_ABBREVIATION = nameof(Client.ClientNameAndAbbreviation);
            string IS_ACTIVE = nameof(Client.IsActive);
            string CREATED_BY = nameof(Client.CreatedBy);
            string CREATED = nameof(Client.Created);
            string MODIFIED_BY = nameof(Client.ModifiedBy);
            string MODIFIED = nameof(Client.Modified);

            if(values.Contains(CLIENT_ID)) {
                model.ClientID = ConvertTo<System.Guid>(values[CLIENT_ID]);
            }

            if(values.Contains(CLIENT_NAME)) {
                model.ClientName = Convert.ToString(values[CLIENT_NAME]);
            }

            if(values.Contains(ABBREVIATION)) {
                model.Abbreviation = Convert.ToString(values[ABBREVIATION]);
            }

            if(values.Contains(CLIENT_NAME_AND_ABBREVIATION)) {
                model.ClientNameAndAbbreviation = Convert.ToString(values[CLIENT_NAME_AND_ABBREVIATION]);
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