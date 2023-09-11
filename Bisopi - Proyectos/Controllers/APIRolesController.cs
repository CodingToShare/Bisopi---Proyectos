using Bisopi___Proyectos.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;

namespace Bisopi___Proyectos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public APIRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ClientID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(DataSourceLoader.Load(roles, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {

            var model = new Microsoft.AspNetCore.Identity.IdentityRole();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);


            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));


            var result = await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return Json(new { result.Succeeded });

        }

        private void PopulateModel(Microsoft.AspNetCore.Identity.IdentityRole model, IDictionary values)
        {
            string ID = nameof(IdentityRole.Id);
            string NAME = nameof(IdentityRole.Name);

            if (values.Contains(ID))
            {
                model.Id = ConvertTo<System.Guid>(values[ID]).ToString();
            }

            if (values.Contains(NAME))
            {
                model.Name = Convert.ToString(values[NAME]);
            }

        }

        private T ConvertTo<T>(object value)
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            }
            else
            {
                // If necessary, implement a type conversion here
                throw new NotImplementedException();
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}
