using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery.RepositoryService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FoodDeliveryDbContext>(OPtions =>
{
    OPtions.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
    OPtions.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<IFood, FoodService>();
builder.Services.AddScoped<IOrders, OrdersService>();
builder.Services.AddScoped<IOrderDetail, OrderDetailService>();
builder.Services.AddScoped<IDeliveryPerson, DeliveryPersonService>();
builder.Services.AddScoped<IOrderShipmentDetail, OrderShipmentDetailService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.Run();