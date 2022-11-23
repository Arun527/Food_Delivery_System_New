using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;


using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;


namespace Food_Delivery.Controllers_Mvc
{
    public class FoodMvcController : Controller
    {

        private readonly ILogger<FoodMvcController> _logger;

        private readonly IWebHostEnvironment webHostEnvironment;


        IFood _food;

        IHotel _hotel;
        public FoodMvcController(ILogger<FoodMvcController> logger, IFood obj,IHotel hottel, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _food = obj;
            _hotel=hottel;
            this.webHostEnvironment = webHostEnvironment;
        }

  
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> AddFood(Food foodType)
        {
            var uploadDirecotroy = "Css/Image/";

            string location = "~wwwroot/Css/Image/";
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, uploadDirecotroy);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(foodType.CoverPhoto.FileName);
            var Iamgepath = Path.Combine(uploadPath, fileName);
            await foodType.CoverPhoto.CopyToAsync(new FileStream(Iamgepath, FileMode.Create)); ;
            foodType.ImageId = fileName;

            _food.InsertFoodType(foodType);
            return RedirectToAction("GetAllFood");
        }   

        public IActionResult Food(Food foodType)
        {
            var foodInsert = _food.InsertFoodType(foodType);
            return Json(foodInsert);
        }

        public IActionResult GetAllFood()
        {
            var foodList = _food.GetAll();
            return View(foodList);
        }

        public IActionResult GetFoodByHotelId(int hotelId)
        {
            var foodList = _food.GetFoodTypeById(hotelId);
            return View(foodList);
        }
    }
}
