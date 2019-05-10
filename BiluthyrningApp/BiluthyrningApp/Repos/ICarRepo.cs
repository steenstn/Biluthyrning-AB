using BiluthyrningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public interface ICarRepo
    {
        List<Car> AllCars();
        List<Car> AllCarsNotBooked();
        void Add(Car car);
        bool CheckIfCarIsOnDatabase(Car car);
        void ServiceCar(int id);
        void CleanCar(int id);
        void RemoveCar(int id);
    }
}
