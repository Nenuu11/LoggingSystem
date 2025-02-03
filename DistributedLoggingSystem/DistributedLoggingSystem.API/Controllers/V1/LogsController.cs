using Asp.Versioning;
using DistributedLoggingSystem.API.JWT;
using DistributedLoggingSystem.API.Models;
using DistributedLoggingSystem.BLL.DTOs;
using DistributedLoggingSystem.BLL.Services;
using DistributedLoggingSystem.DAL;
using DistributedLoggingSystem.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;

namespace DistributedLoggingSystem.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private readonly IJWTFactory _jwtFactory;
        public LogsController(ILoggerService loggerService, IJWTFactory jwtFactory)
        {
            _loggerService = loggerService;
            _jwtFactory = jwtFactory;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("v{version:apiVersion}/logs")]
        public async Task<APIResponse> AddLogs([FromBody] LogDTO log)
        {
            long.TryParse(User.FindFirstValue("exp") ?? "".ToString(), out long ticks);
            if (ticks == 0 || !_jwtFactory.IsValidToken(ticks))
            {
                return new APIResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Data = "token is expired, please generate another token"
                };
            }
            await _loggerService.LogAsync(log);
            return new APIResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = "Log written to all enabled backends."
            }; 
        }

        [MapToApiVersion("1.0")]
        [HttpGet("v{version:apiVersion}/logs")]
        public async Task<APIResponse> GetLogs(string? service, string? level, DateTime? start_time, DateTime? end_time)
        {
            long.TryParse(User.FindFirstValue("exp") ?? "".ToString(), out long ticks);
            if (ticks == 0 || !_jwtFactory.IsValidToken(ticks))
            {
                return new APIResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Data = "token is expired, please generate another token"
                };
            }
            var logs = await _loggerService.GetLogs(service, level, start_time, end_time);
            return new APIResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = logs
            };
        }

        [MapToApiVersion("1.0")]
        [HttpGet("v{version:apiVersion}/logs/{id}")]
        public async Task<APIResponse> GetLog(int id)
        {
            long.TryParse(User.FindFirstValue("exp") ?? "".ToString(), out long ticks);
            if (ticks == 0 || !_jwtFactory.IsValidToken(ticks))
            {
                return new APIResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Data = "token is expired, please generate another token"
                };
            }
            var logs = await _loggerService.GetLog(id);
            return new APIResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = logs
            };
        }
    }
}
