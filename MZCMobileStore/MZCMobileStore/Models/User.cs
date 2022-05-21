using System;
using System.Collections.Generic;
using System.Text;

namespace MZCMobileStore.Models
{
    /// <summary>
    /// Singleton user class
    /// </summary>
    public class User
    {
        private static readonly Lazy<User> _instance = new Lazy<User>(() => new User());

        public static User Instance => _instance.Value;

        private User()
        {
            
        }
    }
}
