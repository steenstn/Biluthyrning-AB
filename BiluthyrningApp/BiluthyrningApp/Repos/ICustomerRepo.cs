using BiluthyrningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public interface ICustomerRepo
    {
        List<Customer> ShowListOfCustomers();
        List<Booking> ShowBookings(int? id);
    }
}
