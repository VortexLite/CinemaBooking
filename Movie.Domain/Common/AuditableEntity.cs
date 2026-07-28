using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity, IAuditable, ISoftDelete
    {
        public DateTime CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
        public bool IsDeleted { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }
    }
}
