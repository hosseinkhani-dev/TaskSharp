namespace TaskSharp.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        protected void UpdateDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
