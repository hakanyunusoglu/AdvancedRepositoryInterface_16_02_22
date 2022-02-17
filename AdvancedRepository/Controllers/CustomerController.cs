using AdvancedRepository.Business;
using AdvancedRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AdvancedRepository.Business.MyRepositories;

namespace AdvancedRepository.Controllers
{
    public class CustomerController : Controller
    {
        CustomersRepository repo = new CustomersRepository();
        CustomerModel cm = new CustomerModel();
        
        public ActionResult Index(string name)
        { 
            if (name == null)
            {
                name = "";
            }
            cm.cList = repo.GenelListe().Where(x => x.CompanyName.Contains(name)).ToList();
            return View(cm);
        }
        public ActionResult Detail(string id)
        {
            cm.Customers = repo.Bul(id);
            return View(cm);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                Customers c = new Customers();
                c.CustomerID = model.Customers.CustomerID.ToUpper();
                c.CompanyName = model.Customers.CompanyName;
                c.ContactName = model.Customers.ContactName;
                c.Phone = model.Customers.Phone;
                c.City = model.Customers.City;
                c.Address = model.Customers.Address;
                c.Country = model.Customers.Country;
                c.PostalCode = model.Customers.PostalCode;
                repo.Ekle(c);
                repo.Guncel();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(string id)
        {
            Customers c = repo.Bul(id);
            repo.Sil(c);
            repo.Guncel();
            return RedirectToAction("Index");
        }
        public ActionResult Update(string id)
        {
            cm.Customers = repo.Bul(id);          
            return View(cm);
        }
        [HttpPost]
        public ActionResult Update(string id, CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                Customers selectedCustomer = repo.Bul(id);
                selectedCustomer.CompanyName = model.Customers.CompanyName;
                selectedCustomer.ContactName = model.Customers.ContactName;
                selectedCustomer.Phone = model.Customers.Phone;
                selectedCustomer.City = model.Customers.City;
                selectedCustomer.Address = model.Customers.Address;
                selectedCustomer.Country = model.Customers.Country;
                selectedCustomer.PostalCode = model.Customers.PostalCode;

                repo.Guncel();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
        public ActionResult Choose()
        {
            CustomerModel c = new CustomerModel();
            c.cList = repo.GenelListe().OrderBy(x => x.CustomerID).Skip(8).Take(19).ToList();
            return View(c);
        }
    }
}