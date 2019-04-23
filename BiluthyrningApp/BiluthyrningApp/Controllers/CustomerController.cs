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

        public CustomerController(IBookingRepo bookingRepo, ICarRepo carRepo, ICustomerRepo customerRepo)
        {
            _bookingRepo = bookingRepo;
            _carRepo = carRepo;
            _customerRepo = customerRepo;
        }

        public IActionResult ShowListOfCustomers()
        {
            return View(_customerRepo.ShowListOfCustomers());
          
        }
        
        public IActionResult ShowBookings(int? Id)
        {            
            return View(_customerRepo.ShowBookings(Id));
        }
    }
}
