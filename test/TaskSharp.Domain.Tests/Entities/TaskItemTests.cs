using TaskSharp.Domain.Entities;
using TaskSharp.Domain.Exceptions;

namespace TaskSharp.Domain.Tests.Entities
{
    public class TaskItemTests 
    {
        [Fact]
        public void Create_Should_CreateTaskItem_WhenParametersAreValid()
        {
            // Arrange
            var title = "Test Task";
            var projectId = 1;
            var assignedUserId = 2;
            // Act
            var taskItem = new TaskItem(title, projectId, assignedUserId);
            // Assert
            Assert.NotNull(taskItem);
            Assert.Equal(title, taskItem.Title);
            Assert.Equal(projectId, taskItem.ProjectId);
            Assert.Equal(assignedUserId, taskItem.AssignedUserId);
        }

        [Fact]
        public void Create_Should_ThrowError_WhenTitleIsEmpty()
        {
            // Arrange
            var title = string.Empty;
            var projectId = 1;
            var assignedUserId = 2;
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new TaskItem(title, projectId, assignedUserId));
        }

        [Fact]
        public void UpdateAssignedUser_Should_UpdateAssignedUserId_WhenParametersAreValid()
        {
            // Arrange
            var title = "Test Task";
            var projectId = 1;
            var assignedUserId = 2;
            var taskItem = new TaskItem(title, projectId, assignedUserId);
            var newAssignedUserId = 3;
            // Act
            taskItem.UpdateAssignedUser(newAssignedUserId);
            // Assert
            Assert.Equal(newAssignedUserId, taskItem.AssignedUserId);
        }

        [Fact]
        public void UpdateDescription_Should_ThrowError_WhenDescriptionIsMoreThan500Chars()
        {
            // Arrange
            var title = "Test Task";
            var projectId = 1;
            var assignedUserId = 2;
            var taskItem = new TaskItem(title, projectId, assignedUserId);
            var longDescription = new string('a', 501);
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateDescription(longDescription));
        }

        [Fact]
        public void UpdateDueDate_Should_ThrowError_WhenDueDateIsEarlierThanCreationDate()
        {
            // Arrange
            var title = "Test Task";
            var projectId = 1;
            var assignedUserId = 2;
            var taskItem = new TaskItem(title, projectId, assignedUserId);
            var invalidDueDate = taskItem.CreatedAt.AddDays(-1);
            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateDueDate(invalidDueDate));
        }

        [Fact]
        public void UpdateDueDate_Should_UpdateDueDate_WhenParametersAreValid()
        {
            // Arrange
            var title = "Test Task";
            var projectId = 1;
            var assignedUserId = 2;
            var taskItem = new TaskItem(title, projectId, assignedUserId);
            var validDueDate = taskItem.CreatedAt.AddDays(5);
            // Act
            taskItem.UpdateDueDate(validDueDate);
            // Assert
            Assert.Equal(validDueDate, taskItem.DueDate);
        }

        [Fact]
        public void UpdateStartDate_Should_ThrowError_When_StartDateIsErlierThanCreationDate()
        {
            // Arrange
            var taskItem = new TaskItem("Test Task", 1, 1);
            var invalidStartDate = taskItem.CreatedAt.AddDays(-1);

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateStartDate(invalidStartDate));
        }

        [Fact]
        public void UpdateStartDate_Should_ThrowError_When_StartDateIsAfterEndDate()
        {
            // Arrange
            var taskItem = new TaskItem("Test Task", 1, 1);
            taskItem.UpdateEndDate(DateTime.UtcNow.AddDays(1));
            var invalidStartDate = taskItem.EndDate!.Value.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateStartDate(invalidStartDate));
        }

        [Fact]
        public void UpdateEndDate_Should_ThrowError_When_EndDateIsErlierThanCreationDate()
        {
            // Arrange
            var taskItem = new TaskItem("Test Task", 1, 1);
            var invalidEndDate = taskItem.CreatedAt.AddDays(-1);

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateEndDate(invalidEndDate));
        }

        [Fact]
        public void UpdateEndDate_Should_ThrowError_When_EndDateIsErlierThanStartDate()
        {
            // Arrange
            var taskItem = new TaskItem("Test Task", 1, 1);
            taskItem.UpdateStartDate(taskItem.CreatedAt.AddDays(1));
            var invalidEndDate = taskItem.StartDate!.Value.AddDays(-1);

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            taskItem.UpdateEndDate(invalidEndDate));
        }
    }
}
