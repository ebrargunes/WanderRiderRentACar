using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

public class ReservationController : Controller
{
    private readonly IReservationService _resService;
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public ReservationController(IReservationService resService, IMapper mapper, ICarService carService)
    {
        _resService = resService;
        _carService = carService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int carId)
    {
        var car = await _carService.GetSingleCarById(carId);

        var stringPickupDate = HttpContext.Session.GetString("PickUpDate");
        var stringReturnDate = HttpContext.Session.GetString("ReturnDate");

        var pickupDate = DateOnly.Parse(stringPickupDate);
        var returnDate = DateOnly.Parse(stringReturnDate);

        var model = new ReservationMainModel()
        {
            SelCarId = carId,
            Car = car,
            User = new UserVM(),
            BreadCrumb = new BreadCrumbModel()
            {
                Step = 2,
                PickUpDate = pickupDate,
                ReturnDate = returnDate
            }
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Index(ReservationMainModel model)
    {

        var carDTO = _mapper.Map<CarDTO>(await _carService.GetSingleCarById(model.SelCarId));

        var stringPickupDate = HttpContext.Session.GetString("PickUpDate");
        var stringReturnDate = HttpContext.Session.GetString("ReturnDate");

        var pickupDate = DateOnly.Parse(stringPickupDate);
        var returnDate = DateOnly.Parse(stringReturnDate);

        var breadCrumb = new BreadCrumbModel()
        {
            Step = 2,
            PickUpDate = pickupDate,
            ReturnDate = returnDate
        };

        model.Car = _mapper.Map<CarVM>(carDTO);
        model.BreadCrumb = breadCrumb;

        ModelState.Remove("Car");
        ModelState.Remove("BreadCrumb");

        if (!ModelState.IsValid)
        {
            //eğer model geçerli değilse formu tekrar döndür
            return View(model);
        }

        var dtoUser = _mapper.Map<UserDTO>(model.User);

        var result = await _resService.AddUserUpdateCar(dtoUser, carDTO, pickupDate, returnDate);
        if (result < 1)
        {
            TempData["Message"] = "Rezervasyon basarisiz lutfen tekrar deneyin!";
        }
        else
        {
            TempData["Message"] = "Rezervasyon basarili!";
        }
        return View(model);
    }
}
