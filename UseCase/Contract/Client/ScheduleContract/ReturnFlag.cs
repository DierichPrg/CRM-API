namespace UseCase.Contract.Client.ScheduleContract
{
    [Flags]
    public enum ReturnFlag : int
    {
        Success = 0,
        Error = 1 << 1,
        Alert = 1 << 2,
        InvalidOperation = 1 << 3,
        InvalidData = 1 << 4,
        AlreadyExist = 1 << 5,
        NoExists = 1 << 6,
        HasDependency = 1 << 7,
    }
}
