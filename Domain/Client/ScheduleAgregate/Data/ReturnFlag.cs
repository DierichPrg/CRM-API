namespace Domain.Client.ScheduleAgregate.Data
{
    [Flags]
    public enum ReturnFlag : int
    {
        Success = 0,
        Error = 1 << 1,
        Alert = 1 << 2,
        InvalidOperation = 1 << 3,
        InvalidData = 1 << 4,
    }
}
