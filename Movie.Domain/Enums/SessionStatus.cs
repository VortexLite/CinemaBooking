namespace Movie.Domain.Enums
{
    public enum SessionStatus : byte
    {
        Scheduled = 1,

        OpenForBooking = 2,

        Started = 3,

        Finished = 4,

        Cancelled = 5
    }
}
