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
            var carSize = new List<SelectListItem>();
            carSize.Add(new SelectListItem
            {
                Text = "Välj en",
                Value = ""
            
            });
            foreach (Carsize item in Enum.GetValues(typeof(Carsize)))
            {
                carSize.Add(new SelectListItem { Text = Enum.GetName(typeof(Carsize), item), Value = item.ToString()});
            }
            ViewBag.carSizeOne = carSize;
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
            booking.Car.IsBooked = true;
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
        public async Task<IActionResult> Payment([Bind("Id, Car, Mileage, BookingDateAndTime, ReturnDateAndTime")] Booking booking)
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
                _db.Update(booking);
                await _db.SaveChangesAsync();
                var bookingPrice = _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == booking.Id);
                return View(bookingPrice);
            }

            return View();
        }

        public async Task<IActionResult> ShowAllActiveBookings()
        {
            List<Booking> bookings = new List<Booking>();
            bookings = await _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Where(x => x.Car.IsBooked == true).ToListAsync();

            return View(bookings);
        }
    }
}