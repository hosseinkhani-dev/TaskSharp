namespace TaskSharp.Domain.Entities
{
    public class ProjectMember : BaseEntity
    {
        public int UserId { get; private set; }
        public int ProjectId { get; private set; }
        public ProjectRole Role { get; private set; }

        protected ProjectMember(){}

        public ProjectMember(int userId, int projectId, ProjectRole role)
        {
            UserId = userId;
            ProjectId = projectId;
            Role = role;
        }

        public void UpdateRole(ProjectRole newRole)
        {
            Role = newRole;
        }

        public enum ProjectRole
        {
            Viewer,
            Contributor,
            Admin
        }
    }
}
