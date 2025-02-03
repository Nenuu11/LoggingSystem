using DistributedLoggingSystem.BLL.Backends;
using DistributedLoggingSystem.BLL.DTOs;
using DistributedLoggingSystem.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IEnumerable<ILoggerCustom> _loggerCustom;

        private readonly ApplicationDBContext _context;
        public LoggerService(IEnumerable<ILoggerCustom> loggerCustom, ApplicationDBContext context)
        {
            _loggerCustom = loggerCustom;
            _context = context;
        }

        public async Task LogAsync(LogDTO log)
        {
            var tasks = _loggerCustom.Select(backend => backend.AddLogAsync(log));
            await Task.WhenAll(tasks);
        }

        public Task<List<LogDTO>> GetLogs(string? service, string? level, DateTime? start_time, DateTime? end_time)
        {
            return _context.logs.Where(l =>
                   (string.IsNullOrEmpty(service) || l.Service.ToLower() == service.ToLower())
               && (string.IsNullOrEmpty(level) || l.Level.ToLower() == level.ToLower())
               && (start_time == null || l.Timestamp >= start_time.Value)
               && (end_time == null || l.Timestamp <= end_time.Value)
           ).Select(l => new LogDTO
           {
               Id = l.Id,
               Service = l.Service,
               Level = l.Level,
               Message = l.Message,
               Timestamp = l.Timestamp
           }).ToListAsync();
        }

        public Task<LogDTO> GetLog(int id)
        {
            return _context.logs.Select(l => new LogDTO
            {
                Id = id,
                Service = l.Service,
                Level = l.Level,
                Message = l.Message,
                Timestamp = l.Timestamp
            }).FirstOrDefaultAsync(l => l.Id == id);
        }

    }
}
