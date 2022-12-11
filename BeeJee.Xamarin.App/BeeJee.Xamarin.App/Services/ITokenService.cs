using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Auth;
using System.Threading.Tasks;

namespace BeeJee.Xamarin.App.Services
{
    public interface ITokenService
    {
        Task<ServiceResult<TokenResponse>> GetToken(Credentials credentials);
    }
}
