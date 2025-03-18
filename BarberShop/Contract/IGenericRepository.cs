namespace BarberShop.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, Guid barberShopId);
        Task<TResult> AddAsync<TSource, TResult>(TSource source, Guid barberShopId);
        Task DeleteAsync(int id, Guid barberShopId);
        Task<bool> Exists(int id, Guid barberShopId);
        Task<List<T>> GetAllAsync(Guid barberShopId);
        Task<List<TResult>> GetAllAsync<TResult>(Guid barberShopId);
        Task<T> GetAsync(int? id, Guid barberShopId);
        Task<TResult> GetAsync<TResult>(int? id, Guid barberShopId);
        Task UpdateAsync(T entity, Guid barberShopId);
        Task UpdateAsync<TSource>(int id, TSource source, Guid barberShopId);
    }

}
