using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;

namespace EShop.Domain.Repositories
{
    public interface IAdminConcertRepository
    {
        Task<Concert> AddConcertAsync(Concert concert);
        Task<Concert?> GetConcertByIdAsync(int id);
        Task<Concert> UpdateConcertAsync(Concert concert);
        Task DeleteConcertAsync(int id);
    }
}
