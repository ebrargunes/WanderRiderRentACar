using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

public interface IAdminController
{
    Task<IActionResult> Index();
    Task<IActionResult> RemoveCar(int carId);
}

public class AdminController : Controller, IAdminController
{
    private readonly ICarService _carService;
    private readonly ISelectorService _selectorService;
    private readonly IMapper _mapper;
    public AdminController(ICarService carService, ISelectorService selectorService, IMapper mapper)
    {
        _mapper = mapper;
        _carService = carService;
        _selectorService = selectorService;
    }

    public IActionResult Login()
    {   
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginVM adminModel)
    {
        if(adminModel.UserName!="EbrarMerve" || adminModel.Password!="wissen2024")
        {
            TempData["Message"]="Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("Login");
        }
        else
        {
            HttpContext.Session.SetString("isAdmin","bullshits");
        }
        return RedirectToAction("Index","Admin");
    }

    public async Task<IActionResult> Index()
    {
        var isAdmin = HttpContext.Session.GetString("isAdmin");
        if(isAdmin==null)
        {
            return RedirectToAction("Login");
        }
        var model = await _carService.GetAllCars();

        return View(model);
    }
 
    [HttpGet]
    public async Task<IActionResult> CreateCar()
    {
        var isAdmin = HttpContext.Session.GetString("isAdmin");
        if(isAdmin==null)
        {
            return RedirectToAction("Login");
        }
        var fuelTypes = _mapper.Map<List<FuelTypeVM>>(await _selectorService.GetFuelTypes());
        var segmentTypes = _mapper.Map<List<SegmentVM>>(await _selectorService.GetSegments());
        var transmissionTypes = _mapper.Map<List<TransmissionVM>>(await _selectorService.GetTransmissions());
        var rentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());
        var model = new AdminAddCarModel()
        {
            CarName = " ", // Set the CarName property
            FuelTypes = fuelTypes,
            Transmissions = transmissionTypes,
            Segments = segmentTypes,
            RentStores = rentStores
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar(AdminAddCarModel newcarmodel)
    {
        newcarmodel.FuelTypes = _mapper.Map<List<FuelTypeVM>>(await _selectorService.GetFuelTypes());
        newcarmodel.Segments = _mapper.Map<List<SegmentVM>>(await _selectorService.GetSegments());
        newcarmodel.Transmissions = _mapper.Map<List<TransmissionVM>>(await _selectorService.GetTransmissions());
        newcarmodel.RentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());

        var state = ModelState;
        ModelState.Remove("Segments");
        ModelState.Remove("FuelTypes");
        ModelState.Remove("RentStores");
        ModelState.Remove("Transmissions");
        if (!ModelState.IsValid) // Formda eksik veya yanlış veri varsa
        {
            return View(newcarmodel); // Hatalı form tekrar gösterilir
        }

        var newCar = new CarVM()
        {
            CarName = newcarmodel.CarName,
            FuelTypeId = newcarmodel.SelFuelTypeId,
            Model = newcarmodel.Model,
            TransmissionId = newcarmodel.SelTransmissionId,
            SegmentId = newcarmodel.SelSegmentId,
            RentStoresId = newcarmodel.SelRentStoreId,
            IsAvailable = true,
            ImageUrl = newcarmodel.ImageUrl,
            DepositPrice = newcarmodel.DepositPrice,
        };

        var dtoCar = _mapper.Map<CarDTO>(newCar);

        // DB'ye kaydetmek için Service kullan
        var result = await _carService.AddCar(dtoCar);

        if (result != null)
        {
            TempData["Message"] = "Araç başariyla eklendi!";
        }
        else
        {
            TempData["Message"] = "Araç ekleme işlemi başarisiz oldu.";
        }
        var model = await _carService.GetAllCars();
        return RedirectToAction("Index");


    }


