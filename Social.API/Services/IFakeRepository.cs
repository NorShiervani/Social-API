using System.Collections.Generic;
using System.Threading.Tasks;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public interface IFakeRespository
    {
        public Task<IEnumerable<Fake>> GetFakes();
        public Task<Fake> GetFake(int id);

        public void PostFake(Fake fake);
    }
}