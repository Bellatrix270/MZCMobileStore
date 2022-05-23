using System;
using System.Collections.Generic;
using System.Net;
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
        private static readonly RestClient _restClient = new RestClient("http://192.168.0.107:3000/api/Users");

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
            //_restClient = new RestClient("http://192.168.0.107:3000/api/Users");
            IsAuth = false;
        }

        public async Task AuthorizationAsync(string password, string login)
        {
            RestRequest request = new RestRequest("auth");
            request.AddParameter("password", password);
            request.AddParameter("login", login);
            RestResponse response = await _restClient.ExecuteAsync(request).ConfigureAwait(false);
            var content = await _restClient.GetJsonAsync<User>(response.Content).ConfigureAwait(false);
        }

        public async Task RegistrationAsync(string name, string phoneNumber, string password, string login)
        {
            var request = new RestRequest("register", Method.Post);
            request.AddBody(new { FirstName = name, PhoneNumber = phoneNumber, Password = password, Login = login });
            var response = await _restClient.ExecutePostAsync(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
                IsAuth = true;
        }

        public static async Task<bool> CheckLoginToUnique(string login)
        {
            var request = new RestRequest("IsUnique");
            request.AddParameter("login", login);
            var response = await _restClient.ExecuteAsync(request).ConfigureAwait(false);
            return Convert.ToBoolean(response.Content);
        }
    }
}
