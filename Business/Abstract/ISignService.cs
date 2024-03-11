using Data;
using Shared;

namespace Business;

public interface ISignService
{
    string? Message {get;set;}
    TokenModel? TokenModel {get;set;}
    Task<bool> Login(LoginModel model);
}
