using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorClicker.Authentication
{
    public class UserAccountService
    {
        public event Action<List<UserAccount>> UsersChanged;

        public List<UserAccount> Users
        {
            get => _users;
            set
            {
                _users = value;
                UsersChanged.Invoke(_users);
            }
        }

        private List<UserAccount> _users;

        public UserAccountService()
        {
            _users = new List<UserAccount>
            {
                new UserAccount{ UserName = "VamoniS", Password = "VamoniS", Role = "Administrator", Level = 1, LevelUpNeeded = 100 },
                new UserAccount{ UserName = "Lairon", Password = "Lairon", Role = "Administrator", Level = 1, LevelUpNeeded = 100 },
                new UserAccount{ UserName = "Мамаев", Password = "Мамаев", Role = "User", Level = 1, LevelUpNeeded = 100 },
                new UserAccount{ UserName = "Егор", Password = "Егор", Role = "User", Level = 1, LevelUpNeeded = 100 },
                new UserAccount{ UserName = "Deniska", Password = "Deniska", Role = "User", Level = 1, LevelUpNeeded = 100 },
                new UserAccount{ UserName = "Urofile", Password = "Urofile", Role = "User", Level = 1, LevelUpNeeded = 100 },
            };
            _users.Sort((x, y) => y.Balance.CompareTo(x.Balance));
        }

        public UserAccount? GetUserByUserName(string? userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }

        public UserAccount? GetUserByState(AuthenticationState state)
        {
            var userName = state.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (userName == null)
            {
                return null;
            }

            return GetUserByUserName(userName.Value);
        }

        public void IncrementBalance(string userName)
        {
            var user = _users.FirstOrDefault(x => x.UserName == userName);

            user.Balance++;

            if (user.Balance > user.LevelUpNeeded)
            {
                user.Level += 1;
                user.Balance -= user.LevelUpNeeded;
                user.LevelUpNeeded += 100;
            }

            user.Progress = user.Balance * 100 / user.LevelUpNeeded;

            _users.Sort((x, y) => y.Level.CompareTo(x.Level));
            UsersChanged.Invoke(_users);
        }
    }
}
