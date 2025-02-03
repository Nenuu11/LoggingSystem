using DistributedLoggingSystem.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLoggingSystem.BLL.Backends
{
    public interface ILoggerCustom
    {
        Task AddLogAsync(LogDTO log);
    }
}
