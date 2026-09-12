namespace Movie.Application.Common.Interfaces
{
    public interface ICacheSerializer
    {
        byte[] Serialize<T>(T value);
        T? Deserialize<T>(byte[] bytes);
    }
}
