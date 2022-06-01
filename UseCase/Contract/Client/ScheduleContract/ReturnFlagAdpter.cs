using ContractReturnFlag = UseCase.Contract.Client.ScheduleContract.ReturnFlag;
using DomainReturnFlag = Domain.Client.ScheduleAgregate.Data.ReturnFlag;

namespace UseCase.Contract.Client.ScheduleContract
{
    // adpter flag thats come from domain to use case contract
    public static class ReturnFlagAdpter
    {
        public static ContractReturnFlag ToContract(this DomainReturnFlag domainReturnFlag)
        {
            switch (domainReturnFlag)
            {
                case DomainReturnFlag.Success:
                    return ContractReturnFlag.Success;
                case DomainReturnFlag.Error:
                    return ContractReturnFlag.Error;
                case DomainReturnFlag.Alert:
                    return ContractReturnFlag.Alert;
                case DomainReturnFlag.InvalidOperation:
                    return ContractReturnFlag.InvalidOperation;
                case DomainReturnFlag.InvalidData:
                    return ContractReturnFlag.InvalidData;
                case DomainReturnFlag.AlreadyExist:
                    return ContractReturnFlag.AlreadyExist;
                case DomainReturnFlag.HasDependency:
                    return ContractReturnFlag.HasDependency;
                case DomainReturnFlag.NoExists:
                    return ContractReturnFlag.NoExists;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
