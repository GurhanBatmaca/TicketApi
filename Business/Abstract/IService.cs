using Shared;

namespace Business;

public interface IService
{
    SuccessResponse? SuccessResponse {get;set;}
    ErrorResponse? ErrorResponse {get;set;}
}