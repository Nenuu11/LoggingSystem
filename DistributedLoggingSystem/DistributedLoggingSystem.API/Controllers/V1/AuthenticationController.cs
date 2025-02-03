using Asp.Versioning;
using DistributedLoggingSystem.API.JWT;
using DistributedLoggingSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DistributedLoggingSystem.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTFactory _jwtFactory;
        private readonly IConfiguration _configuration;
        public AuthenticationController(IJWTFactory jwtFactory, IConfiguration configuration)
        {
            _jwtFactory = jwtFactory;
            _configuration = configuration;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("v{version:apiVersion}/get-token")]
        public APIResponse GenerateToken()
        {
            return new APIResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = _jwtFactory.GenerateToken()
            };
        }
    }
}
