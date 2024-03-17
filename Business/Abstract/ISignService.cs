using Data;
using Shared;

namespace Business;

public interface ISignService: IService
{
    Task<bool> Login(LoginModel model);
}
