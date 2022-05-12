using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Models;

namespace MZCMobileStore.Services.Interfaces
{
    public interface IPcConfigurationRepository
    {
        Task<PcConfiguration> GetByIdAsync(int id);
        Task<IEnumerable<PcConfiguration>> GetByNameAsync();
        Task<IEnumerable<PcConfiguration>> GetByCategoryAsync(string category);
        Task<IEnumerable<PcConfiguration>> GetByRatingAsync();
        Task<IEnumerable<PcConfiguration>> GeAllAsync();
    }
}
