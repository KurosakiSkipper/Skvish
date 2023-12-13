namespace BlazorClicker.Authentication
{
    public class UserAccount
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public int Balance { get; set; }

        public int Level { get; set; }

        public float Progress { get; set; }

        public int LevelUpNeeded { get; set; }
    }
}
