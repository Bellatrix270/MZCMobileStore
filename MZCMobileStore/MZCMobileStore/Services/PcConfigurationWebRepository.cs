using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.Services.Interfaces;
using Newtonsoft.Json;

namespace MZCMobileStore.Services
{
    public class PcConfigurationWebRepository : IPcConfigurationRepository
    {
        private const string Url = "http://192.168.0.107:3000/api/PcConfigurations/";
        private readonly HttpClient _httpClient;

        public PcConfigurationWebRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public Task<PcConfiguration> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PcConfiguration>> GetByNameAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PcConfiguration>> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PcConfiguration>> GetByRatingAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PcConfiguration>> GeAllAsync()
        {
            string result = await _httpClient.GetStringAsync(Url);
            var pcs = JsonConvert.DeserializeObject<IEnumerable<PcConfiguration>>(result);

            return pcs;
        }
    }
}
