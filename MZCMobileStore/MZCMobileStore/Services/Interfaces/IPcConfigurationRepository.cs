using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Models;

namespace MZCMobileStore.Services.Interfaces
{
    public interface IPcConfigurationRepository
    {
        Task<PcConfiguration> GetByIdAsync(int id, bool fullConfig);
        Task<IEnumerable<PcConfiguration>> GetByNameAsync(string configName);
        Task<IEnumerable<PcConfiguration>> GetByCategoryAsync(string category);
        Task<IEnumerable<PcConfiguration>> GetByRatingAsync();
        Task<IEnumerable<PcConfiguration>> GeAllAsync();
    }
}
