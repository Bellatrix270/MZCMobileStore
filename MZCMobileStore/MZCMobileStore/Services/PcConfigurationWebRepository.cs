using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MZCMobileStore.Services
{
    public class PcConfigurationWebRepository : IPcConfigurationRepository
    {
        //private const string Url = "http://192.168.0.107:3000/api/PcConfigurations/";
        //private readonly HttpClient _httpClient;
        private readonly RestClient _restClient = new RestClient("http://192.168.0.107:3000/api/PcConfigurations");

        public PcConfigurationWebRepository()
        {
            //_httpClient = new HttpClient();
            //_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<PcConfiguration> GetByIdAsync(int id, bool fullConfig = false)
        {
            //http://192.168.0.107:3000/api/PcConfigurations/1?fullConfig=true
            //string result = await _httpClient.GetStringAsync(Url + id + $"?fullConfig={fullConfig}");
            //var pc = JsonConvert.DeserializeObject<PcConfiguration>(result);

            RestRequest request = new RestRequest(id.ToString(), Method.Get);
            request.AddParameter("fullConfig", fullConfig);
            RestResponse response = await _restClient.ExecuteAsync(request);
            var pc = JsonConvert.DeserializeObject<PcConfiguration>(response.Content);

            return pc;
        }

        public Task<IEnumerable<PcConfiguration>> GetByNameAsync(string configName)
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
            //string result = await _httpClient.GetStringAsync(Url);
            //var pcs = JsonConvert.DeserializeObject<IEnumerable<PcConfiguration>>(result);

            RestResponse response = await _restClient.ExecuteAsync(new RestRequest());
            var pcs = JsonConvert.DeserializeObject<IEnumerable<PcConfiguration>>(response.Content);

            return pcs;
        }
    }
}
