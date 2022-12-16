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
            if (create.Status == Statuses.Created)
            {
                TempData["AlertMessage"] = create.Message;
                return RedirectToAction("GetAll");
            }
            else
            {
                create.Status = Statuses.Conflict;
                TempData["AlertMessage"] = create.Message;
                return RedirectToAction("AddHotel");
            }
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
            int id = hoteldetail.HotelId;
            var obj = _hotel.UpdateHotelDetail(hoteldetail);
            if (obj.Status==Statuses.Success)
            {
                TempData["AlertMessage"] = obj.Message;
                return Redirect("GetAll?hotelId=" + id);
            }
            else if (obj.Status==Statuses.Conflict)
            {
                TempData["AlertMessage"] =obj.Message;
            }
            return Redirect("UpdateHotel?hotelId=" + id);
        }
        public IActionResult DeleteHotel(int hotelId)
        {
            var obj = _hotel.DeleteHotelDetail(hotelId);
            if (obj.Status == Statuses.Success)
            {
                TempData["AlertMessage"] = obj.Message;
            }
            else
            {
                obj.Status = Statuses.BadRequest;
                TempData["AlertMessage"] = obj.Message;
            }
            return View("GetAll");
        }
    }
}
