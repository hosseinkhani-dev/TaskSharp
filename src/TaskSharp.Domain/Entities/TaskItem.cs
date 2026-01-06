using TaskSharp.Domain.Exceptions;
using static TaskSharp.Domain.Entities.ProjectMember;

namespace TaskSharp.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public TaskStatus TaskStatus { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        // Project
        public int ProjectId { get; private set; }
        public Project? Project { get; private set; }

        // Assigned User
        public int AssignedUserId { get; private set; }
        public User? AssignedUser { get; private set; }

        // Time Spam
        public TimeSpan? Duration => (StartDate.HasValue && EndDate.HasValue)
           ? EndDate - StartDate
           : null;

        protected TaskItem() { }

        public TaskItem(
            string title,
            int projectId,
            int assignedUserId)
        {
            SetTitle(title);
            ProjectId = projectId;
            AssignedUserId = assignedUserId;
            TaskStatus = TaskStatus.Todo;
        }

        public void UpdateTitle(string title)
        {
            SetTitle(title);
            UpdateDate();
        }

        public void UpdateAssignedUser(int userId)
        {
            AssignedUserId = userId;
            UpdateDate();
        }

        public void UpdateDescription(string? description)
        {
            if (description?.Length > 500)
                throw new DomainException(
                    "Description cannot be more than 500 characters.");

            Description = description;
            UpdateDate();
        }

        public void UpdateTaskStatus(TaskStatus taskStatus)
        {
            TaskStatus = taskStatus;
            UpdateDate();
        }

        public void UpdateDueDate(DateTime? dueDate)
        {
            if (dueDate < CreatedAt)
                throw new DomainException(
                    "Due date cannot be earlier than creation date.");
            DueDate = dueDate;
            UpdateDate();
        }

        public void UpdateStartDate(DateTime? startDate)
        {
            if (startDate < CreatedAt)
                throw new DomainException(
                    "Start date cannot be earlier than creation date.");

            if (EndDate.HasValue && startDate > EndDate)
                throw new DomainException(
                    "Start date cannot be after the end date.");

            StartDate = startDate;
            UpdateDate();
        }

        public void UpdateEndDate(DateTime? endDate)
        {
            if (endDate < CreatedAt)
                throw new DomainException(
                    "End date cannot be earlier than creation date.");

            if(endDate < StartDate)
                throw new DomainException(
                    "End date cannot be earlier than start date.");

            EndDate = endDate;
            UpdateDate();
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required");
            Title = title;
        }
    }

    public enum TaskStatus
    {
        Todo,
        InProgress,
        Completed,
        Archived
    }
}
