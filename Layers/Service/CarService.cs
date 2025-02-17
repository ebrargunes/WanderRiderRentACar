using AutoMapper;
using WanderRiderRentACar.DataAccessLayer;
using WanderRiderRentACar.DMO;

public class CarService : ICarService
{
    private readonly IGenericRepository<Car> _carGenericRepository;
    private readonly ICarRepo _carRepo;
    private readonly WanderRiderContext _context;
    private readonly IMapper _mapper;


    public CarService(IGenericRepository<Car> carGenericRepository, ICarRepo carRepo, IMapper mapper, WanderRiderContext context)
    {
        _carGenericRepository = carGenericRepository;
        _carRepo = carRepo;
        _mapper = mapper;
        _context = context;
    }

    /// <summary>
    /// Bu metod detayli liste donmez, transmission type gibi bilgileri id degerelerini donecektir. Cunku generic repo ile calisiyor
    /// </summary>
    /// <returns></returns>
    public async Task<List<CarVM>> GetAllCars()
    {
        var dtoModel = await _carRepo.GetAllCars();
        return _mapper.Map<List<CarVM>>(dtoModel);
    }

    public async Task<int> RemoveCar(Car carToRemove)
    {
        _carGenericRepository.Remove(carToRemove); // araba silme islemi yapar
        var result = await _context.SaveChangesAsync();
        return result;
    }

    /// <summary>
    /// Bu Metod detayli liste doner, Ozel reposunu kullanir, verilen ofis id ye eslesen butun arabalari getirir
    /// </summary>
    /// <param name="rentStoreId"></param>
    /// <returns></returns>
    /// 

    public async Task<List<CarVM>> GetCarsByOfficeId(int rentStoreId)
    {
        var dtoList = await _carRepo.GetCarsByOfficeId(rentStoreId);
        return _mapper.Map<List<CarVM>>(dtoList);
    }

    //    public Task<List<CarVM>> GetAvailableCars(int rentStoreId, DateOnly pickupDate, DateOnly returnDate)
    //     {
    //        var result= _carRepo.GetAvailableCars(rentStoreId, pickupDate, returnDate);
    //        return _mapper.Map<List<CarVM>>(result);
    //     }

    public List<CarVM> GetCarsByFilters(int fuelType = 0, int segment = 0, int transmission = 0)
    {
        var result = _carRepo.GetCarsByFilters(fuelType, segment, transmission);
        return _mapper.Map<List<CarVM>>(result);
    }

    public async Task<CarVM> GetSingleCarById(int carId)
    {
        var result = await _carRepo.GetSingleCarById(carId);
        return _mapper.Map<CarVM>(result);
    }

    public async Task<CarDTO> AddCar(CarDTO car)
    {
        var dmoCar = _mapper.Map<Car>(car);
        var result = await _carGenericRepository.AddAsync(dmoCar);
        return _mapper.Map<CarDTO>(result);
    }

    public async Task<int> UpdateCar(CarDTO car)
    {
        var dmoCar = _mapper.Map<Car>(car);
        _carGenericRepository.Update(dmoCar);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<CarDTO>> GetCarsByDate(DateOnly pickupDate, DateOnly returnDate, int rentStoreId)
    {
        return await _carRepo.GetOnlyAvailabeCarsAsync(pickupDate, returnDate, rentStoreId);
    }

}