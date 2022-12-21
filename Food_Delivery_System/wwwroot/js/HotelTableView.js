$(document).ready(function () {
    bindDatatable();
});

function bindDatatable() {

    datatable = $('#tblHotel')
        .DataTable({
            "sAjaxSource": "/HotelMvc/GetallDetail",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "order": [[1, 'asc']],
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:blue;margin-left:50%;margin-top:80%;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "hotelId",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "hotelName",
                    "autoWidth": true,
                    "searchable": true,
                    'render': function (data, type, row) {
                        return "<a href=/FoodMvc/GetFoodByHotelId?hotelId=" + row.hotelId + " class='nav-link '> <span class=tooltiptext>View Food Detail</span>  " + row.hotelName + " </a>"
                    }
                },
                {
                    "data": "address",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "contactNumber",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "email",
                    "autoWidth": true,
                    "searchable": true
                },

                {
                    "data": "type",
                    "autoWidth": true,
                    "searchable": true
                },
                {

                    "searchable": false,
                    'render': function (data, type, row) 
                    {
                        return " <a href=/HotelMvc/UpdateHotel?hotelId=" + row.hotelId + " class='btn'> <span class=tooltiptext>Edit</span> <i style='font-size:24px ;color:cornflowerblue' class='fas'>&#xf044;</i> </a>  <a href=/HotelMvc/DeleteHotel?hotelId=" + row.hotelId + " id='del' class='btn '> <span class=tooltiptext>Delete</span> <i style='font-size:24px; color:red' class='fas'>&#xf2ed;</i> </a>"
                    }

                },
            ]
        });
}


