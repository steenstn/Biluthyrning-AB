using BiluthyrningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public interface IBookingRepo
    {
        List<Booking> GetBookings();
        void Add(Booking booking);
        Booking EndBooking(int id);
        Booking Update(Booking booking);
        List<Booking> ShowAllActiveBookings();
    }
}
