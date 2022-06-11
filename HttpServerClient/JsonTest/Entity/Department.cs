using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest.Entity
{
    public class Department
    {
        private string _id;
        private string _name;
        private string _description;
        private List<Member> _members = new List<Member>();

        public Department(string id, string name, string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public List<Member> Members { get => _members; }
    }
}
