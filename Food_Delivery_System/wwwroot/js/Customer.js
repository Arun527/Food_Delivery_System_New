




<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.12.1/datatables.min.css" />





     src="https://cdn.datatables.net/v/dt/dt-1.12.1/datatables.min.js"
     src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"
     src="https://cdn.datatables.net/1.10.20/js/dataTables.bootstrap.min.js"
   
        $(document).ready(function () {
            bindDatatable();
        });

        function bindDatatable() {
            datatable = $('#tblHotel')
                .DataTable({
                    "sAjaxSource": "/CustomerMvc/Getall",
                    "bServerSide": true,
                    "bProcessing": true,
                    "bSearchable": true,
                    "order": [[1, 'asc']],
                    "language": {
                        "emptyTable": "No record found.",
                        "processing":
                            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;margin-left:50%;margin-top:80%;color:blue"></i><span class="sr-only">Loading...</span> '
                    },
                    "columns": [
                        {
                            "data": "customerId",
                            "autoWidth": true,
                            "searchable": true
                        },

                        {
                            "data": "name",
                            "autoWidth": true,
                            "searchable": true,
                            'render': function (data, type, row) {
                                return "<a href=/OrderShipmentMvc/GetShipmentByUser?customerId=" + row.customerId + " class='nav-link '> <span class=tooltiptext> View Order Detail </span> " + row.name + " </a>"
                            }
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
                            "data": "gender",
                            "autoWidth": true,
                            "searchable": true
                        },
                        {
                            "data": "address",
                            "autoWidth": true,
                            "searchable": true
                        },
                        {

                            "searchable": false,
                            'render': function (data, type, row) {
                                return "<a href=/CustomerMvc/UpdateCustomer?customerId=" + row.customerId + " class='btn '> <span class=tooltiptext>Edit</span> <i style='font-size:24px;color:cornflowerblue' class='fas'>&#xf044;</i> </a> <a href=/CustomerMvc/Delete?customerId=" + row.customerId + " class='btn '>  <span class=tooltiptext>Delete</span> <i style='font-size:24px;color:red' class='fas'>&#xf2ed;</i> </a>"

                            }


                        },

                    ]
                });
        }


     src='https://kit.fontawesome.com/a076d05399.js' crossorigin='anonymous'></script
    <link rel="stylesheet" href="~/css/jquerytable.css" asp-append-version="true" />

