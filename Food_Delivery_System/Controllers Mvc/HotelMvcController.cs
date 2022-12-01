using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
//using System.Web.Mvc;

namespace Food_Delivery.Controllers_Mvc
{
    public class HotelMvcController : Controller
    {
        private readonly ILogger<HotelMvcController> _logger;

        IHotel _hotel;
        IFood _food;
        public HotelMvcController(ILogger<HotelMvcController> logger, IHotel obj, IFood food)
        {
            _logger = logger;
            _hotel = obj;
            _food = food;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddHotel()
        {

            return View();
        }

        public IActionResult Create(Hotel hotelDetail)
        {
            Messages msg = new Messages();
            var create = _hotel.InsertHotelDetail(hotelDetail);
            if(msg.Success==true)
            {
                TempData["AlertMessage"] = "Hotel Created Successfully.. !";
            }
            TempData["AlertMessage"] = "The Hotel Already Exist.. !";
            return RedirectToAction("GetAll");
        }
        public IActionResult GetAll()
        {
            return View();
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
            TempData["AlertMessage"] = "Hotel Updated Successfully.. !";
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
