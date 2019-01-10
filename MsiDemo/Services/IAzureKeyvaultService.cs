using System.Threading.Tasks;

namespace MsiDemo.Services
{
    public interface IAzureKeyvaultService
    {
        Task<string> GetSecret();
    }
}
