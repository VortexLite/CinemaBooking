using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ICacheSerializer
    {
        byte[] Serialize<T>(T value);
        T? Deserialize<T>(byte[] bytes);
    }
}
