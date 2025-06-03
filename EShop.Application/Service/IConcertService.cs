using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;


namespace EShop.Application.Service
{
    public interface IConcertService
    {
        Task<List<Concert>> GetAllConcertsAsync();
        Task<Concert?> GetConcertByIdAsync(int id);
        Task<List<Concert>> SearchConcertsAsync(string? artist, string? city);
    }

}
