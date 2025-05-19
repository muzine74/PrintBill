using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Entity.UsersEntity
{
    public static class PermissionManager
    {
        private static List<UserGroup> availableGroups = new List<UserGroup>();

        static PermissionManager()
        {
            // Initialize groups with their permissions
            var adminGroup = new UserGroup { Name = "Administrators" };
            adminGroup.Permissions.AddRange(Enum.GetValues(typeof(PermissionType)).Cast<PermissionType>());

            var editorGroup = new UserGroup { Name = "Editors" };
            editorGroup.Permissions.Add(PermissionType.Read);
            editorGroup.Permissions.Add(PermissionType.Write);

            var viewerGroup = new UserGroup { Name = "Viewers" };
            viewerGroup.Permissions.Add(PermissionType.Read);

            availableGroups.AddRange(new[] { adminGroup, editorGroup, viewerGroup });
        }

        public static bool CheckPermission(User user, PermissionType permission)
        {
            return user.Groups.Any(g => g.Permissions.Contains(permission));
        }

        public static List<UserGroup> GetAvailableGroups()
        {
            return availableGroups;
        }
    }
}
