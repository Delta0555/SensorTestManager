using System.Collections.Generic;
using System.Linq;

namespace SensorDataService.Authentication {
    public class ServiceUser {
        public int ID { get; private set; }
        public string Login { get; private set; }

        public string Pass { get; internal set; }

        public List<UserRoles> Roles { get; private set; }

        public ServiceUser(int nID, string sLogin, string sPass, List<UserRoles> pRoles) {
            ID = nID;
            Login = sLogin;
            Pass = sPass;
            Roles = pRoles;
        }
    }

    public enum UserRoles {
        Admin,
        ResultCreator
    }

    public static class ExtensionMethods {
        public static IEnumerable<ServiceUser> WithoutPasswords(this IEnumerable<ServiceUser> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static ServiceUser WithoutPassword(this ServiceUser user) {
            user.Pass = null;
            return user;
        }
    }
}
