using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningApp.Models;
using BiluthyrningApp.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult CreateNewCar()
        {
            var carSize = new List<SelectListItem>();
            carSize.Add(new SelectListItem
            {
                Text = "Välj en",
                Value = ""

            });
            foreach (Carsize item in Enum.GetValues(typeof(Carsize)))
            {
                carSize.Add(new SelectListItem { Text = Enum.GetName(typeof(Carsize), item), Value = item.ToString() });
            }
            ViewBag.carSizeOne = carSize;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CarSize, LicensePlate, DistanceInKm")] Car car)
        {
            car.IsBooked = false;
            bool isCarNew = _carRepo.CheckIfCarIsOnDatabase(car);
            if (isCarNew)
            {
                ViewBag.error = "Bilen med detta reg-nummret finns redan";
                return View("~/Views/Home/Index.cshtml");
            }
            if (ModelState.IsValid)
            {
                _carRepo.Add(car);
                ViewBag.ok = $"Bilen med reg-nummer {car.LicensePlate} är tillagd";
                return View("~/Views/Home/Index.cshtml");
            }

            return View("~/Views/Car/CreateNewCar.cshtml");
        }

        public IActionResult ServiceCar(int id)
        {
            _carRepo.ServiceCar(id);
            ViewBag.ok = $"Bilen är nu servad";
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult CleanCar(int id)
        {
            _carRepo.CleanCar(id);
            ViewBag.ok = $"Bilen är nu tvättad";
            return View("~/Views/Home/Index.cshtml");
        }

         public IActionResult RemoveCar(int id)
        {
            _carRepo.RemoveCar(id);
            ViewBag.ok = $"Bilen är nu bortagen";
            return View("~/Views/Home/Index.cshtml");
        }
    }
}