using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using static Food_Delivery.Models.Messages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using Food_Delivery_System.Models;

namespace Food_Delivery.Controllers_Mvc
{
    public class FoodMvcController : Controller
    {
        private readonly ILogger<FoodMvcController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        IFood _food;
        IHotel _hotel;
        int hotelId;
        public FoodMvcController(ILogger<FoodMvcController> logger, IFood obj,IHotel hottel,IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Explore()
        {
            var hoteldetail = _hotel.GetAll();
            return View(hoteldetail);
        }
        public IActionResult GetallDetail(JqueryDatatableParam param)
        {
            var hoteldetail = _hotel.GetAll();
            var parm = param.sSearch.ToLower();
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                hoteldetail = hoteldetail.Where(x => x.HotelName.ToLower().Contains(parm)
                                              || x.Address.ToLower().Contains(parm)
                                               || x.Type.ToLower().Contains(parm)
                                              || x.IsActive.ToString().Contains(parm)
                                               || x.ContactNumber.ToString().Contains(parm));
            }
            var displayResult = hoteldetail.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = hoteldetail.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }
        public IActionResult Add(int HotelId)
        {
            var id = HotelId;
            FoodDto types = new FoodDto();
            var hotelList = _hotel.GetAll();
            types.hotelname = new List<SelectListItem>();
            types.hotelname.AddRange(
                hotelList.Where(x =>x.HotelId == id).Select(a => new SelectListItem
                {
                    Text = a.HotelName,
                    Value = a.HotelId.ToString(),
                }));
            return View(types);
        }
        public async Task<IActionResult> AddFood(Food foodType)
        {
            var uploadDirecotroy = "Css/Image/";
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, uploadDirecotroy);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(foodType.CoverPhoto.FileName);
            var Iamgepath = Path.Combine(uploadPath, fileName);
            await foodType.CoverPhoto.CopyToAsync(new FileStream(Iamgepath, FileMode.Create)); 
            foodType.ImageId = fileName;
              var food= _food.InsertFoodType(foodType);
            if (food.Status == Statuses.Created )
            {
                TempData["AlertMessage"] = food.Message;
            }
            return Redirect("GetFoodByHotelId?HotelId=" + foodType.HotelId);
        }
        public IActionResult Food(Food foodType)
        {
            var foodInsert = _food.InsertFoodType(foodType);
            return Json(foodInsert);
        }
        public IActionResult GetAllFood()
        {
            var foodList = _food.GetAllFood();
            return View(foodList);
        }
        public IActionResult UpdateFood(int FoodId)
        {
            var obj = _food.GetFoodTypeById(FoodId);
            return View(obj);
        }
        public IActionResult Update(Food foodDetaile)
        {
            var obj = _food.UpdateFood(foodDetaile);
            TempData["AlertMessage"] = obj.Message;
            return Redirect("GetFoodByHotelId?HotelId=" + foodDetaile.HotelId);
        }
        public IActionResult DeleteFood(int foodId)
        {
            var food = _food.GetFoodTypeById(foodId);
            var id = food.HotelId;  
            var obj = _food.DeleteFoodType(foodId);
            TempData["AlertMessage"] = obj.Message;
           return Redirect("GetFoodByHotelId?HotelId=" + id);
        }
        public IActionResult GetFoodByHotelId(int hotelId)
        {
            ViewBag.hotelId = hotelId;
            var foodList = _food.GetFoodByHotelId(hotelId);
            return View(foodList);
        }
        public IActionResult UserByHotelId(int hotelId)
        {
            var foodList = _food.GetFoodByHotelId(hotelId);
            return View(foodList);
        }
    }
}
