using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace EShop.Domain.Repositories
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly DataContext _context;

        public ConcertRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Concert>> GetAllAsync()
        {
            return await _context.concerts.ToListAsync();
        }

        public async Task<Concert?> GetByIdAsync(int id)
        {
            return await _context.concerts.FindAsync(id);
        }

        public async Task<List<Concert>> SearchAsync(string? artist, string? city)
        {
            var query = _context.concerts.AsQueryable();

            if (!string.IsNullOrEmpty(artist))
                query = query.Where(c => c.artist.Contains(artist));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(c => c.city.Contains(city));

            return await query.ToListAsync();
        }
    }

}
