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

        public void Add(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
        }

        public bool CheckIfCarIsOnDatabase(Car car)
        {
            List<Car> allCars = _db.Cars.ToList();
            foreach (var item in allCars)
            {
                if (item.LicensePlate.ToUpper() == car.LicensePlate.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

    }
}
