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
    public class APIBillsController : Controller
    {
        private DataContext _context;

        public APIBillsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, DataSourceLoadOptions loadOptions) {
            var bills = _context.Bills.Where(x => x.MilestoneID == id).Select(i => new {
                i.BillID,
                i.MilestoneID,
                i.StatusBill,
                i.InvoiceShipmentDate,
                i.IssuedDocument,
                i.RelatedDocument,
                i.NoteInvoice,
                i.InvoiceData,
                i.ConceptInvoice,
                i.ProposalDocument,
                i.IsActive,
                i.CreatedBy,
                i.Created,
                i.ModifiedBy,
                i.Modified
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "BillID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(bills, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Bill();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Bills.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.BillID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.Bills.FirstOrDefaultAsync(item => item.BillID == key);
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
            var model = await _context.Bills.FirstOrDefaultAsync(item => item.BillID == key);

            _context.Bills.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> MilestonesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Milestones
                         orderby i.Comment
                         select new {
                             Value = i.MilestoneID,
                             Text = i.Comment
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Bill model, IDictionary values) {
            string BILL_ID = nameof(Bill.BillID);
            string MILESTONE_ID = nameof(Bill.MilestoneID);
            string INVOICE_SHIPMENT_DATE = nameof(Bill.InvoiceShipmentDate);
            string ISSUED_DOCUMENT = nameof(Bill.IssuedDocument);
            string RELATED_DOCUMENT = nameof(Bill.RelatedDocument);
            string NOTE_INVOICE = nameof(Bill.NoteInvoice);
            string INVOICE_DATA = nameof(Bill.InvoiceData);
            string CONCEPT_INVOICE = nameof(Bill.ConceptInvoice);
            string PROPOSAL_DOCUMENT = nameof(Bill.ProposalDocument);
            string IS_ACTIVE = nameof(Bill.IsActive);
            string CREATED_BY = nameof(Bill.CreatedBy);
            string CREATED = nameof(Bill.Created);
            string MODIFIED_BY = nameof(Bill.ModifiedBy);
            string MODIFIED = nameof(Bill.Modified);

            if(values.Contains(BILL_ID)) {
                model.BillID = ConvertTo<System.Guid>(values[BILL_ID]);
            }

            if(values.Contains(MILESTONE_ID)) {
                model.MilestoneID = ConvertTo<System.Guid>(values[MILESTONE_ID]);
            }

            if(values.Contains(INVOICE_SHIPMENT_DATE)) {
                model.InvoiceShipmentDate = values[INVOICE_SHIPMENT_DATE] != null ? Convert.ToDateTime(values[INVOICE_SHIPMENT_DATE]) : (DateTime?)null;
            }

            if(values.Contains(ISSUED_DOCUMENT)) {
                model.IssuedDocument = Convert.ToString(values[ISSUED_DOCUMENT]);
            }

            if(values.Contains(RELATED_DOCUMENT)) {
                model.RelatedDocument = Convert.ToString(values[RELATED_DOCUMENT]);
            }

            if(values.Contains(NOTE_INVOICE)) {
                model.NoteInvoice = Convert.ToString(values[NOTE_INVOICE]);
            }

            if(values.Contains(INVOICE_DATA)) {
                model.InvoiceData = Convert.ToString(values[INVOICE_DATA]);
            }

            if(values.Contains(CONCEPT_INVOICE)) {
                model.ConceptInvoice = Convert.ToString(values[CONCEPT_INVOICE]);
            }

            if(values.Contains(PROPOSAL_DOCUMENT)) {
                model.ProposalDocument = Convert.ToString(values[PROPOSAL_DOCUMENT]);
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