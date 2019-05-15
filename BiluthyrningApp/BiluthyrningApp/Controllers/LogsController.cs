using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BiluthyrningApp.Controllers
{
    public class LogsController : Controller
    {
        private IBookingRepo _bookingRepo;
        private ICarRepo _carRepo;
        private ICustomerRepo _customerRepo;
        private ILogsRepo _logsRepo;


        public LogsController(IBookingRepo bookingRepo, ICarRepo carRepo, ICustomerRepo customerRepo, ILogsRepo logsRepo)
        {
            _bookingRepo = bookingRepo;
            _carRepo = carRepo;
            _customerRepo = customerRepo;
            _logsRepo = logsRepo;
        }

        public IActionResult ShowAllLogs()
        {
            return View(_logsRepo.AllLogs());
        }
    }
}