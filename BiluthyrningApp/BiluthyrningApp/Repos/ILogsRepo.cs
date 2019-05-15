using BiluthyrningApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Repos
{
    public interface ILogsRepo
    {
        List<Logs> AllLogs();
        List<Logs> ShowCustomerLogs(int id);
        List<Logs> ShowCarLogs(int Id);
    }
}
