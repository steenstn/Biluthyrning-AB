using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public class CarRepo : ICarRepo
    {
        private IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;

        public CarRepo(IHostingEnvironment env, ApplicationDbContext db)
        {
            _env = env;
            _db = db;
        }
        public List<Car> AllCars()
        {
            return _db.Cars.ToList(); //_db.Bookings.Include(x => x.Customer).Include(x => x.Car).ToList();
        }
        public List<Car> AllCarsNotBooked()
        {
            return _db.Cars.Where(x => x.IsBooked == false).ToList();
        }

    }
}
