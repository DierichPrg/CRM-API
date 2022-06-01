namespace Contract.Client.CustomerContract
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
        HasDependency = 1 << 6,
        NoExists = 1 << 7,
    }
}
