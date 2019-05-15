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
    public class CustomerRepo : ICustomerRepo
    {
        private IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;

        public CustomerRepo(IHostingEnvironment env, ApplicationDbContext db)
        {
            _env = env;
            _db = db;
        }
        public List<Customer> ShowListOfCustomers()
        {
            return _db.Customers.ToList();
        }

        public List<Booking> ShowBookings(int? Id)
        {
      
            return _db.Bookings.Include(x => x.Car).Include(x => x.Customer).Where(x => x.Customer.Id == Id).ToList();
         
        }

        public void Add(Customer customer)
        {
            
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public List<Customer> AllCustomers()
        {
           return _db.Customers.ToList();
        }
    }
}
