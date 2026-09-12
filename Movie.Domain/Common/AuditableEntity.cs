namespace Movie.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity, IAuditable, ISoftDelete
    {
        public DateTime CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }

        protected void MarkDeleted()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        protected void MarkRestored()
        {
            if (!IsDeleted)
                return;

            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
