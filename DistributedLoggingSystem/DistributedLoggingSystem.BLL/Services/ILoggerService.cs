using DistributedLoggingSystem.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Services
{
    public interface ILoggerService
    {

        Task LogAsync(LogDTO log);
        Task<List<LogDTO>> GetLogs(string? service, string? level, DateTime? start_time, DateTime? end_time);
        Task<LogDTO> GetLog(int id);

    }
}
