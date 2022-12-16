using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery.Controllers_Mvc
{
    public class HotelMvcController : Controller
    {
        private readonly ILogger<HotelMvcController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        IHotel _hotel;
        IFood _food;
        public HotelMvcController(ILogger<HotelMvcController> logger, IHotel obj, IFood food, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _hotel = obj;
            _food = food;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddHotel()
        {
            return View();
        }
        public async Task<IActionResult> CreateAsync(Hotel hotelDetail)
        {
            Messages msg = new Messages();
            var uploadDirecotroy = "Css/Image/";
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, uploadDirecotroy);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(hotelDetail.CoverPhoto.FileName);
            var Iamgepath = Path.Combine(uploadPath, fileName);
            await hotelDetail.CoverPhoto.CopyToAsync(new FileStream(Iamgepath, FileMode.Create));
            hotelDetail.ImageId = fileName;
            var create = _hotel.InsertHotelDetail(hotelDetail);
            return (create.Status == Statuses.Created) ? RedirectToAction("GetAll") : RedirectToAction("AddHotel");
        }

        [HttpGet("api/[controller]/{hotelName}")]
        public IActionResult GetHotelByName(string hotelName)
        {
            var hotel = _hotel.GetHotelDetailByName(hotelName);
            return View(hotel);
        }
        public IActionResult GetAll()
        {
            var hotel = _hotel.GetAll();
            return View(hotel);
        }
        public IActionResult GetallDetail(JqueryDatatableParam  param)
        {
            var hoteldetail = _hotel.GetAll();
         
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                hoteldetail = hoteldetail.Where(x => x.HotelName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Address.ToLower().Contains(param.sSearch.ToLower())
                                               || x.Type.ToLower().Contains(param.sSearch.ToLower())
                                              || x.IsActive.ToString().Contains(param.sSearch.ToLower())
                                              || x.ContactNumber.ToString().Contains(param.sSearch.ToLower()));
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
        public IActionResult HotelDetail(int HotelId)
        {
            var hoteldetail = _hotel.GetHotelById(HotelId);
            return View(hoteldetail);
        }
        public IActionResult FoodDetail(string hotelName)
        {
            var fooddetail = _hotel.GetFoodByHotelName(hotelName);
            return View(fooddetail);
        }
        public IActionResult UpdateHotel(int HotelId)
        {
            var obj = _hotel.GetHotelById(HotelId);
            return View(obj);
        }
        public IActionResult Update(Hotel hoteldetail)
        {
            var obj = _hotel.UpdateHotelDetail(hoteldetail);
            TempData["AlertMessage"] = obj.Message;
            return (obj.Status == Statuses.Success) ? Redirect( "GetAll?hotelId=" + hoteldetail.HotelId) : Redirect("UpdateHotel?hotelId=" + hoteldetail.HotelId);
        }
        public IActionResult DeleteHotel(int hotelId)
        {
            var obj = _hotel.DeleteHotelDetail(hotelId);
            TempData["AlertMessage"] = obj.Message;
            return View("GetAll");
        }
    }
}
