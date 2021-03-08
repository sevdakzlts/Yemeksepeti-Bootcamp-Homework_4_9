using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PathSettings _settings;
        private IConfiguration Configuration;

        public Worker(ILogger<Worker> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;

            _settings.Path = _configuration.GetSection("PathOfDirectory").Value;
            int days = 0;
            int.TryParse(_configuration.GetSection("OlderThanDays").Value, out days);
            _settings.DistanceTime = DateTime.Now.AddDays(days);
            
            _settings.PathInfo = new DirectoryInfo(_settings.Path);
            _settings.ExePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _settings.FilePath = Path.GetFullPath(_settings.ExePath + _configuration.GetSection("FileSavePath").Value);

            
        }

    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var files = _settings.PathInfo.EnumerateDirectories("*")
                    .OrderByDescending(d => d.LastAccessTime).ToList();
                
                 foreach (var file in files)
                 {
                         DateTime time = DateTime.Now;
                         string detail = $"{DateTime.Now}" + $"\t{file.Root}" + $"\t {file.Name}" + $"\t {file.LastAccessTime}\n";
                         if ((DateTime.Now - file.LastAccessTime).TotalDays > _settings.DistanceTime.Day)
                         {
                            File.AppendAllText(_settings.FilePath, detail);

                         }
                 }
                 

                _logger.LogInformation("files.Count: {t} {time}", files, DateTimeOffset.Now);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromHours(4));
            }
        }

    }

    public struct PathSettings
    {
        public string Path;
        public DirectoryInfo PathInfo;
        public string ExePath;
        public string FilePath;
        public DateTime DistanceTime;
    }
}
