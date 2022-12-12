var obj;
var foodDetails = [];
var data = localStorage.getItem("cart");
console.log("data", data);

$.each(JSON.parse(data), function (key, value) {
    $('#productList').append('<tr><td class="word">' + value.hotelname + '</td><td>' + value.foodname + '</td><td>' + ' <img class="foodimg" src="/css/Image/' + value.imageid + '" />' + '</td><td>' + value.price + '</td><td>' + value.quantity + '</td><td>' + value.quantity * value.price + '</td><td><button class="btn btn-danger delete ">Delete </button></td></tr>');









    obj = { "FoodId": value.hotelId, "HotelId": value.foodId, "Quantity": value.quantity }








    foodDetails.push(obj);










});




$(document).on('click', '.delete', function () {
    let index = $(this).closest('tr').index() - 1;
    $(this).closest('tr').remove();
    console.log(index);
    foodDetails.splice(index, 1);
    //console.log(productDetails);
    //if (productDetails.length == 0) {
    //    $('#productList').hide();
    //}
});


var id;
$(document).ready(function () {
    $('#btnsubmit').click(function (e) {
        e.preventDefault();
        var id = $('#customerid').val();

        if (id == null || id == 0) {
            $('#errormsg').html("Customer is required");
            return;
        }

        var Request = {
            "CustomerId": id,
            "Food": foodDetails



        };





        $.ajax({



            url: "/OrderDetailMvc/AddOrder",
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(Request),
            contentType: 'application/json; charset=utf-8',
            success: function (response) {




                if (response.success == true) {
                    localStorage.clear();
                    setTimeout(function () { window.location = '/OrderShipmentMvc/GetShipmentByUser?CustomerId=' + id; }, 1000);
                }
                else {
                    setTimeout(function () { window.location = '/OrderDetailMvc/Add'; }, 1000);
                }




            },
            error: function () {
                alert("error");
            }



        });

    });
});