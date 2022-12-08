using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
//using System.Web.Mvc;

namespace Food_Delivery.Controllers_Mvc
{
     [Route("api/[controller]")]
    [ApiController]
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

            string location = "~wwwroot/Css/Image/";
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, uploadDirecotroy);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(hotelDetail.CoverPhoto.FileName);
            var Iamgepath = Path.Combine(uploadPath, fileName);
            await hotelDetail.CoverPhoto.CopyToAsync(new FileStream(Iamgepath, FileMode.Create));
            hotelDetail.ImageId = fileName;


            var create = _hotel.InsertHotelDetail(hotelDetail);

            return RedirectToAction("GetAll");

            //if(msg.Success==true)
            //{
            //    TempData["AlertMessage"] = "Hotel Created Successfully.. !";
            //}
            //TempData["AlertMessage"] = "The Hotel Already Exist.. !";

        }

        [HttpGet("{hotelName}")]
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
                                              || x.IsActive.ToString().Contains(param.sSearch.ToLower()));
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
            return Redirect("GetAll?hotelId=" + id);
        }

        public IActionResult DeleteHotel(int hotelId)
        {
            var obj = _hotel.DeleteHotelDetail(hotelId);
            TempData["AlertMessage"] = "Hotel Deleted Successfully.. !";
            return View("GetAll");
        }


    }
}
