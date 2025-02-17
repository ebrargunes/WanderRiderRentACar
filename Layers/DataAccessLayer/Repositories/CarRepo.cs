using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WanderRiderRentACar.DataAccessLayer;
using WanderRiderRentACar.DMO;

public class CarRepo : ICarRepo
{
    private readonly WanderRiderContext _context;
    private readonly IMapper _mapper;
    public CarRepo(WanderRiderContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CarDTO>> GetCarsByOfficeId(int rentStoreId)
    {
        var result = await _context.Cars
            .Include(x => x.Transmission)
            .Include(x => x.FuelType)
            .Include(x => x.Segment)
            .Where(x => x.RentStoresId == rentStoreId)
            .Select(x => new CarDTO
            {
                CarId = x.CarId,
                CarName = x.CarName,
                FuelTypeName = x.FuelType.FuelTypeName,
                Model = x.Model,
                TransmissionName = x.Transmission.TransmissionName,
                SegmentName = x.Segment.SegmentName,
                RentStoresName = x.RentStores.RentStoresName,
                DepositPrice = x.Segment.DepositPrice,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();

        return _mapper.Map<List<CarDTO>>(result);
    }

    //     public async Task<List<CarDTO>> GetAvailableCars(int rentStoreId, DateOnly pickupDate, DateOnly returnDate)
    //     {
    //         var result = await _context.Cars
    //             .Include(x => x.Transmission)
    //             .Include(x => x.FuelType)
    //             .Include(x => x.Segment)
    //             .Include(x => x.Sales)    // Rezervasyonları dahil ettik
    //             .Where(x => x.RentStoresId == rentStoreId && !x.Sales.Any(r =>
    //     (pickupDate >= r.PickupDate && pickupDate <= r.ReturnDate) ||
    //     (returnDate >= r.PickupDate && returnDate <= r.ReturnDate) ||
    //     (pickupDate <= r.PickupDate && returnDate >= r.ReturnDate)
    // ))
    //             .Select(x => new CarDTO
    //             {
    //                 CarId = x.CarId,
    //                 CarName = x.CarName,
    //                 FuelTypeName = x.FuelType.FuelTypeName,
    //                 Model = x.Model,
    //                 TransmissionName = x.Transmission.TransmissionName,
    //                 SegmentName = x.Segment.SegmentName,
    //                 RentStoresName = x.RentStores.RentStoresName,
    //                 DepositPrice = x.Segment.DepositPrice,
    //                 ImageUrl = x.ImageUrl,
    //             })
    //             .ToListAsync();

    //         return _mapper.Map<List<CarDTO>>(result);
    //     }

    public async Task<List<CarDTO>> GetAllCars()
    {
        var query = _context.Cars
            .Include(x => x.Transmission)
            .Include(x => x.FuelType)
            .Include(x => x.Segment)
            .AsQueryable();

        return await query.Select(x => new CarDTO
        {
            CarId = x.CarId,
            CarName = x.CarName,
            FuelTypeName = x.FuelType.FuelTypeName,
            Model = x.Model,
            TransmissionName = x.Transmission.TransmissionName,
            SegmentName = x.Segment.SegmentName,
            DepositPrice = x.Segment.DepositPrice,
            RentStoresName = x.RentStores.RentStoresName,
            ImageUrl = x.ImageUrl,
        }).ToListAsync();
    }

    public List<CarDTO> GetCarsByFilters(int fuelTypeId, int segmentId, int transmissionId)
    {
        var query = _context.Cars
        .Include(x => x.Transmission)
        .Include(x => x.FuelType)
        .Include(x => x.Segment)
        .AsQueryable();

        query = query.Where(x => x.IsAvailable == true);

        if (fuelTypeId > 0)
            query = query.Where(x => x.FuelTypeId == fuelTypeId);

        if (segmentId > 0)
            query = query.Where(x => x.SegmentId == segmentId);

        if (transmissionId > 0)
            query = query.Where(x => x.TransmissionId == transmissionId);

        return query.Select(p => new CarDTO   // burada mapleme yok aslında burada bir select sorgusu atılıyor. select * from demiyoruz istediğimiz column ları işaretliyoruz
        {
            CarId = p.CarId,
            CarName = p.CarName,
            FuelTypeName = p.FuelType.FuelTypeName,
            TransmissionName = p.Transmission.TransmissionName,
            SegmentName = p.Segment.SegmentName,
            ImageUrl = p.ImageUrl,
            DepositPrice = p.Segment.DepositPrice,
            Model = p.Model,
        }).ToList();
    }


    public async Task<CarDTO> GetSingleCarById(int id)
    {
        var result = await _context.Cars
            .Where(x => x.CarId == id)
            .Select(x => new CarDTO
            {
                CarId = x.CarId,
                CarName = x.CarName,
                FuelTypeName = x.FuelType.FuelTypeName,
                FuelTypeId = x.FuelTypeId,
                Model = x.Model,
                TransmissionName = x.Transmission.TransmissionName,
                TransmissionId = x.TransmissionId,
                SegmentName = x.Segment.SegmentName,
                SegmentId = x.SegmentId,
                RentStoresName = x.RentStores.RentStoresName,
                RentStoresId = x.RentStoresId,
                ImageUrl = x.ImageUrl,
                DepositPrice = x.Segment.DepositPrice,
                IsAvailable = x.IsAvailable,

            })
            .FirstOrDefaultAsync();

        return result;
    }


    public async Task<List<CarDTO>> GetOnlyAvailabeCarsAsync(DateOnly pickupDate, DateOnly returnDate, int rentStoreId)
    {
        return await _context.Cars
                    .Where(x => x.RentStoresId == rentStoreId)
                    .Where(c => !_context.Sales.Any(s => s.CarId == c.CarId &&
                        !(s.ReturnDate < pickupDate || s.PickupDate > returnDate)))
                    .Select(y => new CarDTO
                    {
                        CarId = y.CarId,
                        CarName = y.CarName,
                        FuelTypeName = y.FuelType.FuelTypeName,
                        Model = y.Model,
                        TransmissionName = y.Transmission.TransmissionName,
                        SegmentName = y.Segment.SegmentName,
                        RentStoresName = y.RentStores.RentStoresName,
                        DepositPrice = y.Segment.DepositPrice,
                        ImageUrl = y.ImageUrl,
                    })
                            .ToListAsync();
    }



}

