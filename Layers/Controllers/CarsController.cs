using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WanderRiderRentACar.DMO;

public class CarsController : Controller
{
    private readonly ISelectorService _selectorService;
    private readonly ICarService _carService;
    private readonly IMapper _mapper;
    private readonly List<TransmissionVM> transmissionList;
    private readonly List<SegmentVM> segmentsLists;
    private readonly List<FuelTypeVM> fuelTypesList;

    public CarsController(ISelectorService selectorService, ICarService carService, IMapper mapper)
    {
        _mapper = mapper;
        _selectorService = selectorService;
        _carService = carService;


        transmissionList = _mapper.Map<List<TransmissionVM>>(_selectorService.GetTransmissions().Result.ToList());
        segmentsLists = _mapper.Map<List<SegmentVM>>(_selectorService.GetSegments().Result.ToList());
        fuelTypesList = _mapper.Map<List<FuelTypeVM>>(_selectorService.GetFuelTypes().Result.ToList());
    }
    public async Task<IActionResult> Index(IndexPageMainModel query)
    {
        var pickupString = HttpContext.Session.GetString("PickUpDate");
        var returnString = HttpContext.Session.GetString("ReturnDate");

        var pickupDate = DateOnly.Parse(pickupString);
        var returnDate = DateOnly.Parse(returnString);
        var selStoreId = query.SelectedOfficeId;
        var carList = await _carService.GetCarsByDate(pickupDate, returnDate, selStoreId);
        // sessiondan kullanincin sectigi tarih bilgilerini geri getirelim
        var breadCrumb = BreadCrumbCreator(HttpContext);

        var model = new CarsPageMainModel()
        {
            Transmissions = transmissionList,
            FuelTypes = fuelTypesList,
            Segments = segmentsLists,
            Cars = _mapper.Map<List<CarVM>>(carList),
            BreadCrumb = breadCrumb,
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Index(CarsPageMainModel model)
    {
        model.Transmissions = transmissionList;
        model.FuelTypes = fuelTypesList;
        model.Segments = segmentsLists;


        var breadCrumb = BreadCrumbCreator(HttpContext);

        model.BreadCrumb = breadCrumb;

        model.Cars = _carService.GetCarsByFilters(model.SelFuelTypeId, model.SelSegmentId, model.SelTransmissionId);
        if (model.Cars.Count <= 0)
        {
            TempData["Message"] = "Seciminize uygun arac bulunamadi";
        }
        return View(model);
    }

    private static BreadCrumbModel BreadCrumbCreator(HttpContext context)
    {
        var stringPickupDate = context.Session.GetString("PickUpDate");
        var stringReturnDate = context.Session.GetString("ReturnDate");

        var pickupDate = DateOnly.Parse(stringPickupDate);
        var returnDate = DateOnly.Parse(stringReturnDate);
        return new BreadCrumbModel()
        {
            Step = 1,
            PickUpDate = pickupDate,
            ReturnDate = returnDate
        };

    }
}

