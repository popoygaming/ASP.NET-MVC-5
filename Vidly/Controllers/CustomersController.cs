using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    //[Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize]
        [Route("Customers")]
        public ActionResult Customers()
        {
            //var viewModel = new CustomerViewModel()
            //{
            //    Customers = _context.Customers.Include(c => c.MembershipType)
            //};
            //return View(viewModel);
            return View();
        }

        [Route("Customers/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var viewModel = new CustomerViewModel();
            viewModel.MembershipTypes = _context.MembershipTypes.ToList();
            viewModel.FormActionName = "Edit Customer";
            var customers = _context.Customers;
            viewModel.Customer = customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (viewModel.Customer == null)
            {
                //return HttpNotFound();
                viewModel.Customer = new Customer() { Name = "Customer not found.", MembershipType = new MembershipType() };
            }
            return View("CustomerForm", viewModel);
        }

        [Route("Customers/New")]
        public ActionResult New()
        {
            var viewModel = new CustomerViewModel();
            var customer = _context.Movies;

            viewModel.Customer = new Customer();
            viewModel.FormActionName = "New Customer";
            viewModel.MembershipTypes = _context.MembershipTypes.ToList();
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerViewModel vModel)
        {
            var newCustomer = vModel.Customer;
            
            // data validation
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerViewModel()
                {
                    Customer = newCustomer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                    FormActionName = "New Customer"
                };
                return View("CustomerForm", viewModel);
            }
            else
            {
                if (newCustomer.Id == 0)
                {
                    // add new customer
                    _context.Customers.Add(newCustomer);
                }
                else
                {
                    // edit existing customer
                    var customerInDb = _context.Customers.Single(m => m.Id == newCustomer.Id);
                    customerInDb.Name = newCustomer.Name;
                    customerInDb.Birthday = newCustomer.Birthday;
                    customerInDb.IsSubscribeToNewsLetter = newCustomer.IsSubscribeToNewsLetter;
                    customerInDb.MembershipTypeId = newCustomer.MembershipTypeId;
                }
                _context.SaveChanges();
                return RedirectToAction("Customers", "Customers");
            }
        }
    }
}