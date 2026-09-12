using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ISessionLifecycleProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken = default);
    }
}
