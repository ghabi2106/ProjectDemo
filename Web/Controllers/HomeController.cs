using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var customerList = CustomerList.GetCustomerList();
            return View(customerList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Details
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ID = id ?? 0;
            var customerEdit = await CustomerInfo.GetCustomerAsync(ID);
            return View(customerEdit);
        }
        #endregion

        // Create: Equipments
        #region Create
        public async Task<ActionResult> Create()
        {
            var customerCreate = await CustomerEdit.NewCustomerAsync();
            return View(customerCreate);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            var customerCreate = await CustomerEdit.NewCustomerAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    TryUpdateModel(customerCreate, collection);
                    if (customerCreate.IsSavable)
                    {
                        customerCreate = await customerCreate.SaveAsync();
                        return RedirectToAction("Edit", new { id = customerCreate.Id });
                    }
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(customerCreate);
        }
        #endregion

        // Edit: Equipments
        #region Edit      
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ID = id ?? 0;
            var customerEdit = CustomerEdit.GetCustomer(ID);
            
            if (customerEdit == null)
            {
                return HttpNotFound();
            }
            return View(customerEdit);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FormCollection collection, int? id)
        {

            var customer = await CustomerEdit.GetCustomerAsync((int)id);

            if (ModelState.IsValid)
            {
                TryUpdateModel(customer, collection);

                if (customer.IsSavable)
                {
                    customer.Save();
                }

                //if (!customerGetter.IsValid)
                //{
                //    return View("CustomerEdit", customerGetter);
                //}
            }

            return View(customer);
        }
        #endregion Edit

        // Delete: Equipments
        #region Delete
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            var ID = id ?? 0;
            var customer = CustomerEdit.GetCustomer(id ?? 0);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var customer = await CustomerEdit.GetCustomerAsync(id);
                if(customer.Id != id)
                {
                    return HttpNotFound();
                }
                await CustomerEdit.DeleteCustomerAsync(id);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}