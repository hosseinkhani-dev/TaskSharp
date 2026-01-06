using TaskSharp.Domain.Exceptions;

namespace TaskSharp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? ProfileImageUrl { get; private set; }

        //Relations
        private readonly List<Project> _projects = new();
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

        private List<TaskItem> _taskItem = new();
        public IReadOnlyCollection<TaskItem> TaskItems => _taskItem.AsReadOnly();

        protected User(){}

        public User(
            string username,
            string passwordHash,
            string email)
        {
            SetUsername(username);
            SetPasswordHash(passwordHash);
            SetEmail(email);
        }

        public void ChangeUserName(string username)
        {
            SetUsername(username);
            UpdateDate();
        }

        public void ChangeEmail(string email)
        {
            SetEmail(email);
            UpdateDate();
        }

        public void ChangeProfileImage(string? imageUrl)
        {
            ProfileImageUrl = imageUrl;
            UpdateDate();
        }

        private void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Username is required");
            Username = username;
        }

        private void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password is required");

            PasswordHash = passwordHash;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("email is required");

            if (!email.Contains("@"))
                throw new DomainException("Email is invalid");

            Email = email;
        }
    }
}
