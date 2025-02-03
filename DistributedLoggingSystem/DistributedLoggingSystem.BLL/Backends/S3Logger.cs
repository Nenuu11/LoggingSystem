using DistributedLoggingSystem.BLL.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Backends
{
    public class S3Logger : ILoggerCustom
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _bucketName;

        public S3Logger(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var awsSettings = configuration.GetSection("LoggingConfig:AWSS3");
            _baseUrl = awsSettings["ServiceURL"] ?? "http://localhost:4566";
            _bucketName = awsSettings["BucketName"] ?? "log-storage";
        }

        public async Task AddLogAsync(LogDTO log)
        {
            string logFileName = $"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid()}.json";
            string logData = JsonSerializer.Serialize(log);

            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{_bucketName}/{logFileName}")
            {
                Content = new StringContent(logData, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<LogDTO>> GetLogsAsync()
        {
            var logs = new List<LogDTO>();

            // List objects in the bucket
            var listRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{_bucketName}");
            var listResponse = await _httpClient.SendAsync(listRequest);
            listResponse.EnsureSuccessStatusCode();
            string listContent = await listResponse.Content.ReadAsStringAsync();

            // Extract file names (S3 returns XML, but LocalStack may return JSON)
            var fileNames = ExtractFileNamesFromResponse(listContent);

            foreach (var fileName in fileNames)
            {
                var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{_bucketName}/{fileName}");
                var getResponse = await _httpClient.SendAsync(getRequest);
                getResponse.EnsureSuccessStatusCode();

                var logData = await getResponse.Content.ReadAsStringAsync();
                var logEntry = JsonSerializer.Deserialize<LogDTO>(logData);
                logs.Add(logEntry);
            }

            return logs;
        }

        private List<string> ExtractFileNamesFromResponse(string responseContent)
        {
            var fileNames = new List<string>();

            // Simple parsing method (adjust based on LocalStack's response format)
            var lines = responseContent.Split('\n');
            foreach (var line in lines)
            {
                if (line.Contains("<Key>"))
                {
                    var start = line.IndexOf("<Key>") + 5;
                    var end = line.IndexOf("</Key>");
                    fileNames.Add(line.Substring(start, end - start));
                }
            }

            return fileNames;
        }

    }
}
