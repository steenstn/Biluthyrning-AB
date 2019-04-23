using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;

        public BookingRepo(IHostingEnvironment env, ApplicationDbContext db)
        {
            _env = env;
            _db = db;
        }

        public List<Booking> GetBookings()
        {
            return _db.Bookings.Include(x => x.Customer).Include(x => x.Car).ToList();
        }

        public void Add(Booking booking)
        {
            _db.Add(booking);
            _db.SaveChanges();
        }

        public Booking EndBooking(int id)
        {
            return (_db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == id));
        }

        public Booking Update(Booking booking)
        {
            _db.Update(booking);            
            _db.SaveChanges();
            var bookingPrice = _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == booking.Id);
            return bookingPrice;
        }

        public List<Booking> ShowAllActiveBookings()
        {
            return _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Where(x => x.Car.IsBooked == true).ToList();
        }
    }
}
