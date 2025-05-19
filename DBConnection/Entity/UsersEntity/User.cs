using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity.UsersEntity
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string  password{ get; set; }
        public List<UserGroup> Groups { get; set; } = new List<UserGroup>();
    }
}
