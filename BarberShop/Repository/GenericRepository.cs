using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Contract; 
using BarberShop.Data;
using BarberShop.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(BarberShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity, Guid barberShopId)
        {
            // Assuming T has a BarberShopId property. If not, this needs a different approach.
            _context.Entry(entity).Property("BarberShopId").CurrentValue = barberShopId;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source, Guid barberShopId)
        {
            var entity = _mapper.Map<T>(source);
            // Assuming T has a BarberShopId property. If not, this needs a different approach.
            _context.Entry(entity).Property("BarberShopId").CurrentValue = barberShopId;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TResult>(entity);
        }

        public async Task DeleteAsync(int id, Guid barberShopId)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null || (Guid)_context.Entry(entity).Property("BarberShopId").CurrentValue != barberShopId)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id, Guid barberShopId)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity != null && (Guid)_context.Entry(entity).Property("BarberShopId").CurrentValue == barberShopId;
        }

        public async Task<List<T>> GetAllAsync(Guid barberShopId)
        {
            return await _context.Set<T>()
                                 .Where(e => EF.Property<Guid>(e, "BarberShopId") == barberShopId)
                                 .ToListAsync();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Guid barberShopId)
        {
            return await _context.Set<T>()
                                 .Where(e => EF.Property<Guid>(e, "BarberShopId") == barberShopId)
                                 .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                 .ToListAsync();
        }

        public async Task<T> GetAsync(int? id, Guid barberShopId)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null && (Guid)_context.Entry(entity).Property("BarberShopId").CurrentValue == barberShopId)
            {
                return entity;
            }
            return null;
        }

        public async Task<TResult> GetAsync<TResult>(int? id, Guid barberShopId)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null || (Guid)_context.Entry(entity).Property("BarberShopId").CurrentValue != barberShopId)
            {
                throw new NotFoundException(typeof(T).Name, id.HasValue ? id.ToString() : "No Key Provided");
            }
            return _mapper.Map<TResult>(entity);
        }

        public async Task UpdateAsync(T entity, Guid barberShopId)
        {
            if ((Guid)_context.Entry(entity).Property("BarberShopId").CurrentValue != barberShopId)
            {
                throw new UnauthorizedAccessException("Attempted to update an entity for a different BarberShopId.");
            }
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<TSource>(int id, TSource source, Guid barberShopId)
        {
            var entity = await GetAsync(id, barberShopId);
            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }
            _mapper.Map(source, entity);
            await UpdateAsync(entity, barberShopId);
        }
    }
}
