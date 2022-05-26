using System;
using System.Collections.Generic;
using System.Text;

namespace MZCMobileStore.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] AvatarImage { get; set; }

        public Dictionary<int, int> CartItems { get; set; }
    }

}
