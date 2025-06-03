using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application.Service
{
    public class AdminConcertService : IAdminConcertService
    {
        private readonly IAdminConcertRepository _repository;

        public AdminConcertService(IAdminConcertRepository repository)
        {
            _repository = repository;
        }

        public async Task<Concert> AddConcertAsync(Concert concert)
        {
            concert.availableSeats = concert.totalSeats;
            return await _repository.AddConcertAsync(concert);
        }

        public async Task<Concert?> UpdateConcertAsync(int id, Concert concert)
        {
            var existing = await _repository.GetConcertByIdAsync(id);
            if (existing == null)
                return null;

            existing.name = concert.name;
            existing.artist = concert.artist;
            existing.city = concert.city;
            existing.venue = concert.venue;
            existing.date = concert.date;
            existing.totalSeats = concert.totalSeats;
            existing.availableSeats = concert.availableSeats;
            existing.price = concert.price;
            existing.category = concert.category;

            return await _repository.UpdateConcertAsync(existing);
        }

        public async Task DeleteConcertAsync(int id)
        {
            await _repository.DeleteConcertAsync(id);
        }
    }
}
