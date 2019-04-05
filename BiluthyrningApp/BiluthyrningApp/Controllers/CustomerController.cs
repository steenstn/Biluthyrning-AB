using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
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
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> ShowListOfCustomers()
        {
            return View(await _db.Customers.ToListAsync());
            //return View(await _db.Bookings.Include(x => x.Customer).Include(x => x.Car).ToListAsync());
        }

        public async Task<IActionResult> ShowBookings(int? Id)
        {
            List<Booking> bookings = new List<Booking>();
            bookings = await _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Where(x => x.Id == Id).ToListAsync();
            
            return View(bookings);
        }
    }
}
