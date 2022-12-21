function Add(ele, HotelId, FoodId, Price, FoodName, HotelName, ImageId) {
    var qty = $(ele).parent("div").find("#Quantity").val();

    if (qty == null || qty == 0) {
        $('#errormsg').html("Please add Minimum Quantity of 1 ");
        return;
    }

    const obj = { hotelId: HotelId, foodId: FoodId, price: Price, quantity: qty, foodname: FoodName, hotelname: HotelName, imageid: ImageId }
    const existing = localStorage.getItem('cart');
    const list = JSON.parse(existing) || [];
    var index = list.findIndex(x => x.foodId === FoodId && x.hotelId === HotelId);

    if (index == -1) {
        list.push(obj);
        var qty = $(ele).parent("div").find("#Quantity").val('0');
    }

    else {
        list[index].quantity = qty;
        var qty = $(ele).parent("div").find("#Quantity").val('0');
    }

    localStorage.setItem('cart', JSON.stringify(list));
    alert("food is added succesfully");
}
