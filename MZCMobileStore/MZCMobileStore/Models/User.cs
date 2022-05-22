using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MZCMobileStore.Models
{
    /// <summary>
    /// Singleton user class
    /// </summary>
    public class User
    {
        private static readonly Lazy<User> _instance = new Lazy<User>(() => new User());

        public static User Instance => _instance.Value;
        private readonly RestClient _restClient;

        #region Property

        public int Id { get; set; }
        public bool IsAuth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] AvatarImage { get; set; }

        #endregion

        private User()
        {
            _restClient = new RestClient("http://192.168.0.107:3000/api/Users");
            IsAuth = false;
        }

        public async Task AuthorizationAsync(string password, string login)
        {
            password = "toP21MobiLLEst93Ore";
            RestRequest request = new RestRequest("auth");
            request.AddParameter("password", password);
            request.AddParameter("login", login);
            RestResponse response = await _restClient.ExecuteAsync(request).ConfigureAwait(false);
            var content = await _restClient.GetJsonAsync<User>(response.Content).ConfigureAwait(false);
        }

        public async Task RegistrationAsync()
        {

        }
    }
}
