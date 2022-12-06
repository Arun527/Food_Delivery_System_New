using Food_Delivery.Controllers_Mvc;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery_System.Controllers_Mvc
{
    public class OrderDetailMvcController : Controller
    {

        private readonly ILogger<OrderDetailMvcController> _logger;

        IOrderDetail _orderDetail;
        ICustomer _customer;
        IHotel _hotel;
        IFood _food;
        IOrders _orders;

        public OrderDetailMvcController(IOrderDetail orderDetail, ICustomer customer, IHotel hotel, IFood food, IOrders orders)
        {
            _orderDetail = orderDetail;
            _customer = customer;
            _hotel = hotel;
            _food = food;
            _orders = orders;
        }

        List<InvoiceDetail> li = new List<InvoiceDetail>();
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            OrderRequest types = new OrderRequest();
            var customer = _customer.GetAll();
            types.CustomerList = new List<SelectListItem>();
            types.CustomerList.Add(new SelectListItem() { Value = "0", Text = "Select Customer" });
            types.CustomerList.AddRange(_customer.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.CustomerId.ToString(),
            }));
            return View(types);
            //    return View();

        }
        public IActionResult AddOrder([FromBody] OrderRequest food)
        {
            //List<FoodDetaile> obj = new List<FoodDetaile>();
            //obj = food.Food;

            var orderDetail = _orderDetail.InsertOrderDetail(food);
            return Json(orderDetail);

        }




        //public IActionResult AddCart(int id)
        //{
        //    var query = _food.GetFoodByHotelId(id);
        //    InvoiceDetail invoiceDetail = new InvoiceDetail();
        //    invoiceDetail.FoodId = id;
        //    li.Add(invoiceDetail);
        //    //TempData["InvoiceDetail"]=li;
        //    TempData.Keep();

        //    return Redirect("Add");
        //}

    }
}