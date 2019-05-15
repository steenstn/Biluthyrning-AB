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
            List<Car> allCars = new List<Car>();
            allCars = _db.Cars.ToList();
            foreach (var item in allCars)
            {
                if (item.LicensePlate.ToString() == booking.Car.LicensePlate.ToString())
                {
                    Car car = item;
                    car.TimeBooked++;
                    booking.Car = car;
                }
            }

            List<Customer> allCustomer = new List<Customer>();
            allCustomer = _db.Customers.ToList();
            foreach (var item in allCustomer)
            {
                if (item.SSN == booking.Customer.SSN)
                {
                    Customer customer = item;
                    booking.Customer = customer;
                }
            }
            Logs logs = new Logs();
            logs.Log = $"{booking.Customer.FirstName} hyrde bil {booking.Car.LicensePlate}";
            _db.Add(logs);
            _db.Add(booking);
            _db.SaveChanges();

        }

        public Booking EndBooking(int id)
        {
            return (_db.Bookings.Include(x => x.Car).Include(x => x.Customer).Single(x => x.Id == id));
        }

        public Booking Update(Booking booking)
        {
            booking.Car.NeedsCleaning = true;
            // Bilen ska på service var 3:e bokning
            if (booking.Car.TimeBooked % 3 == 0)
            {
                booking.Car.NeedService = true;
            }
            Logs logs = new Logs();
            logs.Log = $"{booking.Customer.FirstName} lämnade tillbaka bil {booking.Car.LicensePlate}";
            _db.Add(logs);
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
