using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Bookings.Include(x => x.Customer).Include(x => x.Car).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Customer, Car, Mileage, BookingDateAndTime")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _db.Add(booking);
                await _db.SaveChangesAsync();
                return View("~/Views/Home/Index.cshtml");
            }
            
            return View();
        }
        public IActionResult EndBooking(int id)
        {
            var booking = _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == id);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment([Bind("Id, Customer, Car, Mileage, BookingDateAndTime, ReturnDateAndTime")] Booking booking)
        {
            double baseDayRental = 100;
            double kmPrice = 1;
            double nrOfHours = (booking.ReturnDateAndTime - booking.BookingDateAndTime).TotalHours;            
            double nrOfDays = nrOfHours / 24;

            if (booking.Car.CarSize == "Small")
            {

                booking.Price = (int)Math.Round(baseDayRental * nrOfDays, 0);
            }
            else if (booking.Car.CarSize == "Van")
            {
                booking.Price = (int)Math.Round(baseDayRental * nrOfDays * 1.2 + kmPrice * booking.Mileage, 0);
            }
            else if (booking.Car.CarSize == "Minibus")
            {
                booking.Price = (int)Math.Round(baseDayRental * nrOfDays * 1.7 + kmPrice * booking.Mileage * 1.5, 0);
            }
            booking.Car.DistanceInKm += booking.Mileage;

            if (ModelState.IsValid)
            {
                _db.Update(booking);
                await _db.SaveChangesAsync();
                var bookingPrice = _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == booking.Id);
                return View(bookingPrice);
            }

            return View();
        }
    }
}