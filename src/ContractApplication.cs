using src.request;
using src.response;
using System;

namespace src
{
    public class ContractApplication : IContractApplication
    {
        public ContractResponse GenerateRandomResponse(ContractRequest request)
        {
            return new ContractResponse
            {
                contractId = Guid.NewGuid().ToString(),
                contractValue = request.contractValue
            };
        }
    }
}