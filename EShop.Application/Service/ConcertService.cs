using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application.Service
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _repository;

        public ConcertService(IConcertRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Concert>> GetAllConcertsAsync() => _repository.GetAllAsync();

        public Task<Concert?> GetConcertByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<List<Concert>> SearchConcertsAsync(string? artist, string? city) =>
            _repository.SearchAsync(artist, city);
    }
}
