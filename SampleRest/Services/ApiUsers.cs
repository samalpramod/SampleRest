using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampleRest.Entities;

namespace SampleRest.Services
{
    public class ApiUsers : IApiUsers
    {
        private IReadOnlyDictionary<string, string>? users;
        public ApiUsers(APIUsersConfig config)
        {
            users = config.ApiUsers;
        }

        public bool Authenticate(string login, string password)
        {
            if (users != null && users.ContainsKey(login) && users[login] == password)
            {
                return true;
            }
            return false;
        }
    }
}
