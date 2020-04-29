using System.Threading.Tasks;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public interface IFakeRespository
    {
        public Task<Fake> GetFake();
    }
}