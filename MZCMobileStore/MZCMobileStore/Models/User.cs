using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MZCMobileStore.Models.DTO;
using MZCMobileStore.Models.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Xamarin.Forms;

namespace MZCMobileStore.Models
{
    /// <summary>
    /// Singleton user class
    /// </summary>
    public class User : IEntity
    {
        private static readonly Lazy<User> _instance = new Lazy<User>(() => new User());

        public static User Instance => _instance.Value;
        private static readonly RestClient _restClient = new RestClient("http://192.168.0.107:3000/api/Users");
        private Dictionary<int, int> _cartItems;

        #region Property

        public int Id { get; set; }
        public bool IsAuth { get; private set; }
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

        public Dictionary<int, int> CartItems
        {
            get => _cartItems;
            set
            {
                _cartItems = value;

            }
        }

        #endregion

        private User()
        {
            //_restClient = new RestClient("http://192.168.0.107:3000/api/Users");
            IsAuth = false;
        }

        public async Task<bool> AuthorizationAsync(string password, string login)
        {
            //TODO: Update current user

            RestRequest request = new RestRequest("auth");
            request.AddParameter("password", password);
            request.AddParameter("login", login);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                IsAuth = true;
                var user = JsonConvert.DeserializeObject<UserDto>(response.Content);

                UserFromDto(user);
                await RememberUserAsync();
            }
            
            return IsAuth;
        }

        public async Task<(bool, string)> RegistrationAsync(string name, string phoneNumber, string password, string login)
        {
            FirstName = name;
            Login = login;
            PhoneNumber = phoneNumber;

            var request = new RestRequest("register", Method.Post);
            request.AddBody(new { FirstName = name, PhoneNumber = phoneNumber, Password = password, Login = login });
            var response = await _restClient.ExecutePostAsync(request).ConfigureAwait(false);

            return (response.StatusCode == HttpStatusCode.OK, response.Content?.Trim('"'));
        }

        public async Task<bool> RegistrationConfirmAsync(string phoneNumber, string phoneNumberCode)
        {
            var request = new RestRequest("register/confirm", Method.Post);
            request.AddHeader("userPhoneNumber", phoneNumber);
            request.AddHeader("phoneCode", phoneNumberCode);
            var response = await _restClient.ExecutePostAsync(request).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                IsAuth = true;
                CartItems = new Dictionary<int, int>();

                await RememberUserAsync();
            }
            
            return IsAuth;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (!IsAuth)
                return false;

            var request = new RestRequest(string.Empty, Method.Put);
            request.AddBody(UserToDto());
            var response = await _restClient.ExecuteAsync(request).ConfigureAwait(false);

            return true;
        }

        private void UserFromDto(UserDto user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Patronymic = user.Patronymic;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Login = user.Login;
            Gender = user.Gender;
            BirthDate = user.BirthDate;
            CartItems = user.CartItems;
        }

        private UserDto UserToDto()
        {
            UserDto userDto = new UserDto()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Login = Login,
                Gender = Gender,
                BirthDate = BirthDate,
                CartItems = CartItems,
            };

            return userDto;
        }

        private async Task RememberUserAsync()
        {
            string[] userData = { Password, Login };

            Application.Current.Properties.Add("user", userData);
            await Application.Current.SavePropertiesAsync();
        }
    }
}
