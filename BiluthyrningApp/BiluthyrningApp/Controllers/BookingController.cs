using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using BiluthyrningApp.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningApp.Controllers
{
    public class BookingController : Controller
    {
        private IBookingRepo _bookingRepo;
        private ICarRepo _carRepo;
        private ICustomerRepo _customerRepo;

        public BookingController(IBookingRepo bookingRepo, ICarRepo carRepo, ICustomerRepo customerRepo)
        {
            _bookingRepo = bookingRepo;
            _carRepo = carRepo;
            _customerRepo = customerRepo;
        }

        public IActionResult Create()
        {
            var cars = new List<SelectListItem>();
            cars.Add(new SelectListItem
            {
                Text = "Välj en",
                Value = ""
            });
            List<Car> carsInDatabase = _carRepo.AllCarsNotBooked();
            foreach (var item in carsInDatabase)
            {
                cars.Add(new SelectListItem { Text = item.LicensePlate, Value = item.LicensePlate});
            }
            ViewBag.carSizeOne = cars;
            return View();
        }

        public IActionResult Index()
        {
            var bookings = _bookingRepo.GetBookings();
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            List<Car> allCars = _carRepo.AllCars();
            foreach (var item in allCars)
            {
                if (item.LicensePlate == booking.Car.LicensePlate)
                {
                    booking.Car = item;
                }
            }
            if (booking.Car.LicensePlate == null)
            {
                ViewBag.error = "Vänligen välj en bil, finns ingen måste du lägga till den först";
                return View();
            }
            
            booking.Car.IsBooked = true;
            if (ModelState.IsValid)
            {                
                ViewBag.ok = "Bokningen skapad";
                _bookingRepo.Add(booking);
                return View("~/Views/Home/Index.cshtml");
            }
            ViewBag.error = "Något gick fel, vänligen försök igen";
            return View();
        }
        public IActionResult EndBooking(int id)
        {
            var booking = _bookingRepo.EndBooking(id);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment([Bind("Id, Customer, Car, Mileage, BookingDateAndTime, ReturnDateAndTime")] Booking booking)
        {
            booking.Car.IsBooked = false;            
            decimal baseDayRental = 100; // För alla bilar
            decimal kmPrice = 1; // För alla bilar
            decimal nrOfHours = (decimal)(booking.ReturnDateAndTime - booking.BookingDateAndTime).TotalHours; // Tar fram antal timmar bilen varit bokad
            decimal nrOfDays = nrOfHours / 24; // Gör om antal timmar bilen varit bokad till antal dagar (med decimaler)
            decimal basePrice = baseDayRental * nrOfDays; // Grundpris för alla bilar
            decimal vanPrice = 1.2M; // Extrapris för typen van          
            decimal miniBusPrice = 1.7M; // Extrapris för typen minibuss
            decimal miniBusPriceExtraPerKm = 1.5M; // Extrapris per km för typen minibuss
            
            if (booking.Car.CarSize == Carsize.Small)
            {
                booking.Price = (decimal)Math.Round(basePrice, 0);
            }
            else if (booking.Car.CarSize == Carsize.Van)
            {
                booking.Price = (decimal)Math.Round((basePrice * vanPrice) + kmPrice * booking.Mileage, 0);
            }
            else if (booking.Car.CarSize == Carsize.Minibus)
            {
                booking.Price = (decimal)Math.Round(basePrice * miniBusPrice + kmPrice * booking.Mileage * miniBusPriceExtraPerKm, 0);
            }
            booking.Car.DistanceInKm += booking.Mileage;

            if (ModelState.IsValid)
            {
                return View(_bookingRepo.Update(booking));
            }
                
            return View();
        }

        public IActionResult ShowAllActiveBookings()
        {
            return View(_bookingRepo.ShowAllActiveBookings());
        }
    }
}