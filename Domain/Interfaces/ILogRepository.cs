using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetLogs();
        Task<Log> GetLogById(int id);
        Task<Log> CreateLog(Log log);
    }
}
