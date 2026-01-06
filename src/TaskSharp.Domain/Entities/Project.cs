using TaskSharp.Domain.Exceptions;
using static TaskSharp.Domain.Entities.ProjectMember;

namespace TaskSharp.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        // Owner
        public int OwnerId { get; private set; }
        public User? User { get; private set; }

        //Tasks
        private readonly List<TaskItem> _taskItems = new();
        public IReadOnlyCollection<TaskItem> TaskItems => _taskItems.AsReadOnly();

        //Project Members
        private readonly List<ProjectMember> _projectMembers = new();
        public IReadOnlyCollection<ProjectMember> ProjectMembers =>
            _projectMembers.AsReadOnly();

        protected Project() { }

        public Project(
            string name,
            int ownerId,
            string? description = null)
        {
            SetName(name);
            OwnerId = ownerId;
            SetDescription(description);

            _projectMembers.Add(
                new ProjectMember(ownerId, Id, ProjectRole.Admin));
        }

        public void GrantAccess(int userId, ProjectRole role)
        {
            if (_projectMembers.Any(pm => pm.UserId == userId))
                throw new DomainException(
                    "User already has access to the project");

            _projectMembers.Add(new ProjectMember(userId, Id, role));
            UpdateDate();
        }

        public void ChangeMemberRole(int userId, ProjectRole newRole)
        {
            var member = _projectMembers.FirstOrDefault(
                pm => pm.UserId == userId);

            if(member == null)
                throw new DomainException(
                    "User is not a member of the project.");

            member.UpdateRole(newRole);
            UpdateDate();
        }

        public void UpdateName(string name)
        {
            SetName(name);
            UpdateDate();
        }

        public void UpdateDescription(string? description)
        {
            SetDescription(description);
            UpdateDate();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Project name is required");

            Name = name;
        }

        private void SetDescription(string? description)
        {
            if (description?.Length > 500)
                throw new DomainException(
                    "Description cannot exceed 500 characters");
            Description = description;
        }
    }
}
