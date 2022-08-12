using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal class User
    {
        public string? username { get; set; }

        public string? password { get; set; }

        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public string? age { get; set; }

        public DateTime created { get; set; }

        public static User CreateUser(string? _username, string? _password, string? _firstname, string? _lastname, string? _age, DateTime _created)
        {
            User tmp = new User();
            tmp.username = _username;
            tmp.password = _password;
            tmp.firstName = _firstname;
            tmp.lastName = _lastname;
            tmp.age = _age;
            tmp.created = _created;

            return tmp;
        }
    }
}
