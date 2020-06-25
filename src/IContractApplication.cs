using src.request;
using src.response;

namespace src
{
    public interface IContractApplication
    {
         ContractResponse GenerateRandomResponse(ContractRequest request);
    }
}