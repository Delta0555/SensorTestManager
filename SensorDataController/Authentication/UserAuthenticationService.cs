using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorDataService.Authentication {
    public interface IUserService {
        Task<ServiceUser> Authenticate(string username, string password);
        Task<IEnumerable<ServiceUser>> GetAll();
    }

    public class UserAuthenticationService : IUserService {
        private List<ServiceUser> __pUsers;
        public UserAuthenticationService(List<ServiceUser> pUsers) {
            __pUsers = pUsers ?? new List<ServiceUser>(0);

        }
       
        public async Task<ServiceUser> Authenticate(string username, string password) {
            var user = await Task.Run(() => __pUsers.SingleOrDefault(x => x.Login == username && x.Pass == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<ServiceUser>> GetAll() {
            return await Task.Run(() => __pUsers.WithoutPasswords());
        }
    }
}
