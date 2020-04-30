using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.API.Models.Fake;

namespace Social.API.Services
{
    public class FakeRespository : IFakeRespository
    {
        private readonly DataContext _context;
        public FakeRespository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fake>> GetFakes()
        {
            var query = await _context.Fake.ToListAsync(); 
            
            return query;
        }

        public async Task<Fake> GetFake(int id)
        {
            var query = await _context.Fake.FirstOrDefaultAsync(x => x.Id == id); 
            
            return query;
        }

        public async void PostFake(Fake fake)
        {
            _context.Fake.Add(fake);
            await _context.SaveChangesAsync();
        }

        public async void PutFake(Fake fake)
        {
             _context.Entry(fake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            } 
        }

        public async void DeleteFake(Fake fake)
        {
            _context.Fake.Remove(fake);
            await _context.SaveChangesAsync();
        }
    }
}