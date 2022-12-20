var obj;
var orderDetail=[];
var Request;

$(document).ready(function () {

    $('#addid').click(function (e) {

        e.preventDefault();
        var id = $('#DeliveryPersonId').val();
        var orderId = $('#OrderId').val();
        if (DeliveryPersonId == null || DeliveryPersonId == 0) {
            $('#errormsg').html("Delivery Person  is required");
            return;
        }
        obj = { "OrderDetailId": orderId }

        $('#idList').append('<tr><td>' + orderId + '</td><td><button class="btn btn-danger delete">Delete </button></td></tr>');

        orderDetail.push(obj);

        $(document).on('click', '.delete', function () {
            let index = $(this).closest('tr').index() - 1;
            $(this).closest('tr').remove();
            console.log(index);
            productDetails.splice(index, 1);
            console.log(productDetails);
            if (orderDetail.length == 0) {
                $('#idList').hide();
            }
        });

        Request = {
            "DeliveryPersonId": id,
            "ShipmentRequest": orderDetail
        };
    });
$('#btnsubmit').click(function (e) {
    $.ajax({
        url: "/OrderShipmentMvc/Add",
        type: "POST",
        dataType: 'json',
        data: JSON.stringify(Request),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {

            alert("Order is Succesfully Shipped");
            window.location.href = "/OrderShipmentMvc/GetAllShipment";
        },
        error: function () {
            alert("error");
        }
    });

    });
});






