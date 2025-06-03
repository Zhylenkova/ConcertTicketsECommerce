using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;

namespace EShop.Application.Service
{
    public interface IAdminConcertService
    {
        Task<Concert> AddConcertAsync(Concert concerts);
        Task<Concert?> UpdateConcertAsync(int id, Concert concerts);
        Task DeleteConcertAsync(int concert_id);
    }
}
