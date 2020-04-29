using System.Threading.Tasks;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public class FakeRespository : IFakeRespository
    {
        /*private readonly _context;

        public async Task<Fake> GetFake()
        {
            var query = _context.getFake();

            return await query.FirstOrDefaultAsync();
        }*/
        public Task<Fake> GetFake()
        {
            throw new System.NotImplementedException();
        }
    }
}