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
            return _db.Cars.Where(x => x.IsBooked == false && x.CarRemoved == false).ToList();
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
                if (item.LicensePlate == null)
                {
                    return false;

                } else if (item.LicensePlate.ToUpper() == car.LicensePlate.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public void ServiceCar(int id)
        {
            Car car = _db.Cars.Single(x => x.Id == id);
            car.NeedService = false;
            Logs logs = new Logs();
            logs.Log = $"{car.LicensePlate} skickades på service";
            logs.CarId = car.Id;
            _db.Add(logs);
            _db.Update(car);
            _db.SaveChanges();
        }
        public void CleanCar(int id)
        {
            
            Car car = _db.Cars.Single(x => x.Id == id);
            car.NeedsCleaning = false;
            Logs logs = new Logs();
            logs.Log = $"{car.LicensePlate} städades";
            logs.CarId = car.Id;
            _db.Add(logs);
            _db.Update(car);
            _db.SaveChanges();
        }
        public void RemoveCar(int id)
        {
            Car car = _db.Cars.Single(x => x.Id == id);
            car.CarRemoved = true;    
            _db.Update(car);
            _db.SaveChanges();
        }



    }
}
