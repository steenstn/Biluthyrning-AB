using BiluthyrningApp.Data;
using BiluthyrningApp.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public class LogsRepo : ILogsRepo
    {
        private IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;

        public LogsRepo(IHostingEnvironment env, ApplicationDbContext db)
        {
            _env = env;
            _db = db;
        }

        public List<Logs> AllLogs()
        {
            return _db.Logs.ToList();
        }

        public List<Logs> ShowCustomerLogs(int id)
        {
            return _db.Logs.Where(x => x.CustomerId == id).ToList();
        }

        public List<Logs> ShowCarLogs(int Id)
        {
            return _db.Logs.Where(x => x.CarId == Id).ToList();
        }
    }
}
