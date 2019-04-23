using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BiluthyrningApp.Controllers
{
    public class CarController : Controller
    {
        private IBookingRepo _bookingRepo;
        private ICarRepo _carRepo;
        private ICustomerRepo _customerRepo;

        public CarController(IBookingRepo bookingRepo, ICarRepo carRepo, ICustomerRepo customerRepo)
        {
            _bookingRepo = bookingRepo;
            _carRepo = carRepo;
            _customerRepo = customerRepo;
        }
        public IActionResult ShowAllCars()
        {
            return View(_carRepo.AllCars());
        }
       
    }
}