using TaskSharp.Domain.Entities;
using TaskSharp.Domain.Exceptions;

namespace TaskSharp.Domain.Tests.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void Create_Should_CreateProject_WhenParametersAreValid()
        {
            // Arrange
            var name = "Test Project";
            var ownerId = 1;
            var description = "This is a test project.";
            // Act
            var project = new Project(name, ownerId, description);
            // Assert
            Assert.NotNull(project);
            Assert.Equal(name, project.Name);
            Assert.Equal(ownerId, project.OwnerId);
            Assert.Equal(description, project.Description);
            Assert.Single(project.ProjectMembers);
            Assert.Equal(ownerId, project.ProjectMembers.First().UserId);
            Assert.Equal(ProjectMember.ProjectRole.Admin,
                project.ProjectMembers.First().Role);
        }

        [Fact]
        public void Create_Should_ThrowError_WhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var ownerId = 1;
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Project(name, ownerId));
        }

        [Fact]
        public void Create_Should_ThrowError_WhenDescriptionIsMoreThan500Chars()
        {
            // Arrange
            var name = "Test Project";
            var ownerId = 1;
            var description = new string('a', 501);
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Project(name, ownerId, description));
        }

        [Fact]
        public void GrantAccess_Should_AddProjectMember_WhenParametersAreValid()
        {
            // Arrange
            var project = new Project("Test Project", 1);
            var userId = 2;
            var role = ProjectMember.ProjectRole.Contributor;
            // Act
            project.GrantAccess(userId, role);
            // Assert
            Assert.Equal(2, project.ProjectMembers.Count);
            var member = project.ProjectMembers
                .FirstOrDefault(pm => pm.UserId == userId);
            Assert.NotNull(member);
            Assert.Equal(role, member!.Role);
        }

        [Fact]
        public void GrantAccess_Should_ThrowError_WhenUserAlreadyHasAccess()
        {
            // Arrange
            var project = new Project("Test Project", 1);
            var userId = 2;
            project.GrantAccess(userId, ProjectMember.ProjectRole.Viewer);
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            project.GrantAccess(userId, ProjectMember.ProjectRole.Contributor));
        }

        [Fact]
        public void ChangeMemberRole_Should_UpdateRole_WhenParametersAreValid()
        {
            // Arrange
            var project = new Project("Test Project", 1);
            var userId = 2;
            project.GrantAccess(userId, ProjectMember.ProjectRole.Viewer);
            var newRole = ProjectMember.ProjectRole.Contributor;
            // Act
            project.ChangeMemberRole(userId, newRole);
            // Assert
            var member = project.ProjectMembers
                .FirstOrDefault(pm => pm.UserId == userId);
            Assert.NotNull(member);
            Assert.Equal(newRole, member!.Role);
        }

        [Fact]
        public void ChangeMemberRole_Should_ThrowError_WhenUserIsNotMember()
        {
            // Arrange
            var project = new Project("Test Project", 1);
            var userId = 2;
            var newRole = ProjectMember.ProjectRole.Contributor;
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            project.ChangeMemberRole(userId, newRole));
        }
    }
}
