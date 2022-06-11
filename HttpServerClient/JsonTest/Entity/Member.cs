using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.Entity
{
    public class Member
    {
        private string _id;
        private string _name;
        private string _email;
        private string _password;

        public Member(string id, string name, string email, string password)
        {
            _id = id;
            _name = name;
            _email = email;
            _password = password;
        }

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
