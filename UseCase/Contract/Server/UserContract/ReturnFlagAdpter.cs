﻿using ContractReturnFlag = UseCase.Contract.Server.UserContract.ReturnFlag;
using DomainReturnFlag = Domain.Server.UserAgregate.Data.ReturnFlag;

namespace UseCase.Contract.Server.UserContract
{
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
