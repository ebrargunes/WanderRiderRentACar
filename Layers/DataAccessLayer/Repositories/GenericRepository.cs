using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WanderRiderRentACar.DataAccessLayer;
using WanderRiderRentACar.DMO;

/// <summary>
/// Temel veritabani islemelerini gerceklestiren GenericRepository sinifi
/// </summary>
/// <typeparam name="T">Veritabani tablosuna karsilik gelen entity tipi</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly WanderRiderContext _context;
    private readonly DbSet<T> _dbSet;


    public GenericRepository(WanderRiderContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Veritabanindaki tum kayitlari getirir
    /// </summary>
    /// <returns>Entity listesini dondurur</returns>
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }


    /// <summary>
    /// Id'ye gore veritabanindan kayit getirir
    /// </summary>
    /// <param name="id">Aranacak kaydin ID degeri</param>
    /// <returns>ID'ye karsilik gelen entity dondurulur</returns>
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Belirtilen filtreye uygun tum kayitlari geitirir
    /// </summary>
    /// <param name="predicate">Filtreleme kriteri. Expression olarak</param>
    /// <returns>Filtreye uygun kayitlarin listesini dondurur</returns>
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }


    /// <summary>
    /// Belirtilen filtreye uygun bulunan ilk kaydi getirir
    /// </summary>
    /// <param name="predicate">Filtreleme kriteri. Expression olarak</param>
    /// <returns>Filtreye uygun ilk bulunan kayit donulur, eger kayit bulunmazsa null doner</returns>
    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Veritabanina yeni kayit ekler
    /// </summary>
    /// <param name="entity">Eklenecek entitu</param>
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);     //Yeni varlığı (aracı) eklemek için hazırlar.
        await _context.SaveChangesAsync();
        return entity;

    }


    /// <summary>
    /// Veritabanina birden fazla kayit ekler
    /// </summary>
    /// <param name="entities">Eklenecek entity listesi</param>
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// Mevcut bir kaydi gunceller
    /// </summary>
    /// <param name="entity">Guncellenecek entity</param>
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// Mevcut bir kaydi siler
    /// </summary>
    /// <param name="entity">Silinecek entity</param>
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <summary>
    /// Birden fazla kaydi siler
    /// </summary>
    /// <param name="entities">Silinecek entity listesi</param>
    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);

    }

    public Car GetById(int carId)
    {
        throw new NotImplementedException();
    }


}


// remove yapmak yerine isAvailable= false işaretlemek ve sonra update çağırmak daha doğru. _dbSet.Update(entity);
// entity.isAvailable=false;
//_dbSet.Update(entity);

