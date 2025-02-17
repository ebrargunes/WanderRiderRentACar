
using AutoMapper;
using WanderRiderRentACar.DataAccessLayer;
using WanderRiderRentACar.DMO;

public class ReservationService : IReservationService
{

    private IGenericRepository<User> _userRepo;
    private IGenericRepository<Car> _carRepo;
    private IGenericRepository<Sale> _saleRepo;
    private readonly WanderRiderContext _context;
    private IMapper _mapper;

    public ReservationService(IGenericRepository<User> userRepo, IMapper mapper, IGenericRepository<Car> carRepo, WanderRiderContext context, IGenericRepository<Sale> saleRepo)
    {
        _userRepo = userRepo;
        _carRepo = carRepo;
        _saleRepo = saleRepo;

        _mapper = mapper;
        _context = context;
    }
    public async Task<int> AddUserUpdateCar(UserDTO user, CarDTO car, DateOnly pickUpDate, DateOnly returnDate)
    {
        var dmoUser = _mapper.Map<User>(user);
        var dmoCar = _mapper.Map<Car>(car);
        // dmoCar.IsAvailable = false;

        var registeredUser = await _userRepo.AddAsync(dmoUser);

        var sale = new Sale()
        {
            UserId = registeredUser.UserId,
            CarId = dmoCar.CarId,
            PickupDate = pickUpDate,
            ReturnDate = returnDate
        };
        var registeredSale = await _saleRepo.AddAsync(sale);

        _carRepo.Update(dmoCar);

        return await _context.SaveChangesAsync();
    }
}