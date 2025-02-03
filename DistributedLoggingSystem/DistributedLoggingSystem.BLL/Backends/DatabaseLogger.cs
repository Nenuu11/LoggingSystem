using DistributedLoggingSystem.BLL.DTOs;
using DistributedLoggingSystem.DAL;
using DistributedLoggingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Backends
{
    public class DatabaseLogger : ILoggerCustom
    {
        private readonly ApplicationDBContext _context;

        public DatabaseLogger(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(LogDTO log)
        {
            var _log = new Log
            {
                Level = log.Level,
                Message = log.Message,
                Timestamp = log.Timestamp,
                Service = log.Service
            };

            _context.logs.Add(_log);
            await _context.SaveChangesAsync();
        }

        //public Task<List<LogDTO>> GetLogs(string? service, string? level, DateTime? start_time, DateTime? end_time)
        //{
        //    return _context.logs.Where(l =>
        //            (string.IsNullOrEmpty(service) || l.Service.ToLower() == service.ToLower())
        //        && (string.IsNullOrEmpty(level) || l.Level.ToLower() == level.ToLower())
        //        && (start_time == null || l.Timestamp >= start_time.Value)
        //        && (end_time == null || l.Timestamp <= end_time.Value)
        //    ).Select(l => new LogDTO
        //    {
        //        Service = l.Service,
        //        Level = l.Level,
        //        Message = l.Message,
        //        Timestamp = l.Timestamp
        //    }).ToListAsync();
        //}
    }
}
