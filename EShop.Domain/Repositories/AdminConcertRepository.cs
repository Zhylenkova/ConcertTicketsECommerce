using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;

namespace EShop.Domain.Repositories
{
    public class AdminConcertRepository : IAdminConcertRepository
    {
        private readonly DataContext _context;

        public AdminConcertRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Concert> AddConcertAsync(Concert concert)
        {
            _context.concerts.Add(concert);
            await _context.SaveChangesAsync();
            return concert;
        }

        public async Task<Concert?> GetConcertByIdAsync(int id)
        {
            return await _context.concerts.FindAsync(id);
        }

        public async Task<Concert> UpdateConcertAsync(Concert concert)
        {
            _context.concerts.Update(concert);
            await _context.SaveChangesAsync();
            return concert;
        }

        public async Task DeleteConcertAsync(int id)
        {
            var concert = await _context.concerts.FindAsync(id);
            if (concert != null)
            {
                _context.concerts.Remove(concert);
                await _context.SaveChangesAsync();
            }
        }
    }
}