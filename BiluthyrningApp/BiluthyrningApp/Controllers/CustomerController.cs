using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using BiluthyrningApp.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Controllers
{
    public class CustomerController : Controller
    {
        private IBookingRepo _bookingRepo;
        private ICarRepo _carRepo;
        private ICustomerRepo _customerRepo;
        private ILogsRepo _logsRepo;


        public CustomerController(IBookingRepo bookingRepo, ICarRepo carRepo, ICustomerRepo customerRepo, ILogsRepo logsRepo)
        {
            _bookingRepo = bookingRepo;
            _carRepo = carRepo;
            _customerRepo = customerRepo;
            _logsRepo = logsRepo;
        }

        public IActionResult ShowListOfCustomers()
        {
            return View(_customerRepo.ShowListOfCustomers());
          
        }
        
        public IActionResult ShowBookings(int? Id)
        {            
            return View(_customerRepo.ShowBookings(Id));
        }

        public IActionResult CreateNewCustomer()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SSN, FirstName, LastName")] Customer customer)
        {
            foreach (var item in _customerRepo.AllCustomers())
            {
                if (item.SSN == customer.SSN)
                {
                    ViewBag.error = $"Kunden {customer.SSN} finns redan i databasen";
                    return View("~/Views/Customer/CreateNewCustomer.cshtml");

                }
            }
            if (ModelState.IsValid)
            {
                _customerRepo.Add(customer);
                ViewBag.ok = $"Kunden {customer.FirstName} är tillagd";
                return View("~/Views/Home/Index.cshtml");
            }

            return View("~/Views/Customer/CreateNewCustomer.cshtml");
                     
        }

        public IActionResult ShowCustomerLogs(int Id)
        {
            List<Logs> customerLogs = _logsRepo.ShowCustomerLogs(Id);
            return View(customerLogs);
        }
    }
}
