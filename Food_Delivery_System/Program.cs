using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery.RepositoryService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FoodDeliveryDbContext>(OPtions =>
{
    OPtions.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<IFood, FoodService>();
builder.Services.AddScoped<IOrders, OrdersService>();
builder.Services.AddScoped<IOrderDetail, OrderDetailService>();
builder.Services.AddScoped<IDeliveryPerson, DeliveryPersonService>();
builder.Services.AddScoped<IOrderShipmentDetail, OrderShipmentDetailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
