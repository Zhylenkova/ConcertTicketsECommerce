using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;

namespace EShop.Domain.Repositories
{
    public interface IConcertRepository
    {
        Task<List<Concert>> GetAllAsync();
        Task<Concert?> GetByIdAsync(int concert_id);
        Task<List<Concert>> SearchAsync(string? artist, string? city);
    }
}
