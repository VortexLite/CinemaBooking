using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
