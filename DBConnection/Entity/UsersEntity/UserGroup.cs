using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity.UsersEntity
{
    public enum PermissionType
    {
        Read,
        Write,
        Delete,
        Admin
    }
    public class UserGroup
    {
        public string Name { get; set; }
        public List<PermissionType> Permissions { get; set; } = new List<PermissionType>();
    }
}