    [HttpPost]
    public async Task<IActionResult> RemoveCar(int carId)
    {

        var carToRemove = await _carService.GetSingleCarById(carId);

        if (carToRemove == null)
        {

            TempData["Message"] = "Araç bulunamadi!";
            return RedirectToAction("Index");
        }

        var carEntity = _mapper.Map<WanderRiderRentACar.DMO.Car>(carToRemove);

        var result = await _carService.RemoveCar(carEntity);

        if (result > 0)
        {
            TempData["Message"] = "Araç başariyla silindi!";
        }
        else
        {
            TempData["Message"] = "Silme işlemi başarisiz oldu.";
        }
        var model = await _carService.GetAllCars();
        return RedirectToAction("Index", model);
    }


    public async Task<IActionResult> UpdateCar(int carId)
    {
        var isAdmin = HttpContext.Session.GetString("isAdmin");
        if(isAdmin==null)
        {
            return RedirectToAction("Login");
        }
        var carToUpdate = await _carService.GetSingleCarById(carId);
        var fuelTypes = _mapper.Map<List<FuelTypeVM>>(await _selectorService.GetFuelTypes());
        var segmentTypes = _mapper.Map<List<SegmentVM>>(await _selectorService.GetSegments());
        var transmissionTypes = _mapper.Map<List<TransmissionVM>>(await _selectorService.GetTransmissions());
        var rentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());

        var model = new AdminAddCarModel()
        {
            FuelTypes = fuelTypes,
            Segments = segmentTypes,
            Transmissions = transmissionTypes,
            RentStores = rentStores,
            CarName = carToUpdate.CarName,
            Model = carToUpdate.Model,
            SelFuelTypeId = carToUpdate.FuelTypeId,
            SelRentStoreId = carToUpdate.RentStoresId,
            SelTransmissionId = carToUpdate.TransmissionId,
            SelSegmentId = carToUpdate.SegmentId,
            ImageUrl = carToUpdate.ImageUrl,
            DepositPrice = carToUpdate.DepositPrice
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateCar(AdminAddCarModel updatedModel)
    {
        var fuelTypes = _mapper.Map<List<FuelTypeVM>>(await _selectorService.GetFuelTypes());
        var segmentTypes = _mapper.Map<List<SegmentVM>>(await _selectorService.GetSegments());
        var transmissionTypes = _mapper.Map<List<TransmissionVM>>(await _selectorService.GetTransmissions());
        var rentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());



        ModelState.Remove("Segments");
        ModelState.Remove("FuelTypes");
        ModelState.Remove("RentStores");
        ModelState.Remove("Transmissions");

        if (!ModelState.IsValid) // Formda eksik veya yanlış veri varsa
        {
            updatedModel.FuelTypes = fuelTypes;
            updatedModel.Segments = segmentTypes;
            updatedModel.Transmissions = transmissionTypes;
            updatedModel.RentStores = rentStores;
            return View(updatedModel); // Hatalı form tekrar gösterilir
        }

        var carToUpdate = await _carService.GetSingleCarById(updatedModel.CarId);
        carToUpdate.CarName = updatedModel.CarName;
        carToUpdate.Model = updatedModel.Model;
        carToUpdate.DepositPrice = updatedModel.DepositPrice;
        carToUpdate.FuelTypeId = updatedModel.SelFuelTypeId;
        carToUpdate.SegmentId = updatedModel.SelSegmentId;
        carToUpdate.TransmissionId = updatedModel.SelTransmissionId;
        carToUpdate.RentStoresId = updatedModel.SelRentStoreId;
        carToUpdate.ImageUrl = updatedModel.ImageUrl;

        var dtoCar = _mapper.Map<CarDTO>(carToUpdate);

        // DB'ye kaydetmek için Service kullan
        var result = await _carService.UpdateCar(dtoCar);


        if (result > 0)
        {
            TempData["Message"] = "Araç başariyla güncellendi!";
        }
        else
        {
            TempData["Message"] = "Güncelleme işlemi başarisiz oldu.";
        }
        var model = await _carService.GetAllCars();
        return RedirectToAction("Index", model);

    }



}
