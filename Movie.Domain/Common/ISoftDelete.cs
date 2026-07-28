using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }

        DateTime? DeletedAt { get; }
    }
}
