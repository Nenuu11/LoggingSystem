using DistributedLoggingSystem.BLL.DTOs;
using DistributedLoggingSystem.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Backends
{
    public class FileLogger : ILoggerCustom
    {
        private readonly string _logDirectory;
        public FileLogger(IConfiguration config)
        {
            _logDirectory = config["LoggingConfig:FileSystem:LogDirectory"];
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }
        public async Task AddLogAsync(LogDTO log)
        {
            string filePath = Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyyMMdd}.log");
            List<LogDTO> logs;

            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);

                logs = string.IsNullOrEmpty(jsonData)
                    ? new List<LogDTO>()
                    : JsonConvert.DeserializeObject<List<LogDTO>>(jsonData);
            }
            else
            {
                logs = new List<LogDTO>();
            }
            logs.Add(log);

            var updatedJsonData = JsonConvert.SerializeObject(logs, Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonData);

        }

        //public async Task<List<LogDTO>> GetLogs(string? service, string? level, DateTime? start_time, DateTime? end_time)
        //{
        //    var logs = new List<LogDTO>();

        //    if (!string.IsNullOrEmpty(service))
        //        logs = logs.Where(log => log.Service == service).ToList();

        //    if (!string.IsNullOrEmpty(level))
        //        logs = logs.Where(log => log.Level == level).ToList();

        //    if (start_time.HasValue)
        //        logs = logs.Where(log => log.Timestamp >= start_time.Value).ToList();

        //    if (end_time.HasValue)
        //        logs = logs.Where(log => log.Timestamp <= end_time.Value).ToList();

        //    return await Task.FromResult(logs); // Return logs asynchronously
        //}
    }
}
